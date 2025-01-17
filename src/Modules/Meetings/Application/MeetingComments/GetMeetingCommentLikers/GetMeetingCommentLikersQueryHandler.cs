﻿using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingCommentLikes
{
    internal class GetMeetingCommentLikersQueryHandler : IQueryHandler<GetMeetingCommentLikersQuery, List<MeetingCommentLikerDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingCommentLikersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingCommentLikerDto>> Handle(GetMeetingCommentLikersQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var sql = "SELECT " +
                      $"[Liker].[{nameof(MeetingCommentLikerDto.Id)}]," +
                      $"[Liker].[{nameof(MeetingCommentLikerDto.Name)}] " +
                      "FROM [meetings].[Members] AS [Liker]" +
                      "INNER JOIN [meetings].[MeetingMemberCommentLikes] AS [Like]" +
                      "ON [Liker].[Id] = [Like].[MemberId]" +
                      "WHERE [Like].[MeetingCommentId] = @MeetingCommentId";

            var meetingCommentLikers = await connection.QueryAsync<MeetingCommentLikerDto>(sql, query.MeetingCommentId);

            return meetingCommentLikers.ToList();
        }
    }
}