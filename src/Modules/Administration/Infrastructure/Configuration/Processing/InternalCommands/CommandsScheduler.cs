using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

using DomainPack.Contracts.CQSContracts;

using ICommandsScheduler = DomainPack.Contracts.CQSContracts.ICommandsScheduler;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.InternalCommands
{
    public class CommandsScheduler : ICommandsScheduler
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IInternalCommandsMapper _internalCommandsMapper;

        public CommandsScheduler(
            ISqlConnectionFactory sqlConnectionFactory,
            IInternalCommandsMapper internalCommandsMapper)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _internalCommandsMapper = internalCommandsMapper;
        }

        public async Task EnqueueAsync(ICommand command)
        {
            object connection = _sqlConnectionFactory.GetOpenConnection();

            //const string sqlInsert = "INSERT INTO [administration].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
            //                         "(@Id, @EnqueueDate, @Type, @Data)";

            //await connection.ExecuteAsync(sqlInsert, new
            //{
            //    command.Id,
            //    EnqueueDate = DateTime.UtcNow,
            //    Type = _internalCommandsMapper.GetName(command.GetType()),
            //    Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
            //    {
            //        ContractResolver = new AllPropertiesContractResolver()
            //    })
            //});
        }

        public async Task EnqueueAsync<T>(ICommand<T> command)
        {
            object connection = _sqlConnectionFactory.GetOpenConnection();

            //const string sqlInsert = "INSERT INTO [administration].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
            //                         "(@Id, @EnqueueDate, @Type, @Data)";

            //await connection.ExecuteAsync(sqlInsert, new
            //{
            //    command.Id,
            //    EnqueueDate = DateTime.UtcNow,
            //    Type = _internalCommandsMapper.GetName(command.GetType()),
            //    Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
            //    {
            //        ContractResolver = new AllPropertiesContractResolver()
            //    })
            //});
        }

        public Task EnqueueAsync<T>(IDomainCommand<T> command)
        {
            throw new NotImplementedException();
        }
    }
}