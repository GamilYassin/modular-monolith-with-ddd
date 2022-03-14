using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingLimits : ValueObjectBase
    {
        public int? AttendeesLimit { get; }

        public int GuestsLimit { get; }

        private MeetingLimits(int? attendeesLimit, int guestsLimit)
        {
            AttendeesLimit = attendeesLimit;
            GuestsLimit = guestsLimit;
        }

        public static MeetingLimits Create(int? attendeesLimit, int guestsLimit)
        {
            var meetingLimits = new MeetingLimits(attendeesLimit, guestsLimit);
            meetingLimits.CheckRule(new MeetingAttendeesLimitCannotBeNegativeRule(attendeesLimit));

            meetingLimits.CheckRule(new MeetingGuestsLimitCannotBeNegativeRule(guestsLimit));

            meetingLimits.CheckRule(new MeetingAttendeesLimitMustBeGreaterThanGuestsLimitRule(attendeesLimit, guestsLimit));

            return meetingLimits;
        }
    }
}