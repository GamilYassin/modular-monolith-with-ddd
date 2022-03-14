using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class NotActiveMemberOfWaitlistCannotBeSignedOffRule : IBusinessRule
    {
        private readonly List<MeetingWaitlistMember> _waitlistMembers;

        private readonly Guid _memberId;

        public NotActiveMemberOfWaitlistCannotBeSignedOffRule(List<MeetingWaitlistMember> waitlistMembers, Guid memberId)
        {
            _waitlistMembers = waitlistMembers;
            _memberId = memberId;
        }

        public bool IsBroken() => _waitlistMembers.SingleOrDefault(x => x.IsActiveOnWaitList(_memberId)) == null;

        public string Message => "Not active member of waitlist cannot be signed off";
    }
}