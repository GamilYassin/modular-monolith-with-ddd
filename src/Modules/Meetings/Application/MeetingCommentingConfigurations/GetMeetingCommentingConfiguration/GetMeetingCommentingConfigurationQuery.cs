using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration
{
    public class GetMeetingCommentingConfigurationQuery : QueryBase<MeetingCommentingConfigurationDto>
    {
        public Guid MeetingId { get; }

        public GetMeetingCommentingConfigurationQuery(Guid meetingId)
        {
            MeetingId = meetingId;
        }
    }
}
