using System;


namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions.Events
{
    public class MemberSubscriptionExpirationDateChangedDomainEvent : DomainEventBase
    {
        public MemberSubscriptionExpirationDateChangedDomainEvent(Guid memberId, DateTime expirationDate)
        {
            MemberId = memberId;
            ExpirationDate = expirationDate;
        }

        public Guid MemberId { get; }

        public DateTime ExpirationDate { get; }
    }
}