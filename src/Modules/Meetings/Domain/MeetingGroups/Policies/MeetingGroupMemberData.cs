namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Policies
{
    public class MeetingGroupMemberData
    {
        public MeetingGroupMemberData(Guid meetingGroupId, MeetingGroupMemberRole role)
        {
            MeetingGroupId = meetingGroupId;
            Role = role;
        }

        public Guid MeetingGroupId { get; }

        public MeetingGroupMemberRole Role { get; }
    }
}