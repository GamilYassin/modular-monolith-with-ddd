using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;

using System.Reflection;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase).Assembly;
    }
}