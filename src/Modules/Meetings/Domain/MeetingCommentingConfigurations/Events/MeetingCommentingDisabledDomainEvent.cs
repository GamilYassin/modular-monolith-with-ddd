namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events
{
    public class MeetingCommentingDisabledDomainEvent : DomainEventBase
    {
        public Guid MeetingId { get; }

        public MeetingCommentingDisabledDomainEvent(Guid meetingId)
        {
            MeetingId = meetingId;
        }
    }
}