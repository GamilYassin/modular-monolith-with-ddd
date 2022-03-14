
using System;
using DomainPack.DomainEvents;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events
{
    public class MeetingAttendeeChangedDecisionDomainEvent : DomainEventBase
    {
        public MeetingAttendeeChangedDecisionDomainEvent(Guid memberId, Guid meetingId)
        {
            MemberId = memberId;
            MeetingId = meetingId;
        }

        public Guid MemberId { get; }

        public Guid MeetingId { get; }
    }
}