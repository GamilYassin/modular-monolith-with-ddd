namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes
{
    public interface IMeetingMemberCommentLikesRepository
    {
        Task AddAsync(MeetingMemberCommentLike meetingMemberCommentLike);

        Task<MeetingMemberCommentLike> GetAsync(Guid memberId, Guid meetingCommentId);

        Task<int> CountMemberCommentLikesAsync(Guid memberId, Guid meetingCommentId);

        void Remove(MeetingMemberCommentLike meetingMemberCommentLike);
    }
}