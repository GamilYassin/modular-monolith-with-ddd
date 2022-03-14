using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

using DomainPack.DomainEvents;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events
{
    public class NewMeetingGroupMemberJoinedDomainEvent : DomainEventBase
    {
        public Guid MeetingGroupId { get; }

        public Guid MemberId { get; }

        public MeetingGroupMemberRole Role { get; }

        public NewMeetingGroupMemberJoinedDomainEvent(Guid meetingGroupId, Guid memberId, MeetingGroupMemberRole role)
        {
            this.MeetingGroupId = meetingGroupId;
            this.MemberId = memberId;
            this.Role = role;
        }
    }
}
