using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;



namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.AddMeetingNotAttendee
{
    internal class AddMeetingNotAttendeeCommandHandler : ICommandHandler<AddMeetingNotAttendeeCommand>
    {
        private readonly IMemberContext _memberContext;
        private readonly IMeetingRepository _meetingRepository;

        public AddMeetingNotAttendeeCommandHandler(IMemberContext memberContext, IMeetingRepository meetingRepository)
        {
            _memberContext = memberContext;
            _meetingRepository = meetingRepository;
        }

        public async Task<Unit> Handle(AddMeetingNotAttendeeCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(request.MeetingId);

            meeting.AddNotAttendee(_memberContext.MemberId);

            return Unit.Value;
        }
    }
}