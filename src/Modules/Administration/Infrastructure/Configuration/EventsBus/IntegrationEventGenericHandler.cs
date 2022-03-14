using Autofac;

using DomainPack.DomainEvents.EventBus;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.EventsBus
{
    internal class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IntegrationEvent
    {
        public async Task Handle(T @event)
        {
            using ILifetimeScope scope = AdministrationCompositionRoot.BeginLifetimeScope();
            //using var connection = scope.Resolve<ISqlConnectionFactory>().GetOpenConnection();

            //string type = @event.GetType().FullName;
            //string data = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            //{
            //    ContractResolver = new AllPropertiesContractResolver()
            //});

            //string sql = "INSERT INTO [administration].[InboxMessages] (Id, OccurredOn, Type, Data) " +
            //          "VALUES (@Id, @OccurredOn, @Type, @Data)";

            //await connection.ExecuteScalarAsync(sql, new
            //{
            //    @event.Id,
            //    @event.OccurredOn,
            //    type,
            //    data
            //});

            throw new NotImplementedException();
        }
    }
}