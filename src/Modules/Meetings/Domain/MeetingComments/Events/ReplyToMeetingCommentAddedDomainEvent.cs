namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events
{
    public class ReplyToMeetingCommentAddedDomainEvent : DomainEventBase
    {
        public Guid MeetingCommentId { get; }

        public Guid InReplyToCommentId { get; }

        public string Reply { get; }

        public ReplyToMeetingCommentAddedDomainEvent(Guid meetingCommentId, Guid inReplyToCommentId, string reply)
        {
            MeetingCommentId = meetingCommentId;
            InReplyToCommentId = inReplyToCommentId;
            Reply = reply;
        }
    }
}