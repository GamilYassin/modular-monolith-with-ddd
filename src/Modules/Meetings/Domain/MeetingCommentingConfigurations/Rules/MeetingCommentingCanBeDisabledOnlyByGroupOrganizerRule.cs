using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

using DomainPack.Contracts.ValidationContracts;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Rules
{
    public class MeetingCommentingCanBeDisabledOnlyByGroupOrganizerRule : IBusinessRule
    {
        private readonly MeetingGroup _meetingGroup;
        private readonly Guid _disablingMemberId;

        public MeetingCommentingCanBeDisabledOnlyByGroupOrganizerRule(Guid disablingMemberId, MeetingGroup meetingGroup)
        {
            _meetingGroup = meetingGroup;
            _disablingMemberId = disablingMemberId;
        }

        public bool IsBroken() => !_meetingGroup.IsOrganizer(_disablingMemberId);

        public string Message => "Commenting for meeting can be disabled only by group organizer";
    }
}