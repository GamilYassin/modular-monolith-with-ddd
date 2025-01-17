﻿using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.ChangeMeetingMainAttributes
{
    internal class ChangeMeetingMainAttributesCommandHandler : ICommandHandler<ChangeMeetingMainAttributesCommand>
    {
        private readonly IMemberContext _memberContext;
        private readonly IMeetingRepository _meetingRepository;

        public ChangeMeetingMainAttributesCommandHandler(IMemberContext memberContext, IMeetingRepository meetingRepository)
        {
            _memberContext = memberContext;
            _meetingRepository = meetingRepository;
        }

        public async Task<Unit> Handle(ChangeMeetingMainAttributesCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(request.MeetingId);

            meeting.ChangeMainAttributes(
                request.Title,
                MeetingTerm.CreateNewBetweenDates(request.TermStartDate, request.TermStartDate),
                request.Description,
                MeetingLocation.CreateNew(request.MeetingLocationName, request.MeetingLocationAddress, request.MeetingLocationPostalCode, request.MeetingLocationCity),
                MeetingLimits.Create(request.AttendeesLimit, request.GuestsLimit),
                Term.CreateNewBetweenDates(request.RSVPTermStartDate, request.RSVPTermEndDate),
                request.EventFeeValue.HasValue ? Money.Of(request.EventFeeValue.Value, request.EventFeeCurrency) : Money.Undefined,
                _memberContext.MemberId);

            return Unit.Value;
        }
    }
}