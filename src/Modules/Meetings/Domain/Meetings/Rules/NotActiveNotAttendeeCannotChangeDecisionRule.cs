using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class NotActiveNotAttendeeCannotChangeDecisionRule : IBusinessRule
    {
        private readonly List<MeetingNotAttendee> _notAttendees;

        private readonly Guid _memberId;

        internal NotActiveNotAttendeeCannotChangeDecisionRule(List<MeetingNotAttendee> notAttendees, Guid memberId)
        {
            _notAttendees = notAttendees;
            _memberId = memberId;
        }

        public bool IsBroken() => _notAttendees.SingleOrDefault(x => x.IsActiveNotAttendee(_memberId)) == null;

        public string Message => "Member is not active not attendee";
    }
}