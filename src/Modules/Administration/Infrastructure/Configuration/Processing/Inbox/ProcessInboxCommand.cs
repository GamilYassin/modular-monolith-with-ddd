using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

using DomainPack.Contracts.MediatorContracts;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Inbox
{
    public class ProcessInboxCommand : CommandBase<Unit>, IRecurringCommand
    {
    }
}