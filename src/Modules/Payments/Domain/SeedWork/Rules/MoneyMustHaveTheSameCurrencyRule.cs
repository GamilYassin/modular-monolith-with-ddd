namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork.Rules
{
    public class MoneyMustHaveTheSameCurrencyRule : IBusinessRule
    {
        private readonly Money _left;

        private readonly Money _right;

        public MoneyMustHaveTheSameCurrencyRule(Money left, Money right)
        {
            _left = left;
            _right = right;
        }

        public bool IsBroken() => _left.Currency != _right.Currency;

        public string Message => "Currency of money must be the same.";
    }
}