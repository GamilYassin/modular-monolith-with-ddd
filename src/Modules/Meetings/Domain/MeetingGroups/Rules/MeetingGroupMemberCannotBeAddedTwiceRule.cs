using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules
{
    public class MeetingGroupMemberCannotBeAddedTwiceRule : IBusinessRule
    {
        private readonly List<MeetingGroupMember> _members;

        private readonly Guid _memberId;

        public MeetingGroupMemberCannotBeAddedTwiceRule(List<MeetingGroupMember> members, Guid memberId)
            : base()
        {
            _members = members;
            _memberId = memberId;
        }

        public bool IsBroken()
        {
            return this._members.SingleOrDefault(x => x.IsMember(_memberId)) != null;
        }

        public string Message => "Member cannot be added twice to the same group";
    }
}