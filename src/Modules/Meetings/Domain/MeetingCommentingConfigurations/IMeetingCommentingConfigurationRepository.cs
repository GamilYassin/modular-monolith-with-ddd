using System.Threading.Tasks;
using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations
{
    public interface IMeetingCommentingConfigurationRepository
    {
        Task AddAsync(MeetingCommentingConfiguration meetingCommentingConfiguration);

        Task<MeetingCommentingConfiguration> GetByMeetingIdAsync(Guid meetingId);
    }
}