using DomainPack.DomainEvents;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingCanceledDomainEvent : DomainEventBase
    {
        public MeetingCanceledDomainEvent(Guid meetingId, Guid cancelMemberId, DateTime cancelDate)
        {
            MeetingId = meetingId;
            CancelMemberId = cancelMemberId;
            CancelDate = cancelDate;
        }

        public Guid MeetingId { get; }

        public Guid CancelMemberId { get; }

        public DateTime CancelDate { get; }
    }
}