
using DomainPack.DomainEvents;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events
{
    public class MeetingCommentAddedDomainEvent : DomainEventBase
    {
        public Guid MeetingCommentId { get; }

        public Guid MeetingId { get; }

        public string Comment { get; }

        public MeetingCommentAddedDomainEvent(Guid meetingCommentId, Guid meetingId, string comment)
        {
            MeetingCommentId = meetingCommentId;
            MeetingId = meetingId;
            Comment = comment;
        }
    }
}