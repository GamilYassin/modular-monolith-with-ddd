using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class MeetingAttendeeMustBeAMemberOfGroupRule : IBusinessRule
    {
        private readonly MeetingGroup _meetingGroup;

        private readonly Guid _attendeeId;

        internal MeetingAttendeeMustBeAMemberOfGroupRule(Guid attendeeId, MeetingGroup meetingGroup)
        {
            _attendeeId = attendeeId;
            _meetingGroup = meetingGroup;
        }

        public bool IsBroken()
        {
            return !_meetingGroup.IsMemberOfGroup(_attendeeId);
        }

        public string Message => "Meeting attendee must be a member of group";
    }
}