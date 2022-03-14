using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class MemberOnWaitlistMustBeAMemberOfGroupRule : IBusinessRule
    {
        private readonly MeetingGroup _meetingGroup;

        private readonly Guid _memberId;

        private readonly List<MeetingAttendee> _attendees;

        internal MemberOnWaitlistMustBeAMemberOfGroupRule(MeetingGroup meetingGroup, Guid memberId, List<MeetingAttendee> attendees)
            : base()
        {
            _meetingGroup = meetingGroup;
            _memberId = memberId;
            _attendees = attendees;
        }

        public bool IsBroken() => !_meetingGroup.IsMemberOfGroup(_memberId);

        public string Message => "Member on waitlist must be a member of group";
    }
}