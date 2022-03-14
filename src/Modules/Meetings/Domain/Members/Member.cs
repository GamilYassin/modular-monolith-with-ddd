using CompanyName.MyMeetings.Modules.Meetings.Domain.Members.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

using DomainPack.Contracts.EntitiesContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members
{
    public class Member : EntityObjectBase<Guid>, IAggregateRoot
    {
        private string _login;

        private string _email;

        private string _firstName;

        private string _lastName;

        private string _name;

        private DateTime _createDate;

        public static Member Create(Guid id, string login, string email, string firstName, string lastName, string name)
        {
            return new Member(id, login, email, firstName, lastName, name);
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }

        private Member(Guid id, string login, string email, string firstName, string lastName, string name)
            : base(id)
        {
            _login = login;
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _name = name;
            _createDate = SystemClock.Now;

            this.AddDomainEvent(new MemberCreatedDomainEvent(this.Id));
        }
    }
}