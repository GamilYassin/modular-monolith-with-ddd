namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members
{
    public class MeetingGroupMemberData
    {
        public Guid MeetingGroupId { get; }

        public Guid MemberId { get; }

        public MeetingGroupMemberData(Guid meetingGroupId, Guid memberId)
        {
            MemberId = memberId;
            MeetingGroupId = meetingGroupId;
        }
    }
}