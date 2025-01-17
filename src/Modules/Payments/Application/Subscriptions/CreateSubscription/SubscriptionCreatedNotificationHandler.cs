﻿using CompanyName.MyMeetings.Modules.Payments.IntegrationEvents;

using DomainPack.DomainEvents.EventBus;

using System.Threading;
using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.CreateSubscription
{
    public class SubscriptionCreatedNotificationHandler : INotificationHandler<SubscriptionCreatedNotification>
    {
        private readonly IEventsBus _eventsBus;

        public SubscriptionCreatedNotificationHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public Task Handle(SubscriptionCreatedNotification notification, CancellationToken cancellationToken)
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