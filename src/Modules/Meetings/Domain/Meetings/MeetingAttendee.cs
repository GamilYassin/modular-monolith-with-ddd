using System;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

using DomainPack.Entities;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingAttendee : EntityObjectBase<Guid>
    {
        internal Guid AttendeeId { get; private set; }

        internal Guid MeetingId { get; private set; }

        private DateTime _decisionDate;

        private MeetingAttendeeRole _role;

        private int _guestsNumber;

        private bool _decisionChanged;

        private DateTime? _decisionChangeDate;

        private DateTime? _removedDate;

        private Guid _removingMemberId;

        private string _removingReason;

        private bool _isRemoved;

        private Money _fee;

        private bool _isFeePaid;

         internal static MeetingAttendee CreateNew(
            Guid meetingId,
            Guid attendeeId,
            DateTime decisionDate,
            MeetingAttendeeRole role,
            int guestsNumber,
            Money eventFee)
        {
            return new MeetingAttendee(meetingId, attendeeId, decisionDate, role, guestsNumber, eventFee);
        }

        private MeetingAttendee(
            Guid meetingId,
            Guid attendeeId,
            DateTime decisionDate,
            MeetingAttendeeRole role,
            int guestsNumber,
            Money eventFee)
            : base(default)
        {
            this.AttendeeId = attendeeId;
            this.MeetingId = meetingId;
            this._decisionDate = decisionDate;
            this._role = role;
            _guestsNumber = guestsNumber;
            _decisionChanged = false;
            _isFeePaid = false;

            if (eventFee != Money.Undefined)
            {
                _fee = (1 + guestsNumber) * eventFee;
            }
            else
            {
                _fee = Money.Undefined;
            }

            this.AddDomainEvent(new MeetingAttendeeAddedDomainEvent(
                this.MeetingId,
                AttendeeId,
                decisionDate,
                role.Value,
                guestsNumber,
                _fee.Value,
                _fee.Currency));
        }

        internal void ChangeDecision()
        {
            _decisionChanged = true;
            _decisionChangeDate = SystemClock.Now;

            this.AddDomainEvent(new MeetingAttendeeChangedDecisionDomainEvent(this.AttendeeId, this.MeetingId));
        }

        internal bool IsActiveAttendee(Guid attendeeId)
        {
            return this.AttendeeId == attendeeId && !_decisionChanged;
        }

        internal bool IsActive()
        {
            return !_decisionChangeDate.HasValue && !_isRemoved;
        }

        internal bool IsActiveHost()
        {
            return this.IsActive() && _role == MeetingAttendeeRole.Host;
        }

        internal int GetAttendeeWithGuestsNumber()
        {
            return 1 + _guestsNumber;
        }

        internal void SetAsHost()
        {
            _role = MeetingAttendeeRole.Host;

            this.AddDomainEvent(new NewMeetingHostSetDomainEvent(this.MeetingId, this.AttendeeId));
        }

        internal void SetAsAttendee()
        {
            this.CheckRule(new MemberCannotHaveSetAttendeeRoleMoreThanOnceRule(_role));
            _role = MeetingAttendeeRole.Attendee;

            this.AddDomainEvent(new MemberSetAsAttendeeDomainEvent(this.MeetingId, this.AttendeeId));
        }

        internal void Remove(Guid removingMemberId, string reason)
        {
            this.CheckRule(new ReasonOfRemovingAttendeeFromMeetingMustBeProvidedRule(reason));

            _isRemoved = true;
            _removedDate = SystemClock.Now;
            _removingReason = reason;
            _removingMemberId = removingMemberId;

            this.AddDomainEvent(new MeetingAttendeeRemovedDomainEvent(this.AttendeeId, this.MeetingId, reason));
        }

        internal void MarkFeeAsPayed()
        {
            _isFeePaid = true;

            this.AddDomainEvent(new MeetingAttendeeFeePaidDomainEvent(this.MeetingId, this.AttendeeId));
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}