

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class Money : ValueObjectBase
    {
        public static Money Undefined => new Money(null, null);

        public decimal? Value { get; }

        public string Currency { get; }

        public static Money Of(decimal value, string currency)
        {
            return new Money(value, currency);
        }

        private Money(decimal? value, string currency)
        {
            this.Value = value;
            this.Currency = currency;
        }

        public static Money operator *(int left, Money right)
        {
            return new Money(right.Value * left, right.Currency);
        }
    }
}