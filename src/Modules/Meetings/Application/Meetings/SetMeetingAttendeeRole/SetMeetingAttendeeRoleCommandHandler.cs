﻿using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingAttendeeRole
{
    internal class SetMeetingAttendeeRoleCommandHandler : ICommandHandler<SetMeetingAttendeeRoleCommand>
    {
        private readonly IMemberContext _memberContext;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingGroupRepository _meetingGroupRepository;

        internal SetMeetingAttendeeRoleCommandHandler(
            IMemberContext memberContext,
            IMeetingRepository meetingRepository,
            IMeetingGroupRepository meetingGroupRepository)
        {
            _memberContext = memberContext;
            _meetingRepository = meetingRepository;
            _meetingGroupRepository = meetingGroupRepository;
        }

        public async Task<Unit> Handle(SetMeetingAttendeeRoleCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(request.MeetingId);

            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(meeting.GetMeetingGroupId());

            meeting.SetAttendeeRole(meetingGroup, _memberContext.MemberId, request.MemberId);

            return Unit.Value;
        }
    }
}