﻿using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class OnlyMeetingOrGroupOrganizerCanSetMeetingMemberRolesRule : IBusinessRule
    {
        private readonly Guid _settingMemberId;
        private readonly MeetingGroup _meetingGroup;
        private readonly List<MeetingAttendee> _attendees;

        public OnlyMeetingOrGroupOrganizerCanSetMeetingMemberRolesRule(Guid settingMemberId, MeetingGroup meetingGroup, List<MeetingAttendee> attendees)
        {
            _settingMemberId = settingMemberId;
            _meetingGroup = meetingGroup;
            _attendees = attendees;
        }

        public bool IsBroken()
        {
            var settingMember = _attendees.SingleOrDefault(x => x.IsActiveAttendee(_settingMemberId));

            var isHost = settingMember != null && settingMember.IsActiveHost();
            var isOrganizer = _meetingGroup.IsOrganizer(_settingMemberId);

            return !isHost && !isOrganizer;
        }

        public string Message => "Only meeting host or group organizer can set meeting member roles";
    }
}