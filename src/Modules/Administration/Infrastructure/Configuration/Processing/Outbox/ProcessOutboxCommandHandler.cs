﻿using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;

using Dapper;

using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Outbox
{
    internal class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand, Unit>
    {
        private readonly IMediator _mediator;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IDomainNotificationsMapper _domainNotificationsMapper;

        public ProcessOutboxCommandHandler(
            IMediator mediator,
            ISqlConnectionFactory sqlConnectionFactory,
            IDomainNotificationsMapper domainNotificationsMapper)
        {
            _mediator = mediator;
            _sqlConnectionFactory = sqlConnectionFactory;
            _domainNotificationsMapper = domainNotificationsMapper;
        }

        public async Task<Unit> Handle(ProcessOutboxCommand command, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                               $"[OutboxMessage].[Id] AS [{nameof(OutboxMessageDto.Id)}], " +
                               $"[OutboxMessage].[Type] AS [{nameof(OutboxMessageDto.Type)}], " +
                               $"[OutboxMessage].[Data] AS [{nameof(OutboxMessageDto.Data)}] " +
                               "FROM [administration].[OutboxMessages] AS [OutboxMessage] " +
                               "WHERE [OutboxMessage].[ProcessedDate] IS NULL " +
                               "ORDER BY [OutboxMessage].[OccurredOn]";

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
            var messagesList = messages.AsList();

            const string sqlUpdateProcessedDate = "UPDATE [administration].[OutboxMessages] " +
                                                  "SET [ProcessedDate] = @Date " +
                                                  "WHERE [Id] = @Id";
            if (messagesList.Count > 0)
            {
                foreach (var message in messagesList)
                {
                    var type = _domainNotificationsMapper.GetType(message.Type);
                    var @event = JsonConvert.DeserializeObject(message.Data, type) as IDomainEventNotification;

                    using (LogContext.Push(new OutboxMessageContextEnricher(@event)))
                    {
                        await this._mediator.Publish(@event, cancellationToken);

                        await connection.ExecuteAsync(sqlUpdateProcessedDate, new
                        {
                            Date = DateTime.UtcNow,
                            message.Id
                        });
                    }
                }
            }

            return Unit.Value;
        }

        private class OutboxMessageContextEnricher : ILogEventEnricher
        {
            private readonly IDomainEventNotification _notification;

            public OutboxMessageContextEnricher(IDomainEventNotification notification)
            {
                _notification = notification;
            }

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"OutboxMessage:{_notification.Id.ToString()}")));
            }
        }
    }
}