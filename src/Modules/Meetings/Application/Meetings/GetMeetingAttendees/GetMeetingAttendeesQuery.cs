﻿using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingAttendees
{
    public class GetMeetingAttendeesQuery : QueryBase<List<MeetingAttendeeDto>>
    {
        public GetMeetingAttendeesQuery(Guid meetingId)
        {
            MeetingId = meetingId;
        }

        public Guid MeetingId { get; }
    }
}