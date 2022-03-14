using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

using System.Collections.Generic;
using System.Linq;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.PricingStrategies
{
    public class DiscountedValueFromPriceListPricingStrategy : IPricingStrategy
    {
        private readonly List<PriceListItemData> _items;

        private readonly Money _discountValue;

        public DiscountedValueFromPriceListPricingStrategy(
            List<PriceListItemData> items,
            Money discountValue)
        {
            _items = items;
            _discountValue = discountValue;
        }

        public Money GetPrice(string countryCode, SubscriptionPeriod subscriptionPeriod, PriceListItemCategory category)
        {
            var priceListItem = _items.Single(x =>
                x.CountryCode == countryCode && x.SubscriptionPeriod == subscriptionPeriod &&
                x.Category == category);

            return priceListItem.Value - _discountValue;
        }
    }
}