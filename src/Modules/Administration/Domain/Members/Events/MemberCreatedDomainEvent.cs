
using DomainPack.DomainEvents;

using System;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.Members.Events
{
    public class MemberCreatedDomainEvent : DomainEventBase
    {
        public MemberCreatedDomainEvent(Guid memberId)
        {
            MemberId = memberId;
        }

        public Guid MemberId { get; }
    }
}