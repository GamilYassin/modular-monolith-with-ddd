using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.SendMeetingGroupCreatedEmail
{
    internal class SendMeetingGroupCreatedEmailCommand : InternalCommandBase
    {
        internal Guid MeetingGroupId { get; }

        internal Guid CreatorId { get; }

        internal SendMeetingGroupCreatedEmailCommand(Guid id, Guid meetingGroupId, Guid creatorId)
            : base(id)
        {
            MeetingGroupId = meetingGroupId;
            CreatorId = creatorId;
        }
    }
}