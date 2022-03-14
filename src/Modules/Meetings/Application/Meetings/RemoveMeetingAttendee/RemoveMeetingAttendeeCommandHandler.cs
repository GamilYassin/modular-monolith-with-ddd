using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;



namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.RemoveMeetingAttendee
{
    internal class RemoveMeetingAttendeeCommandHandler : ICommandHandler<RemoveMeetingAttendeeCommand>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMemberContext _memberContext;

        internal RemoveMeetingAttendeeCommandHandler(IMeetingRepository meetingRepository, IMemberContext memberContext)
        {
            _meetingRepository = meetingRepository;
            _memberContext = memberContext;
        }

        public async Task<Unit> Handle(RemoveMeetingAttendeeCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(request.MeetingId);

            meeting.RemoveAttendee(request.AttendeeId, _memberContext.MemberId, request.RemovingReason);

            return Unit.Value;
        }
    }
}