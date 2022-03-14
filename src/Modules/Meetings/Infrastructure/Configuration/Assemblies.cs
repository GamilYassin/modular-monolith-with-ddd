using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;

using System.Reflection;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase).Assembly;
    }
}