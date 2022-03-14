using CompanyName.MyMeetings.Modules.Payments.Domain.Payers.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

using DomainPack.Contracts.EntitiesContracts;
using DomainPack.Contracts.EventsContracts;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Payers
{
    public class Payer : EntityObjectBase<Guid>, IAggregateRoot
    {
        protected void Apply(IDomainEvent @event)
        {
            this.When((dynamic)@event);
        }

        private string _login;

        private string _email;

        private string _firstName;

        private string _lastName;

        private string _name;

        private DateTime _createDate;

        public static Payer Create(
            Guid id,
            string login,
            string email,
            string firstName,
            string lastName,
            string name)
        {
            var payer = new Payer();

            var payerCreated = new PayerCreatedDomainEvent(
                id,
                login,
                firstName,
                lastName,
                name,
                email);

            payer.Apply(payerCreated);
            payer.AddDomainEvent(payerCreated);

            return payer;
        }

        private void When(PayerCreatedDomainEvent @event)
        {
            this.Id = @event.PayerId;
            _login = @event.Login;
            _createDate = @event.CreatedOn;
            _email = @event.Email;
            _firstName = @event.FirstName;
            _lastName = @event.LastName;
            _name = @event.Name;
        }
    }
}