using System;
using CompanyName.MyMeetings.Modules.Administration.Domain.Members.Events;

using DomainPack.Contracts.EntitiesContracts;
using DomainPack.Entities;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.Members
{
    public class Member : EntityObjectBase<Guid>, IAggregateRoot
    {

        private string _login;

        private string _email;

        private string _firstName;

        private string _lastName;

        private string _name;

        private DateTime _createDate;


        private Member(Guid id, string login, string email, string firstName, string lastName, string name): base(id)
        {
            _login = login;
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _name = name;
            _createDate = DateTime.UtcNow;

            this.AddDomainEvent(new MemberCreatedDomainEvent(this.Id));
        }

        public static Member Create(Guid id, string login, string email, string firstName, string lastName, string name)
        {
            return new Member(id, login, email, firstName, lastName, name);
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}