using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class RemovingReasonCanBeProvidedOnlyByGroupOrganizerRule : IBusinessRule
    {
        private readonly MeetingGroup _meetingGroup;
        private readonly Guid _removingMemberId;
        private readonly string _removingReason;

        public RemovingReasonCanBeProvidedOnlyByGroupOrganizerRule(MeetingGroup meetingGroup, Guid removingMemberId, string removingReason)
        {
            _meetingGroup = meetingGroup;
            _removingMemberId = removingMemberId;
            _removingReason = removingReason;
        }

        public bool IsBroken() =>
            !string.IsNullOrEmpty(_removingReason) && !_meetingGroup.IsOrganizer(_removingMemberId);

        public string Message => "Only group organizer can provide comment's removing reason.";
    }
}