
using DomainPack.DomainEvents;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events
{
    public class MeetingGroupCreatedDomainEvent : DomainEventBase
    {
        public Guid MeetingGroupId { get; }

        public Guid CreatorId { get; }

        public MeetingGroupCreatedDomainEvent(Guid meetingGroupId, Guid creatorId)
        {
            this.MeetingGroupId = meetingGroupId;
            this.CreatorId = creatorId;
        }
    }
}
