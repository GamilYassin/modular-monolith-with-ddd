namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingAttendeeRole : ValueObjectBase
    {
        public static MeetingAttendeeRole Host => new MeetingAttendeeRole("Host");

        public static MeetingAttendeeRole Attendee => new MeetingAttendeeRole("Attendee");

        public string Value { get; }

        private MeetingAttendeeRole(string value)
        {
            this.Value = value;
        }
    }
}