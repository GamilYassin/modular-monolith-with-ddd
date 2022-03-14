using CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions.Events;

using DomainPack.Contracts.EntitiesContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions
{
    public class MemberSubscription : EntityObjectBase<Guid>, IAggregateRoot
    {
        private DateTime _expirationDate;

        private MemberSubscription(Guid memberId, DateTime expirationDate)
            : base(memberId)
        {
            _expirationDate = expirationDate;

            this.AddDomainEvent(new MemberSubscriptionExpirationDateChangedDomainEvent(memberId, _expirationDate));
        }

        public static MemberSubscription CreateForMember(Guid memberId, DateTime expirationDate)
        {
            return new MemberSubscription(memberId, expirationDate);
        }

        public void ChangeExpirationDate(DateTime expirationDate)
        {
            _expirationDate = expirationDate;

            this.AddDomainEvent(new MemberSubscriptionExpirationDateChangedDomainEvent(
                this.Id,
                _expirationDate));
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}