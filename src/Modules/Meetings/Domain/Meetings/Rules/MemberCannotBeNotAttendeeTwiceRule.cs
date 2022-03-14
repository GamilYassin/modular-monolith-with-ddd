﻿using System;
using System.Collections.Generic;
using System.Linq;

using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class MemberCannotBeNotAttendeeTwiceRule : IBusinessRule
    {
        private readonly List<MeetingNotAttendee> _notAttendees;

        private readonly Guid _memberId;

        public MemberCannotBeNotAttendeeTwiceRule(List<MeetingNotAttendee> notAttendees, Guid memberId)
        {
            _notAttendees = notAttendees;
            _memberId = memberId;
        }

        public bool IsBroken() => _notAttendees.SingleOrDefault(x => x.IsActiveNotAttendee(_memberId)) != null;

        public string Message => "Member cannot be active not attendee twice";
    }
}