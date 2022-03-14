
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events
{
    public class MeetingCommentLikedDomainEvent : DomainEventBase
    {
        public Guid MeetingCommentId { get; }

        public Guid LikerId { get; }

        public MeetingCommentLikedDomainEvent(Guid meetingCommentId, Guid likerId)
        {
            MeetingCommentId = meetingCommentId;
            LikerId = likerId;
        }
    }
}