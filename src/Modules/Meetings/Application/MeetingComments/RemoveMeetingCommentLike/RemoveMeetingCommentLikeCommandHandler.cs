﻿using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingCommentLike
{
    internal class RemoveMeetingCommentLikeCommandHandler : ICommandHandler<RemoveMeetingCommentLikeCommand>
    {
        private readonly IMeetingMemberCommentLikesRepository _meetingMemberCommentLikesRepository;
        private readonly IMemberContext _memberContext;

        internal RemoveMeetingCommentLikeCommandHandler(IMeetingMemberCommentLikesRepository meetingMemberCommentLikesRepository, IMemberContext memberContext)
        {
            _meetingMemberCommentLikesRepository = meetingMemberCommentLikesRepository;
            _memberContext = memberContext;
        }

        public async Task<Unit> Handle(RemoveMeetingCommentLikeCommand command, CancellationToken cancellationToken)
        {
            var commentLike = await _meetingMemberCommentLikesRepository.GetAsync(_memberContext.MemberId, command.MeetingCommentId);
            if (commentLike == null)
            {
                throw new InvalidCommandException(new List<string> { "Meeting comment like for removing must exist." });
            }

            commentLike.Remove();

            _meetingMemberCommentLikesRepository.Remove(commentLike);

            return Unit.Value;
        }
    }
}