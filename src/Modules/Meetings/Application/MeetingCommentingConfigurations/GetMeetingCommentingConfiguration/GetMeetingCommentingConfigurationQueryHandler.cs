using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration
{
    internal class GetMeetingCommentingConfigurationQueryHandler : IQueryHandler<GetMeetingCommentingConfigurationQuery, MeetingCommentingConfigurationDto>
    {
        private readonly IMeetingCommentRepository _sqlConnectionFactory;

        public GetMeetingCommentingConfigurationQueryHandler(IMeetingCommentRepository sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MeetingCommentingConfigurationDto> Handle(GetMeetingCommentingConfigurationQuery query, CancellationToken cancellationToken)
        {
            //var connection = _sqlConnectionFactory.GetOpenConnection();

            //string sql = "SELECT " +
            //             $"[MeetingCommentingConfiguration].[MeetingId] AS [{nameof(MeetingCommentingConfigurationDto.MeetingId)}], " +
            //             $"[MeetingCommentingConfiguration].[IsCommentingEnabled] AS [{nameof(MeetingCommentingConfigurationDto.IsCommentingEnabled)}] " +
            //             "FROM [meetings].[MeetingCommentingConfigurations] AS [MeetingCommentingConfiguration] " +
            //             "WHERE [MeetingCommentingConfiguration].[MeetingId] = @MeetingId";

            ////return await connection.QuerySingleOrDefaultAsync<MeetingCommentingConfigurationDto>(sql, new { query.MeetingId });

            //return await _sqlConnectionFactory.GetByMeetingIdAsync()

            throw new NotImplementedException();
        }
    }
}
