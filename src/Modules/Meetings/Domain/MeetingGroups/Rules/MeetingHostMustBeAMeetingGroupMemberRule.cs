using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules
{
    public class MeetingHostMustBeAMeetingGroupMemberRule : IBusinessRule
    {
        private readonly Guid _creatorId;

        private readonly List<Guid> _hostsMembersIds;

        private readonly List<MeetingGroupMember> _members;

        public MeetingHostMustBeAMeetingGroupMemberRule(
            Guid creatorId,
            List<Guid> hostsMembersIds,
            List<MeetingGroupMember> members)
        {
            _creatorId = creatorId;
            _hostsMembersIds = hostsMembersIds;
            _members = members;
        }

        public bool IsBroken()
        {
            var memberIds = _members.Select(x => x.MemberId).ToList();
            if (!_hostsMembersIds.Any() && !memberIds.Contains(_creatorId))
            {
                return true;
            }

            return _hostsMembersIds.Any() && _hostsMembersIds.Except(memberIds).Any();
        }

        public string Message => "Meeting host must be a meeting group member";
    }
}