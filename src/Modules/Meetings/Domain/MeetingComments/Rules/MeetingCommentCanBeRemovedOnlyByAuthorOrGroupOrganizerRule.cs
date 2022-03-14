
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class MeetingCommentCanBeRemovedOnlyByAuthorOrGroupOrganizerRule : IBusinessRule
    {
        private readonly MeetingGroup _meetingGroup;
        private readonly Guid _authorId;
        private readonly Guid _removingMemberId;

        public MeetingCommentCanBeRemovedOnlyByAuthorOrGroupOrganizerRule(MeetingGroup meetingGroup, Guid authorId, Guid removingMemberId)
        {
            _meetingGroup = meetingGroup;
            _authorId = authorId;
            _removingMemberId = removingMemberId;
        }

        public bool IsBroken() => _removingMemberId != _authorId && !_meetingGroup.IsOrganizer(_removingMemberId);

        public string Message => "Only author of comment or group organizer can remove meeting comment.";
    }
}