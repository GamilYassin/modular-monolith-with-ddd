using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;

using DomainPack.Contracts.MediatorContracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    internal class AcceptMeetingGroupProposalCommandHandler : ICommandHandler<AcceptMeetingGroupProposalCommand>
    {
        private readonly IMeetingGroupProposalRepository _meetingGroupProposalRepository;
        private readonly IUserContext _userContext;

        internal AcceptMeetingGroupProposalCommandHandler(IMeetingGroupProposalRepository meetingGroupProposalRepository, IUserContext userContext)
        {
            _meetingGroupProposalRepository = meetingGroupProposalRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(AcceptMeetingGroupProposalCommand request, CancellationToken cancellationToken)
        {
            MeetingGroupProposal meetingGroupProposal = await _meetingGroupProposalRepository.GetByIdAsync(request.MeetingGroupProposalId);

            meetingGroupProposal.Accept(_userContext.UserId);

            return Unit.Value;
        }
    }
}