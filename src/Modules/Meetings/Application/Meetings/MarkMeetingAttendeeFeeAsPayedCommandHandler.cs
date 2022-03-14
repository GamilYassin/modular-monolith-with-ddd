using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;



namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings
{
    internal class MarkMeetingAttendeeFeeAsPayedCommandHandler : ICommandHandler<MarkMeetingAttendeeFeeAsPayedCommand>
    {
        private readonly IMeetingRepository _meetingRepository;

        public MarkMeetingAttendeeFeeAsPayedCommandHandler(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public async Task<Unit> Handle(MarkMeetingAttendeeFeeAsPayedCommand command, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(command.MeetingId);

            meeting.MarkAttendeeFeeAsPayed(command.MemberId);

            return Unit.Value;
        }
    }
}