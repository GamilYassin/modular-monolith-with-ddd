using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events;

using DomainPack.Contracts.EntitiesContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes
{
    public class MeetingMemberCommentLike : EntityObjectBase<Guid>, IAggregateRoot
    {
        private Guid _meetingCommentId;

        private Guid _memberId;

        private MeetingMemberCommentLike(Guid meetingCommentId, Guid memberId) : base(Guid.NewGuid())
        {
            _meetingCommentId = meetingCommentId;
            _memberId = memberId;

            this.AddDomainEvent(new MeetingCommentLikedDomainEvent(meetingCommentId, memberId));
        }

        public void Remove()
        {
            this.AddDomainEvent(new MeetingCommentUnlikedDomainEvent(_meetingCommentId, _memberId));
        }

        public static MeetingMemberCommentLike Create(Guid meetingCommentId, Guid memberId)
            => new MeetingMemberCommentLike(meetingCommentId, memberId);

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}