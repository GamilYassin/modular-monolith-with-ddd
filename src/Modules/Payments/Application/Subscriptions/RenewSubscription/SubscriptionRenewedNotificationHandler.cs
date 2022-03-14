using CompanyName.MyMeetings.Modules.Payments.IntegrationEvents;

using DomainPack.DomainEvents.EventBus;

using System.Threading;
using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.RenewSubscription
{
    public class SubscriptionRenewedNotificationHandler : INotificationHandler<SubscriptionRenewedNotification>
    {
        private readonly IEventsBus _eventsBus;

        public SubscriptionRenewedNotificationHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public Task Handle(SubscriptionRenewedNotification notification, CancellationToken cancellationToken)
        {
            _eventsBus.Publish(new SubscriptionExpirationDateChangedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.PayerId,
                notification.DomainEvent.ExpirationDate));

            return Task.CompletedTask;
        }
    }
}