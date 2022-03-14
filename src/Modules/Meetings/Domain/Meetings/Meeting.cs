using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

using DomainPack.Contracts.EntitiesContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class Meeting : EntityObjectBase<Guid>, IAggregateRoot
    {
        private Guid _meetingGroupId;

        private string _title;

        private MeetingTerm _term;

        private string _description;

        private MeetingLocation _location;

        private List<MeetingAttendee> _attendees;

        private List<MeetingNotAttendee> _notAttendees;

        private List<MeetingWaitlistMember> _waitlistMembers;

        private MeetingLimits _meetingLimits;

        private Term _rsvpTerm;

        private Money _eventFee;

        private Guid _creatorId;

        private DateTime _createDate;

        private Guid _changeMemberId;

        private DateTime? _changeDate;

        private DateTime? _cancelDate;

        private Guid _cancelMemberId;

        private bool _isCanceled;

        internal static Meeting CreateNew(
            Guid meetingGroupId,
            string title,
            MeetingTerm term,
            string description,
            MeetingLocation location,
            MeetingLimits meetingLimits,
            Term rsvpTerm,
            Money eventFee,
            List<Guid> hostsMembersIds,
            Guid creatorId)
        {
            return new Meeting(
                meetingGroupId,
                title,
                term,
                description,
                location,
                meetingLimits,
                rsvpTerm,
                eventFee,
                hostsMembersIds,
                creatorId);
        }

        private Meeting(
            Guid meetingGroupId,
            string title,
            MeetingTerm term,
            string description,
            MeetingLocation location,
            MeetingLimits meetingLimits,
            Term rsvpTerm,
            Money eventFee,
            List<Guid> hostsMembersIds,
            Guid creatorId)
            : base(default)
        {
            _meetingGroupId = meetingGroupId;
            _title = title;
            _term = term;
            _description = description;
            _location = location;
            _meetingLimits = meetingLimits;

            this.SetRsvpTerm(rsvpTerm, _term);
            _eventFee = eventFee;
            _creatorId = creatorId;
            _createDate = SystemClock.Now;

            _attendees = new List<MeetingAttendee>();
            _notAttendees = new List<MeetingNotAttendee>();
            _waitlistMembers = new List<MeetingWaitlistMember>();

            this.AddDomainEvent(new MeetingCreatedDomainEvent(this.Id));
            DateTime rsvpDate = SystemClock.Now;
            if (hostsMembersIds.Any())
            {
                foreach (Guid hostMemberId in hostsMembersIds)
                {
                    _attendees.Add(MeetingAttendee.CreateNew(this.Id, hostMemberId, rsvpDate, MeetingAttendeeRole.Host, 0, Money.Undefined));
                }
            }
            else
            {
                _attendees.Add(MeetingAttendee.CreateNew(this.Id, creatorId, rsvpDate, MeetingAttendeeRole.Host, 0, Money.Undefined));
            }
        }

        public void ChangeMainAttributes(
            string title,
            MeetingTerm term,
            string description,
            MeetingLocation location,
            MeetingLimits meetingLimits,
            Term rsvpTerm,
            Money eventFee,
            Guid modifyUserId)
        {
            this.CheckRule(new AttendeesLimitCannotBeChangedToSmallerThanActiveAttendeesRule(
                meetingLimits,
                this.GetAllActiveAttendeesWithGuestsNumber()));

            _title = title;
            _term = term;
            _description = description;
            _location = location;
            _meetingLimits = meetingLimits;
            this.SetRsvpTerm(rsvpTerm, _term);
            _eventFee = eventFee;

            _changeDate = SystemClock.Now;
            _changeMemberId = modifyUserId;

            this.AddDomainEvent(new MeetingMainAttributesChangedDomainEvent(this.Id));
        }

        public void AddAttendee(MeetingGroup meetingGroup, Guid attendeeId, int guestsNumber)
        {
            this.CheckRule(new MeetingCannotBeChangedAfterStartRule(_term));

            this.CheckRule(new AttendeeCanBeAddedOnlyInRsvpTermRule(_rsvpTerm));

            this.CheckRule(new MeetingAttendeeMustBeAMemberOfGroupRule(attendeeId, meetingGroup));

            this.CheckRule(new MemberCannotBeAnAttendeeOfMeetingMoreThanOnceRule(attendeeId, _attendees));

            this.CheckRule(new MeetingGuestsNumberIsAboveLimitRule(_meetingLimits.GuestsLimit, guestsNumber));

            this.CheckRule(new MeetingAttendeesNumberIsAboveLimitRule(_meetingLimits.AttendeesLimit, this.GetAllActiveAttendeesWithGuestsNumber(), guestsNumber));

            MeetingNotAttendee notAttendee = this.GetActiveNotAttendee(attendeeId);
            notAttendee?.ChangeDecision();

            _attendees.Add(MeetingAttendee.CreateNew(
                this.Id,
                attendeeId,
                SystemClock.Now,
                MeetingAttendeeRole.Attendee,
                guestsNumber,
                _eventFee));
        }

        public void AddNotAttendee(Guid memberId)
        {
            this.CheckRule(new MeetingCannotBeChangedAfterStartRule(_term));

            this.CheckRule(new MemberCannotBeNotAttendeeTwiceRule(_notAttendees, memberId));

            _notAttendees.Add(MeetingNotAttendee.CreateNew(this.Id, memberId));

            MeetingAttendee attendee = this.GetActiveAttendee(memberId);

            attendee?.ChangeDecision();

            MeetingWaitlistMember nextWaitlistMember = _waitlistMembers
                .Where(x => x.IsActive())
                .OrderBy(x => x.SignUpDate)
                .FirstOrDefault();
            if (nextWaitlistMember != null)
            {
                _attendees.Add(MeetingAttendee.CreateNew(
                    this.Id,
                    nextWaitlistMember.MemberId,
                    nextWaitlistMember.SignUpDate,
                    MeetingAttendeeRole.Attendee,
                    0,
                    this._eventFee));
                nextWaitlistMember.MarkIsMovedToAttendees();
            }
        }

        public void ChangeNotAttendeeDecision(Guid memberId)
        {
            this.CheckRule(new MeetingCannotBeChangedAfterStartRule(_term));

            this.CheckRule(new NotActiveNotAttendeeCannotChangeDecisionRule(_notAttendees, memberId));

            MeetingNotAttendee notAttendee = _notAttendees.Single(x => x.IsActiveNotAttendee(memberId));

            notAttendee.ChangeDecision();
        }

        public void SignUpMemberToWaitlist(MeetingGroup meetingGroup, Guid memberId)
        {
            this.CheckRule(new MeetingCannotBeChangedAfterStartRule(_term));

            this.CheckRule(new AttendeeCanBeAddedOnlyInRsvpTermRule(_rsvpTerm));

            this.CheckRule(new MemberOnWaitlistMustBeAMemberOfGroupRule(meetingGroup, memberId, _attendees));

            this.CheckRule(new MemberCannotBeMoreThanOnceOnMeetingWaitlistRule(_waitlistMembers, memberId));

            _waitlistMembers.Add(MeetingWaitlistMember.CreateNew(this.Id, memberId));
        }

        public void SignOffMemberFromWaitlist(Guid memberId)
        {
            this.CheckRule(new MeetingCannotBeChangedAfterStartRule(_term));

            this.CheckRule(new NotActiveMemberOfWaitlistCannotBeSignedOffRule(_waitlistMembers, memberId));

            MeetingWaitlistMember memberOnWaitlist = this.GetActiveMemberOnWaitlist(memberId);

            memberOnWaitlist.SignOff();
        }

        public void SetHostRole(MeetingGroup meetingGroup, Guid settingMemberId, Guid newOrganizerId)
        {
            this.CheckRule(new MeetingCannotBeChangedAfterStartRule(_term));

            this.CheckRule(new OnlyMeetingOrGroupOrganizerCanSetMeetingMemberRolesRule(settingMemberId, meetingGroup, _attendees));

            this.CheckRule(new OnlyMeetingAttendeeCanHaveChangedRoleRule(_attendees, newOrganizerId));

            MeetingAttendee attendee = this.GetActiveAttendee(newOrganizerId);

            attendee.SetAsHost();
        }

        public void SetAttendeeRole(MeetingGroup meetingGroup, Guid settingMemberId, Guid newOrganizerId)
        {
            this.CheckRule(new MeetingCannotBeChangedAfterStartRule(_term));

            this.CheckRule(new OnlyMeetingOrGroupOrganizerCanSetMeetingMemberRolesRule(settingMemberId, meetingGroup, _attendees));

            this.CheckRule(new OnlyMeetingAttendeeCanHaveChangedRoleRule(_attendees, newOrganizerId));

            MeetingAttendee attendee = this.GetActiveAttendee(newOrganizerId);

            attendee.SetAsAttendee();

            int meetingHostNumber = _attendees.Count(x => x.IsActiveHost());

            this.CheckRule(new MeetingMustHaveAtLeastOneHostRule(meetingHostNumber));
        }

        public Guid GetMeetingGroupId()
        {
            return _meetingGroupId;
        }

        public void Cancel(Guid cancelMemberId)
        {
            this.CheckRule(new MeetingCannotBeChangedAfterStartRule(_term));

            if (!_isCanceled)
            {
                _isCanceled = true;
                _cancelDate = SystemClock.Now;
                _cancelMemberId = cancelMemberId;

                this.AddDomainEvent(new MeetingCanceledDomainEvent(this.Id, _cancelMemberId, _cancelDate.Value));
            }
        }

        public void RemoveAttendee(Guid attendeeId, Guid removingPersonId, string reason)
        {
            this.CheckRule(new MeetingCannotBeChangedAfterStartRule(_term));
            this.CheckRule(new OnlyActiveAttendeeCanBeRemovedFromMeetingRule(_attendees, attendeeId));

            MeetingAttendee attendee = this.GetActiveAttendee(attendeeId);

            attendee.Remove(removingPersonId, reason);
        }

        public void MarkAttendeeFeeAsPayed(Guid memberId)
        {
            MeetingAttendee attendee = GetActiveAttendee(memberId);

            attendee.MarkFeeAsPayed();
        }

        public MeetingComment AddComment(Guid authorId, string comment, MeetingGroup meetingGroup, MeetingCommentingConfiguration meetingCommentingConfiguration)
        {
            return MeetingComment.Create(
                           this.Id,
                           authorId,
                           comment,
                           meetingGroup,
                           meetingCommentingConfiguration);
        }

        public MeetingCommentingConfiguration CreateCommentingConfiguration()
        {
            return MeetingCommentingConfiguration.Create(this.Id);
        }

        private MeetingWaitlistMember GetActiveMemberOnWaitlist(Guid memberId)
        {
            return _waitlistMembers.SingleOrDefault(x => x.IsActiveOnWaitList(memberId));
        }

        private MeetingAttendee GetActiveAttendee(Guid attendeeId)
        {
            return _attendees.SingleOrDefault(x => x.IsActiveAttendee(attendeeId));
        }

        private MeetingNotAttendee GetActiveNotAttendee(Guid memberId)
        {
            return _notAttendees.SingleOrDefault(x => x.IsActiveNotAttendee(memberId));
        }

        private int GetAllActiveAttendeesWithGuestsNumber()
        {
            return _attendees.Where(x => x.IsActive()).Sum(x => x.GetAttendeeWithGuestsNumber());
        }

        private void SetRsvpTerm(Term rsvpTerm, MeetingTerm meetingTerm)
        {
            if (!rsvpTerm.EndDate.HasValue || rsvpTerm.EndDate > meetingTerm.StartDate)
            {
                _rsvpTerm = Term.CreateNewBetweenDates(rsvpTerm.StartDate, meetingTerm.StartDate);
            }
            else
            {
                _rsvpTerm = rsvpTerm;
            }
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}