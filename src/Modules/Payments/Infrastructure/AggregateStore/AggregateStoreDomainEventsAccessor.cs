using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

using DomainPack.Contracts.EventsContracts;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    public class AggregateStoreDomainEventsAccessor : IDomainEventsAccessor
    {
        private readonly IAggregateStore _aggregateStore;

        public AggregateStoreDomainEventsAccessor(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            return _aggregateStore
                .GetChanges()
                .ToList()
                .AsReadOnly();
        }

        public void ClearAllDomainEvents()
        {
            _aggregateStore.ClearChanges();
        }
    }
}