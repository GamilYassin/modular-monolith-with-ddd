using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfiguration.DisbaleMeetingCommentingConfiguration
{
    internal class DisableMeetingCommentingConfigurationCommandHandler : ICommandHandler<DisableMeetingCommentingConfigurationCommand>
    {
        private readonly IMeetingCommentingConfigurationRepository _meetingCommentingConfigurationRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingGroupRepository _meetingGroupRepository;
        private readonly IMemberContext _memberContext;

        public DisableMeetingCommentingConfigurationCommandHandler(
            IMeetingCommentingConfigurationRepository meetingCommentingConfigurationRepository,
            IMeetingGroupRepository meetingGroupRepository,
            IMeetingRepository meetingRepository,
            IMemberContext memberContext)
        {
            _meetingCommentingConfigurationRepository = meetingCommentingConfigurationRepository;
            _meetingGroupRepository = meetingGroupRepository;
            _meetingRepository = meetingRepository;
            _memberContext = memberContext;
        }

        public async Task<Unit> Handle(DisableMeetingCommentingConfigurationCommand command, CancellationToken cancellationToken)
        {
            var meetingCommentingConfiguration = await _meetingCommentingConfigurationRepository.GetByMeetingIdAsync(Guid.NewGuid());
            if (meetingCommentingConfiguration == null)
            {
                throw new InvalidCommandException(new List<string> { "Meeting commenting configuration for disabling commenting must exist." });
            }

            var meeting = await _meetingRepository.GetByIdAsync(Guid.NewGuid());

            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(meeting.GetMeetingGroupId());

            meetingCommentingConfiguration.DisableCommenting(_memberContext.MemberId, meetingGroup);

            return Unit.Value;
        }
    }
}