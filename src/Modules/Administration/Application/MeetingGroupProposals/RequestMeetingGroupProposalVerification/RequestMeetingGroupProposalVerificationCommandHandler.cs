using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.RequestMeetingGroupProposalVerification
{
    internal class RequestMeetingGroupProposalVerificationCommandHandler :
        ICommandHandler<RequestMeetingGroupProposalVerificationCommand, Guid>
    {
        private readonly IMeetingGroupProposalRepository _meetingGroupProposalRepository;

        public RequestMeetingGroupProposalVerificationCommandHandler(IMeetingGroupProposalRepository meetingGroupProposalRepository)
        {
            _meetingGroupProposalRepository = meetingGroupProposalRepository;
        }

        public async Task<Guid> Handle(RequestMeetingGroupProposalVerificationCommand request, CancellationToken cancellationToken)
        {
            var meetingGroupProposal = MeetingGroupProposal.CreateToVerify(
                request.MeetingGroupProposalId,
                request.Name,
                request.Description,
                MeetingGroupLocation.Create(request.LocationCity, request.LocationCountryCode),
                request.ProposalUserId,
                request.ProposalDate);

            await _meetingGroupProposalRepository.AddAsync(meetingGroupProposal);

            return meetingGroupProposal.Id;
        }
    }
}