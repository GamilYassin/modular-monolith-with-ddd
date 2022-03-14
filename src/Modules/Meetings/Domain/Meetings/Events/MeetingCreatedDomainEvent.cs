
using DomainPack.DomainEvents;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingCreatedDomainEvent : DomainEventBase
    {
        public MeetingCreatedDomainEvent(Guid meetingId)
        {
            MeetingId = meetingId;
        }

        public Guid MeetingId { get; }
    }
}