using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeAddedEmail
{
    internal class SendMeetingAttendeeAddedEmailCommand : InternalCommandBase
    {
        internal Guid AttendeeId { get; }

        internal Guid MeetingId { get; }

        internal SendMeetingAttendeeAddedEmailCommand(Guid id, Guid attendeeId, Guid meetingId)
            : base(id)
        {
            AttendeeId = attendeeId;
            MeetingId = meetingId;
        }
    }
}