using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class CommentCanBeLikedOnlyByMeetingGroupMemberRule : IBusinessRule
    {
        private readonly MeetingGroupMemberData _likerMeetingGroupMember;

        public CommentCanBeLikedOnlyByMeetingGroupMemberRule(MeetingGroupMemberData? likerMeetingGroupMember)
        {
            _likerMeetingGroupMember = likerMeetingGroupMember;
        }

        public bool IsBroken() => _likerMeetingGroupMember == null;

        public string Message => "Comment can be liked only by meeting group member.";
    }
}