using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);

        Task EnqueueAsync<T>(ICommand<T> command);
    }
}