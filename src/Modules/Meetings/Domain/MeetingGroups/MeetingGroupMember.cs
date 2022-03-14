using System;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

using DomainPack.Entities;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups
{
    public class MeetingGroupMember : EntityObjectBase<Guid>
    {
        internal Guid MeetingGroupId { get; private set; }

        internal Guid MemberId { get; private set; }

        private MeetingGroupMemberRole _role;

        internal DateTime JoinedDate { get; private set; }

        private bool _isActive;

        private DateTime? _leaveDate;


        private MeetingGroupMember(
            Guid meetingGroupId,
            Guid memberId,
            MeetingGroupMemberRole role)
            : base(meetingGroupId)
        {
            this.MeetingGroupId = meetingGroupId;
            this.MemberId = memberId;
            this._role = role;
            this.JoinedDate = SystemClock.Now;
            this._isActive = true;

            this.AddDomainEvent(new NewMeetingGroupMemberJoinedDomainEvent(this.MeetingGroupId, this.MemberId, this._role));
        }

        internal static MeetingGroupMember CreateNew(
            Guid meetingGroupId,
            Guid memberId,
            MeetingGroupMemberRole role)
        {
            return new MeetingGroupMember(meetingGroupId, memberId, role);
        }

        internal void Leave()
        {
            _isActive = false;
            _leaveDate = SystemClock.Now;

            this.AddDomainEvent(new MeetingGroupMemberLeftGroupDomainEvent(this.MeetingGroupId, this.MemberId));
        }

        internal bool IsMember(Guid memberId)
        {
            return this._isActive && this.MemberId == memberId;
        }

        internal bool IsOrganizer(Guid memberId)
        {
            return this.IsMember(memberId) && _role == MeetingGroupMemberRole.Organizer;
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}