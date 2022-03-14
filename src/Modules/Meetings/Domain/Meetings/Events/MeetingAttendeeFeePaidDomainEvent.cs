namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingAttendeeFeePaidDomainEvent : DomainEventBase
    {
        public MeetingAttendeeFeePaidDomainEvent(Guid meetingId, Guid attendeeId)
        {
            MeetingId = meetingId;
            AttendeeId = attendeeId;
        }

        public Guid MeetingId { get; }

        public Guid AttendeeId { get; }
    }
}