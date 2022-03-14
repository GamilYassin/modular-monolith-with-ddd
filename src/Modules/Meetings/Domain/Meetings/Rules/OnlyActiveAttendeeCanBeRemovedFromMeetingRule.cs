using DomainPack.Contracts.ValidationContracts;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class OnlyActiveAttendeeCanBeRemovedFromMeetingRule : IBusinessRule
    {
        private readonly List<MeetingAttendee> _attendees;
        private readonly Guid _attendeeId;

        internal OnlyActiveAttendeeCanBeRemovedFromMeetingRule(
            List<MeetingAttendee> attendees,
            Guid attendeeId)
        {
            _attendees = attendees;
            _attendeeId = attendeeId;
        }

        public bool IsBroken()
        {
            return _attendees.SingleOrDefault(x => x.IsActiveAttendee(_attendeeId)) == null;
        }

        public string Message => "Only active attendee can be removed from meeting";
    }
}