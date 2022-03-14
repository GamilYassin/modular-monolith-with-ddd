namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events
{
    public class MeetingCommentEditedDomainEvent : DomainEventBase
    {
        public Guid MeetingCommentId { get; }

        public string EditedComment { get; }

        public MeetingCommentEditedDomainEvent(Guid meetingCommentId, string editedComment)
        {
            MeetingCommentId = meetingCommentId;
            EditedComment = editedComment;
        }
    }
}