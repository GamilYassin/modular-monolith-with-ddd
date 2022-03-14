using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;

using System.Reflection;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase).Assembly;
    }
}