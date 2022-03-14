using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;

using System.Reflection;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase<>).Assembly;
    }
}