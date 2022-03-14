using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.PricingStrategies
{
    public class DirectValuePricingStrategy : IPricingStrategy
    {
        private readonly Money _directValue;

        public DirectValuePricingStrategy(Money directValue)
        {
            _directValue = directValue;
        }

        public Money GetPrice(string countryCode, SubscriptionPeriod subscriptionPeriod, PriceListItemCategory category)
        {
            return _directValue;
        }
    }
}