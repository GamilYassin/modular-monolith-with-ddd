
using DomainPack.DomainEvents;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events
{
    public class MeetingNotAttendeeChangedDecisionDomainEvent : DomainEventBase
    {
        public MeetingNotAttendeeChangedDecisionDomainEvent(Guid memberId, Guid meetingId)
        {
            MemberId = memberId;
            MeetingId = meetingId;
        }

        public Guid MemberId { get; }

        public Guid MeetingId { get; }
    }
}