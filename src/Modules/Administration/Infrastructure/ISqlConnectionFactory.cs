namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure;

public interface ISqlConnectionFactory
{
    object GetOpenConnection();
}