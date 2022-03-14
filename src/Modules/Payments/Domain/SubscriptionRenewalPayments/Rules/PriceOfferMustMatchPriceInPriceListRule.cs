using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments.Rules
{
    public class PriceOfferMustMatchPriceInPriceListRule : IBusinessRule
    {
        private readonly Money _priceOffer;

        private readonly Money _priceInPriceList;

        public PriceOfferMustMatchPriceInPriceListRule(
            Money priceOffer,
            Money priceInPriceList)
        {
            _priceOffer = priceOffer;
            _priceInPriceList = priceInPriceList;
        }

        public bool IsBroken() => _priceOffer != _priceInPriceList;

        public string Message => "Price offer must match price in Price List";
    }
}