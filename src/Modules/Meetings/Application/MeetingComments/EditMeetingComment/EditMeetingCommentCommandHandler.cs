﻿using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration;



namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.EditMeetingComment
{
    internal class EditMeetingCommentCommandHandler : ICommandHandler<EditMeetingCommentCommand>
    {
        private readonly IMeetingCommentRepository _meetingCommentRepository;
        private readonly IMeetingCommentingConfigurationRepository _meetingCommentingConfigurationRepository;
        private readonly IMemberContext _memberContext;

        internal EditMeetingCommentCommandHandler(
            IMeetingCommentRepository meetingCommentRepository,
            IMeetingCommentingConfigurationRepository meetingCommentingConfigurationRepository,
            IMemberContext memberContext)
        {
            _meetingCommentRepository = meetingCommentRepository;
            _meetingCommentingConfigurationRepository = meetingCommentingConfigurationRepository;
            _memberContext = memberContext;
        }

        public async Task<Unit> Handle(EditMeetingCommentCommand command, CancellationToken cancellationToken)
        {
            var meetingComment = await _meetingCommentRepository.GetByIdAsync(command.MeetingCommentId);
            if (meetingComment == null)
            {
                throw new InvalidCommandException(new List<string> { "Meeting comment for editing must exist." });
            }

            var meetingCommentingConfiguration = await _meetingCommentingConfigurationRepository.GetByMeetingIdAsync(meetingComment.GetMeetingId());

            meetingComment.Edit(_memberContext.MemberId, command.EditedComment, meetingCommentingConfiguration);

            return Unit.Value;
        }
    }
}