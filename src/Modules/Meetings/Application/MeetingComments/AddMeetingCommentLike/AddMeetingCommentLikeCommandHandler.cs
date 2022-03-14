using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration;
using CompanyName.MyMeetings.Modules.Meetings.Application.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;



namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentLike
{
    internal class AddMeetingCommentLikeCommandHandler : ICommandHandler<AddMeetingCommentLikeCommand>
    {
        private readonly IMeetingCommentRepository _meetingCommentRepository;
        private readonly IMeetingMemberCommentLikesRepository _meetingMemberCommentLikesRepository;
        private readonly IMemberContext _memberContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public AddMeetingCommentLikeCommandHandler(
            IMeetingCommentRepository meetingCommentRepository,
            IMeetingMemberCommentLikesRepository meetingMemberCommentLikesRepository,
            IMemberContext memberContext,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _meetingCommentRepository = meetingCommentRepository;
            _memberContext = memberContext;
            _sqlConnectionFactory = sqlConnectionFactory;
            _meetingMemberCommentLikesRepository = meetingMemberCommentLikesRepository;
        }

        public async Task<Unit> Handle(AddMeetingCommentLikeCommand request, CancellationToken cancellationToken)
        {
            Domain.MeetingComments.MeetingComment meetingComment = await _meetingCommentRepository.GetByIdAsync(request.MeetingCommentId);
            if (meetingComment == null)
            {
                throw new InvalidCommandException(new List<string> { "To add like the comment must exist." });
            }

            IDbConnection connection = _sqlConnectionFactory.GetOpenConnection();
            var likerMeetingGroupMemberData = await MembersQueryHelper.GetMeetingGroupMember(_memberContext.MemberId, meetingComment.GetMeetingId(), connection);

            int meetingMemeberCommentLikesCount = await _meetingMemberCommentLikesRepository.CountMemberCommentLikesAsync(
                    _memberContext.MemberId,
                    request.MeetingCommentId);

            MeetingMemberCommentLike like = meetingComment.Like(_memberContext.MemberId, likerMeetingGroupMemberData, meetingMemeberCommentLikesCount);

            await _meetingMemberCommentLikesRepository.AddAsync(like);

            return Unit.Value;
        }
    }
}