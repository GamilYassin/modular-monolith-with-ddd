using Autofac;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents;
using CompanyName.MyMeetings.Modules.UserAccess.IntegrationEvents;
using DomainPack.DomainEvents.EventBus;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.EventsBus
{
    internal static class EventsBusStartup
    {
        internal static void Initialize(
            ILogger logger)
        {
            SubscribeToIntegrationEvents(logger);
        }

        private static void SubscribeToIntegrationEvents(ILogger logger)
        {
            var eventBus = AdministrationCompositionRoot.BeginLifetimeScope().Resolve<IEventsBus>();

            SubscribeToIntegrationEvent<MeetingGroupProposedIntegrationEvent>(eventBus, logger);
            SubscribeToIntegrationEvent<NewUserRegisteredIntegrationEvent>(eventBus, logger);
        }

        private static void SubscribeToIntegrationEvent<T>(IEventsBus eventBus, ILogger logger)
            where T : IntegrationEvent
        {
            logger.Information("Subscribe to {@IntegrationEvent}", typeof(T).FullName);
            eventBus.Subscribe(
                new IntegrationEventGenericHandler<T>());
        }
    }
}