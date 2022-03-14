using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Rules
{
    public class MeetingCommentingCanBeEnabledOnlyByGroupOrganizerRule : IBusinessRule
    {
        private readonly MeetingGroup _meetingGroup;
        private readonly Guid _enablingMemberId;

        public MeetingCommentingCanBeEnabledOnlyByGroupOrganizerRule(Guid enablingMemberId, MeetingGroup meetingGroup)
        {
            _meetingGroup = meetingGroup;
            _enablingMemberId = enablingMemberId;
        }

        public bool IsBroken() => !_meetingGroup.IsOrganizer(_enablingMemberId);

        public string Message => "Commenting for meeting can be enabled only by group organizer";
    }
}