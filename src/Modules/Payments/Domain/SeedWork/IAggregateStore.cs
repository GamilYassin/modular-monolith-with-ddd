using DomainPack.Contracts.EntitiesContracts;
using DomainPack.Contracts.EventsContracts;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork
{
    public interface IAggregateStore
    {
        Task Save();

        Task<T> Load<T>(Guid aggregateId)
            where T : IAggregateRoot;

        List<IDomainEvent> GetChanges();

        void AppendChanges<T>(T aggregate)
            where T : IAggregateRoot;

        void ClearChanges();
    }
}