using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration;

using Dapper;

using NSubstitute;

using NUnit.Framework;

using Serilog;

using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.Administration.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString { get; private set; }

        protected ILogger Logger { get; private set; }

        protected IAdministrationModule AdministrationModule { get; private set; }

        protected IEmailSender EmailSender { get; private set; }

        protected ExecutionContextMock ExecutionContext { get; private set; }

        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable =
                "ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString";
            ConnectionString = EnvironmentVariablesProvider.GetVariable(connectionStringEnvironmentVariable);
            if (ConnectionString == null)
            {
                throw new ApplicationException(
                    $"Define connection string to integration tests database using environment variable: {connectionStringEnvironmentVariable}");
            }

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                await ClearDatabase(sqlConnection);
            }

            Logger = Substitute.For<ILogger>();
            EmailSender = Substitute.For<IEmailSender>();
            ExecutionContext = new ExecutionContextMock(Guid.NewGuid());

            AdministrationStartup.Initialize(
                ConnectionString,
                ExecutionContext,
                Logger,
                null);

            AdministrationModule = new AdministrationModule();
        }

        protected async Task<T> GetLastOutboxMessage<T>()
            where T : class, INotification
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var messages = await OutboxMessagesHelper.GetOutboxMessages(connection);

                return OutboxMessagesHelper.Deserialize<T>(messages.Last());
            }
        }

        protected static void AssertBrokenRule<TRule>(AsyncTestDelegate testDelegate)
            where TRule : class, IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var businessRuleValidationException = Assert.CatchAsync<BusinessRuleValidationException>(testDelegate, message);
            if (businessRuleValidationException != null)
            {
                Assert.That(businessRuleValidationException.BrokenRule, Is.TypeOf<TRule>(), message);
            }
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM [administration].[InboxMessages] " +
                               "DELETE FROM [administration].[InternalCommands] " +
                               "DELETE FROM [administration].[OutboxMessages] " +
                               "DELETE FROM [administration].[MeetingGroupProposals] " +
                               "DELETE FROM [administration].[Members] ";

            await connection.ExecuteScalarAsync(sql);
        }
    }
}