
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class CommentCanBeAddedOnlyByMeetingGroupMemberRule : IBusinessRule
    {
        private readonly Guid _authorId;
        private readonly MeetingGroup _meetingGroup;

        public CommentCanBeAddedOnlyByMeetingGroupMemberRule(Guid authorId, MeetingGroup meetingGroup)
        {
            _authorId = authorId;
            _meetingGroup = meetingGroup;
        }

        public bool IsBroken() => !_meetingGroup.IsMemberOfGroup(_authorId);

        public string Message => "Only meeting attendee can add comments";
    }
}