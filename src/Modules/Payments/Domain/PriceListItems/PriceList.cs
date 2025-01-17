﻿using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.PricingStrategies;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Rules;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

using System.Collections.Generic;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems
{
    public class PriceList : ValueObjectBase
    {
        private readonly List<PriceListItemData> _items;

        private readonly IPricingStrategy _pricingStrategy;

        private PriceList(
            List<PriceListItemData> items,
            IPricingStrategy pricingStrategy)
        {
            _items = items;
            _pricingStrategy = pricingStrategy;
        }

        public static PriceList Create(
            List<PriceListItemData> items,
            IPricingStrategy pricingStrategy)
        {
            return new PriceList(items, pricingStrategy);
        }

        public Money GetPrice(
            string countryCode,
            SubscriptionPeriod subscriptionPeriod,
            PriceListItemCategory category)
        {
            CheckRule(new PriceForSubscriptionMustBeDefinedRule(countryCode, subscriptionPeriod, _items, category));

            return _pricingStrategy.GetPrice(countryCode, subscriptionPeriod, category);
        }
    }
}