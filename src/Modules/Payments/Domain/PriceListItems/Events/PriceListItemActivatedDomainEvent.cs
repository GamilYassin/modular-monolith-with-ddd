using System;


namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.Events
{
    public class PriceListItemActivatedDomainEvent : DomainEventBase
    {
        public PriceListItemActivatedDomainEvent(Guid priceListItemId)
        {
            PriceListItemId = priceListItemId;
        }

        public Guid PriceListItemId { get; }
    }
}