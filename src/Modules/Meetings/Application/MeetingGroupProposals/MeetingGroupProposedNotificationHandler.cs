﻿using CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents;

using DomainPack.DomainEvents.EventBus;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals
{
    public class MeetingGroupProposedNotificationHandler : INotificationHandler<MeetingGroupProposedNotification>
    {
        private readonly IEventsBus _eventsBus;

        public MeetingGroupProposedNotificationHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public Task Handle(MeetingGroupProposedNotification notification, CancellationToken cancellationToken)
        {
            _eventsBus.Publish(new MeetingGroupProposedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.CreatedOn,
                notification.DomainEvent.MeetingGroupProposalId,
                notification.DomainEvent.Name,
                notification.DomainEvent.Description,
                notification.DomainEvent.LocationCity,
                notification.DomainEvent.LocationCountryCode,
                notification.DomainEvent.ProposalUserId,
                notification.DomainEvent.ProposalDate));

            return Task.CompletedTask;
        }
    }
}