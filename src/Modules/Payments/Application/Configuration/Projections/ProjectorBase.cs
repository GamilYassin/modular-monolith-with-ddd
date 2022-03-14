using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Projections
{
    internal abstract class ProjectorBase
    {
        protected static Task When(IDomainEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}