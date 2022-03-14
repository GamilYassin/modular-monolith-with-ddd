

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members.Events
{
    public class MemberCreatedDomainEvent : DomainEventBase
    {
        public Guid MemberId { get; }

        public MemberCreatedDomainEvent(Guid memberId)
        {
            MemberId = memberId;
        }
    }
}