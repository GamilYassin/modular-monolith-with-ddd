namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events
{
    public class MeetingCommentRemovedDomainEvent : DomainEventBase
    {
        public Guid MeetingCommentId { get; }

        public MeetingCommentRemovedDomainEvent(Guid meetingCommentId)
        {
            MeetingCommentId = meetingCommentId;
        }
    }
}