namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingAttendeeRemovedDomainEvent : DomainEventBase
    {
        public MeetingAttendeeRemovedDomainEvent(Guid memberId, Guid meetingId, string reason)
        {
            MemberId = memberId;
            MeetingId = meetingId;
            Reason = reason;
        }

        public Guid MemberId { get; }

        public Guid MeetingId { get; }

        public string Reason { get; }
    }
}