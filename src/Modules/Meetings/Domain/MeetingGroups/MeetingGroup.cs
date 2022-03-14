using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

using DomainPack.Contracts.EntitiesContracts;
using DomainPack.Entities;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups
{
    public class MeetingGroup : EntityObjectBase<Guid>, IAggregateRoot
    {

        private string _name;

        private string _description;

        private MeetingGroupLocation _location;

        private Guid _creatorId;

        private List<MeetingGroupMember> _members;

        private DateTime _createDate;

        private DateTime? _paymentDateTo;

        internal static MeetingGroup CreateBasedOnProposal(
            Guid meetingGroupProposalId,
            string name,
            string description,
            MeetingGroupLocation location,
            Guid creatorId)
        {
            return new MeetingGroup(meetingGroupProposalId, name, description, location, creatorId);
        }


        private MeetingGroup(Guid meetingGroupProposalId, string name, string description, MeetingGroupLocation location, Guid creatorId): base(meetingGroupProposalId)
        {
            this._name = name;
            this._description = description;
            this._creatorId = creatorId;
            this._location = location;
            this._createDate = SystemClock.Now;

            this.AddDomainEvent(new MeetingGroupCreatedDomainEvent(this.Id, creatorId));

            this._members = new List<MeetingGroupMember>
            {
                MeetingGroupMember.CreateNew(this.Id, this._creatorId, MeetingGroupMemberRole.Organizer)
            };
        }

        public void EditGeneralAttributes(string name, string description, MeetingGroupLocation location)
        {
            this._name = name;
            this._description = description;
            this._location = location;

            this.AddDomainEvent(new MeetingGroupGeneralAttributesEditedDomainEvent(this._name, this._description, this._location));
        }

        public void JoinToGroupMember(Guid memberId)
        {
            this.CheckRule(new MeetingGroupMemberCannotBeAddedTwiceRule(_members, memberId));

            this._members.Add(MeetingGroupMember.CreateNew(this.Id, memberId, MeetingGroupMemberRole.Member));
        }

        public void LeaveGroup(Guid memberId)
        {
            this.CheckRule(new NotActualGroupMemberCannotLeaveGroupRule(_members, memberId));

            MeetingGroupMember member = this._members.Single(x => x.IsMember(memberId));

            member.Leave();
        }

        public void SetExpirationDate(DateTime dateTo)
        {
            _paymentDateTo = dateTo;

            this.AddDomainEvent(new MeetingGroupPaymentInfoUpdatedDomainEvent(this.Id, _paymentDateTo.Value));
        }

        public Meeting CreateMeeting(
            string title,
            MeetingTerm term,
            string description,
            MeetingLocation location,
            int? attendeesLimit,
            int guestsLimit,
            Term rsvpTerm,
            Money eventFee,
            List<Guid> hostsMembersIds,
            Guid creatorId)
        {
            this.CheckRule(new MeetingCanBeOrganizedOnlyByPayedGroupRule(_paymentDateTo));

            this.CheckRule(new MeetingHostMustBeAMeetingGroupMemberRule(creatorId, hostsMembersIds, _members));

            return Meeting.CreateNew(
                this.Id,
                title,
                term,
                description,
                location,
                MeetingLimits.Create(attendeesLimit, guestsLimit),
                rsvpTerm,
                eventFee,
                hostsMembersIds,
                creatorId);
        }

        internal bool IsMemberOfGroup(Guid attendeeId)
        {
            return _members.Any(x => x.IsMember(attendeeId));
        }

        internal bool IsOrganizer(Guid memberId)
        {
            return _members.Any(x => x.IsOrganizer(memberId));
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
