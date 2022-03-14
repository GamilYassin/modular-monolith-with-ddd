using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals;

using DomainPack.Contracts.MediatorContracts;
using DomainPack.DomainEvents.EventBus;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    public class MeetingGroupProposalAcceptedNotificationHandler : INotificationHandler<MeetingGroupProposalAcceptedNotification>
    {
        private readonly IEventsBus _eventsBus;

        public MeetingGroupProposalAcceptedNotificationHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public Task Handle(MeetingGroupProposalAcceptedNotification notification, CancellationToken cancellationToken)
        {
            _eventsBus.Publish(new MeetingGroupProposalAcceptedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.CreatedOn,
                notification.DomainEvent.MeetingGroupProposalId));

            return Task.CompletedTask;
        }
    }
}