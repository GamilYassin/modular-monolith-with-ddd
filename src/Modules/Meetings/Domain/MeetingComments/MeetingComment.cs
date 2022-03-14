using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

using DomainPack.Contracts.EntitiesContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments
{
    public class MeetingComment : EntityObjectBase<Guid>, IAggregateRoot
    {
        private Guid _meetingId;

        private Guid _authorId;

        private Guid? _inReplyToCommentId;

        private string _comment;

        private DateTime _createDate;

        private DateTime? _editDate;

        private bool _isRemoved;

        private string _removedByReason;

        private MeetingComment(
            Guid meetingId,
            Guid authorId,
            string comment,
            Guid inReplyToCommentId,
            MeetingCommentingConfiguration meetingCommentingConfiguration,
            MeetingGroup meetingGroup)
            : base(Guid.NewGuid())
        {
            this.CheckRule(new CommentTextMustBeProvidedRule(comment));
            this.CheckRule(new CommentCanBeCreatedOnlyIfCommentingForMeetingEnabledRule(meetingCommentingConfiguration));
            this.CheckRule(new CommentCanBeAddedOnlyByMeetingGroupMemberRule(authorId, meetingGroup));

            _meetingId = meetingId;
            _authorId = authorId;
            _comment = comment;

            _inReplyToCommentId = inReplyToCommentId;

            _createDate = SystemClock.Now;
            _editDate = null;

            _isRemoved = false;
            _removedByReason = null;

            if (inReplyToCommentId == null)
            {
                this.AddDomainEvent(new MeetingCommentAddedDomainEvent(this.Id, _meetingId, comment));
            }
            else
            {
                this.AddDomainEvent(new ReplyToMeetingCommentAddedDomainEvent(this.Id, inReplyToCommentId, comment));
            }
        }

        public void Edit(Guid editorId, string editedComment, MeetingCommentingConfiguration meetingCommentingConfiguration)
        {
            this.CheckRule(new CommentTextMustBeProvidedRule(editedComment));
            this.CheckRule(new MeetingCommentCanBeEditedOnlyByAuthorRule(this._authorId, editorId));
            this.CheckRule(new CommentCanBeEditedOnlyIfCommentingForMeetingEnabledRule(meetingCommentingConfiguration));

            _comment = editedComment;
            _editDate = SystemClock.Now;

            this.AddDomainEvent(new MeetingCommentEditedDomainEvent(this.Id, editedComment));
        }

        public void Remove(Guid removingMemberId, MeetingGroup meetingGroup, string reason = null)
        {
            this.CheckRule(new MeetingCommentCanBeRemovedOnlyByAuthorOrGroupOrganizerRule(meetingGroup, this._authorId, removingMemberId));
            this.CheckRule(new RemovingReasonCanBeProvidedOnlyByGroupOrganizerRule(meetingGroup, removingMemberId, reason));

            _isRemoved = true;
            _removedByReason = reason ?? string.Empty;

            this.AddDomainEvent(new MeetingCommentRemovedDomainEvent(this.Id));
        }

        public MeetingComment Reply(Guid replierId, string reply, MeetingGroup meetingGroup, MeetingCommentingConfiguration meetingCommentingConfiguration)
            => new MeetingComment(
                _meetingId,
                replierId,
                reply,
                this.Id,
                meetingCommentingConfiguration,
                meetingGroup);

        public MeetingMemberCommentLike Like(
            Guid likerId,
            MeetingGroupMemberData likerMeetingGroupMember,
            int meetingMemberCommentLikesCount)
        {
            this.CheckRule(new CommentCanBeLikedOnlyByMeetingGroupMemberRule(likerMeetingGroupMember));
            this.CheckRule(new CommentCannotBeLikedByTheSameMemberMoreThanOnceRule(meetingMemberCommentLikesCount));

            return MeetingMemberCommentLike.Create(this.Id, likerId);
        }

        public Guid GetMeetingId() => this._meetingId;

        internal static MeetingComment Create(
            Guid meetingId,
            Guid authorId,
            string comment,
            MeetingGroup meetingGroup,
            MeetingCommentingConfiguration meetingCommentingConfiguration)
            => new MeetingComment(
                meetingId,
                authorId,
                comment,
                inReplyToCommentId: Guid.Empty,
                meetingCommentingConfiguration,
                meetingGroup);

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}