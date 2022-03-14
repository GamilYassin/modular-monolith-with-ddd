using DomainPack.DomainEvents;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MemberSignedOffFromMeetingWaitlistDomainEvent : DomainEventBase
    {
        public MemberSignedOffFromMeetingWaitlistDomainEvent(Guid meetingId, Guid memberId)
        {
            MeetingId = meetingId;
            MemberId = memberId;
        }

        public Guid MeetingId { get; }  

        public Guid MemberId { get; }
    }
}