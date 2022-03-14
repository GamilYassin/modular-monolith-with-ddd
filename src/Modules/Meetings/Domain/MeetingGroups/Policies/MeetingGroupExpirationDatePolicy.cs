namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Policies
{
    public static class MeetingGroupExpirationDatePolicy
    {
        public static List<Guid> GetMeetingGroupsCoveredByMemberSubscription(
            List<MeetingGroupMemberData> meetingGroups)
        {
            return meetingGroups
                .Where(x => x.Role == MeetingGroupMemberRole.Organizer)
                .Select(x => x.MeetingGroupId)
                .ToList();
        }
    }
}