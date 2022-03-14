using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingCommentLikes;

namespace CompanyName.MyMeetings.Modules.Meetings.Application;

public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();
}

public interface IDbConnection
{
    Task<T> QuerySingleAsync<T>(string v, object p);

    Task ExecuteAsync(string sql, object p);

    Task<T> ExecuteScalarAsync<T>(string v, object p);

    Task<IQueryable<MeetingCommentLikerDto>> QueryAsync<T>(string sql, Guid meetingCommentId);
}