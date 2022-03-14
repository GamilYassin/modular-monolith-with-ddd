using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;

using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);

        Task EnqueueAsync<T>(ICommand<T> command);
    }
}