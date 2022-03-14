using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration
{
    public interface IMeetingCommentRepository
    {
        Task AddAsync(MeetingComment meetingComment);

        Task<MeetingComment> GetByIdAsync(Guid meetingCommentId);

        Task<MeetingCommentingConfigurationDto> GetByMeetingIdAsync(Guid meetingCommentId);
    }
}