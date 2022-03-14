namespace CompanyName.MyMeetings.Modules.Administration.Application;
public interface ISqlConnectionFactory
{
    object GetOpenConnection();
}
