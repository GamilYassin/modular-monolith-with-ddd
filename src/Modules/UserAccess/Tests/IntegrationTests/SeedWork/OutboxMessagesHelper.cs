﻿using CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;
using CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;

using Dapper;

using MediatR;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests.SeedWork
{
    public class OutboxMessagesHelper
    {
        public static async Task<List<OutboxMessageDto>> GetOutboxMessages(IDbConnection connection)
        {
            const string sql = "SELECT " +
                               "[OutboxMessage].[Id], " +
                               "[OutboxMessage].[Type], " +
                               "[OutboxMessage].[Data] " +
                               "FROM [users].[OutboxMessages] AS [OutboxMessage] " +
                               "ORDER BY [OutboxMessage].[OccurredOn]";

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
            return messages.AsList();
        }

        public static T Deserialize<T>(OutboxMessageDto message)
            where T : class, INotification
        {
            Type type = Assembly.GetAssembly(typeof(NewUserRegisteredNotification)).GetType(typeof(T).FullName);
            return JsonConvert.DeserializeObject(message.Data, type) as T;
        }
    }
}