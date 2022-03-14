using CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions.Events;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MemberSubscriptions
{
    public class MemberSubscriptionExpirationDateChangedNotification : DomainNotificationBase<MemberSubscriptionExpirationDateChangedDomainEvent>
    {
        public MemberSubscriptionExpirationDateChangedNotification(MemberSubscriptionExpirationDateChangedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}