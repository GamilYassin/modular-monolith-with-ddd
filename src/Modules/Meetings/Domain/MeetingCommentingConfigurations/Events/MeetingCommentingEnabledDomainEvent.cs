namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events
{
    public class MeetingCommentingEnabledDomainEvent : DomainEventBase
    {
        public Guid MeetingId { get; }

        public MeetingCommentingEnabledDomainEvent(Guid meetingId)
        {
            MeetingId = meetingId;
        }
    }
}