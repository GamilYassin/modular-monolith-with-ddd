using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class MemberCannotBeAnAttendeeOfMeetingMoreThanOnceRule : IBusinessRule
    {
        private readonly Guid _attendeeId;

        private readonly List<MeetingAttendee> _attendees;

        public MemberCannotBeAnAttendeeOfMeetingMoreThanOnceRule(Guid attendeeId, List<MeetingAttendee> attendees)
        {
            this._attendeeId = attendeeId;
            _attendees = attendees;
        }

        public bool IsBroken() => _attendees.SingleOrDefault(x => x.IsActiveAttendee(_attendeeId)) != null;

        public string Message => "Member is already an attendee of this meeting";
    }
}