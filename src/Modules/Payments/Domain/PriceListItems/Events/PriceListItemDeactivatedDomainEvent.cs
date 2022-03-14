using System;


namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.Events
{
    public class PriceListItemDeactivatedDomainEvent : DomainEventBase
    {
        public PriceListItemDeactivatedDomainEvent(Guid priceListItemId)
        {
            PriceListItemId = priceListItemId;
        }

        public Guid PriceListItemId { get; }
    }
}