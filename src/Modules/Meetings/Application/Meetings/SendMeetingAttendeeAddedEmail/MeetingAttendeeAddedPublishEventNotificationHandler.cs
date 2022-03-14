using CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents;

using DomainPack.DomainEvents.EventBus;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeAddedEmail
{
    internal class MeetingAttendeeAddedPublishEventNotificationHandler : INotificationHandler<MeetingAttendeeAddedNotification>
    {
        private readonly IEventsBus _eventsBus;

        internal MeetingAttendeeAddedPublishEventNotificationHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public Task Handle(MeetingAttendeeAddedNotification notification, CancellationToken cancellationToken)
        {
            _eventsBus.Publish(new MeetingAttendeeAddedIntegrationEvent(
                Guid.NewGuid(),
                notification.DomainEvent.CreatedOn,
                notification.DomainEvent.MeetingId,
                notification.DomainEvent.AttendeeId,
                notification.DomainEvent.FeeValue,
                notification.DomainEvent.FeeCurrency));

            return Task.CompletedTask;
        }
    }
}