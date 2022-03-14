using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.SetMeetingGroupExpirationDate
{
    public class SetMeetingGroupExpirationDateCommand : InternalCommandBase
    {
        public SetMeetingGroupExpirationDateCommand(Guid id, Guid meetingGroupId, DateTime dateTo)
            : base(id)
        {
            MeetingGroupId = meetingGroupId;
            DateTo = dateTo;
        }

        internal Guid MeetingGroupId { get; }

        internal DateTime DateTo { get; }
    }
}