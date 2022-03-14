using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    internal class OnlyMeetingAttendeeCanHaveChangedRoleRule : IBusinessRule
    {
        private readonly List<MeetingAttendee> _attendees;

        private readonly Guid _newOrganizerId;

        internal OnlyMeetingAttendeeCanHaveChangedRoleRule(List<MeetingAttendee> attendees, Guid newOrganizerId)
        {
            _attendees = attendees;
            _newOrganizerId = newOrganizerId;
        }

        public bool IsBroken() => _attendees.SingleOrDefault(x => x.IsActiveAttendee(_newOrganizerId)) == null;

        public string Message => "Only meeting attendee can be se as organizer of meeting";
    }
}