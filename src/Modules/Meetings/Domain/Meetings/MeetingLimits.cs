using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

using DomainPack.Entities;

using System.Collections.Generic;

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
            CheckRule(new MeetingAttendeesLimitCannotBeNegativeRule(attendeesLimit));

            CheckRule(new MeetingGuestsLimitCannotBeNegativeRule(guestsLimit));

            CheckRule(new MeetingAttendeesLimitMustBeGreaterThanGuestsLimitRule(attendeesLimit, guestsLimit));

            return new MeetingLimits(attendeesLimit, guestsLimit);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}