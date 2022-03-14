using DomainPack.DomainEvents;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MemberSetAsAttendeeDomainEvent : DomainEventBase
    {
        public MemberSetAsAttendeeDomainEvent(Guid meetingId, Guid hostId)
        {
            MeetingId = meetingId;
            HostId = hostId;
        }

        public Guid MeetingId { get; }

        public Guid HostId { get; }
    }
}