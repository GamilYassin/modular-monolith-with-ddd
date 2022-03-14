using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;


namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members
{
    public class MembersQueryHelper
    {
        public static async Task<MemberDto> GetMember(Guid memberId, IDbConnection connection)
        {
            return await connection.QuerySingleAsync<MemberDto>(
                "SELECT " +
                "[Member].Id, " +
                "[Member].[Name], " +
                "[Member].[Login], " +
                "[Member].[Email] " +
                "FROM [meetings].[v_Members] AS [Member] " +
                "WHERE [Member].[Id] = @Id", new
                {
                    Id = memberId
                });
        }

        public static async Task<MeetingGroupMemberData> GetMeetingGroupMember(Guid memberId, Guid meetingOfGroupId, IDbConnection connection)
        {
            var result = await connection.QuerySingleAsync<MeetingGroupMemberResponse>(
                "SELECT " +
                $"[MeetingGroupMember].{nameof(MeetingGroupMemberResponse.MeetingGroupId)}, " +
                $"[MeetingGroupMember].{nameof(MeetingGroupMemberResponse.MemberId)} " +
                "FROM [meetings].[v_MeetingGroupMembers] AS [MeetingGroupMember] " +
                "INNER JOIN [meetings].[Meetings] AS [Meeting] ON [Meeting].[MeetingGroupId] = [MeetingGroupMember].[MeetingGroupId] " +
                "WHERE [MeetingGroupMember].[MemberId] = @MemberId AND [Meeting].[Id] = @MeetingId",
                new
                {
                    MemberId = memberId,
                    MeetingId = meetingOfGroupId
                });

            return new MeetingGroupMemberData(
                result.MeetingGroupId,
                result.MemberId);
        }

        private class MeetingGroupMemberResponse
        {
            public Guid MeetingGroupId { get; set; }

            public Guid MemberId { get; set; }
        }
    }
}