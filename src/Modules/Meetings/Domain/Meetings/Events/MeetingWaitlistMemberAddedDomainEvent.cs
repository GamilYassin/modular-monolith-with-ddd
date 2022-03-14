namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingWaitlistMemberAddedDomainEvent : DomainEventBase
    {
        public MeetingWaitlistMemberAddedDomainEvent(Guid meetingId, Guid memberId)
        {
            MeetingId = meetingId;
            MemberId = memberId;
        }

        public Guid MeetingId { get; }

        public Guid MemberId { get; }
    }
}