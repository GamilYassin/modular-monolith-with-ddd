namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingNotAttendeeAddedDomainEvent : DomainEventBase
    {
        public Guid MeetingId { get; }

        public Guid MemberId { get; }

        public MeetingNotAttendeeAddedDomainEvent(Guid meetingId, Guid memberId)
        {
            MeetingId = meetingId;
            MemberId = memberId;
        }
    }
}