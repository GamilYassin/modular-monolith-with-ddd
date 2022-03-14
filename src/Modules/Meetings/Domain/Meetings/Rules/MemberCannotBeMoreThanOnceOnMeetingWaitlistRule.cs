﻿using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class MemberCannotBeMoreThanOnceOnMeetingWaitlistRule : IBusinessRule
    {
        private readonly List<MeetingWaitlistMember> _waitListMembers;

        private readonly Guid _memberId;

        internal MemberCannotBeMoreThanOnceOnMeetingWaitlistRule(List<MeetingWaitlistMember> waitListMembers, Guid memberId)
        {
            _waitListMembers = waitListMembers;
            _memberId = memberId;
        }

        public bool IsBroken() => _waitListMembers.SingleOrDefault(x => x.IsActiveOnWaitList(_memberId)) != null;

        public string Message => "Member cannot be more than once on the meeting waitlist";
    }
}