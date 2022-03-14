using DomainPack.DomainEvents;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingEditedDomainEvent : DomainEventBase
    {
        public MeetingEditedDomainEvent(Guid meetingId)
        {
            MeetingId = meetingId;
        }

        public Guid MeetingId { get; }
    }
}