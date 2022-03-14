namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments
{
    public class SubscriptionPaymentStatus : ValueObjectBase
    {
        public static SubscriptionPaymentStatus WaitingForPayment => new SubscriptionPaymentStatus(nameof(WaitingForPayment));

        public static SubscriptionPaymentStatus Paid => new SubscriptionPaymentStatus(nameof(Paid));

        public static SubscriptionPaymentStatus Expired => new SubscriptionPaymentStatus(nameof(Expired));

        public string Code { get; }

        private SubscriptionPaymentStatus(string code)
        {
            Code = code;
        }

        public static SubscriptionPaymentStatus Of(string code)
        {
            return new SubscriptionPaymentStatus(code);
        }
    }
}