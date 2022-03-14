﻿using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetMeetingGroupDetails
{
    public class GetMeetingGroupDetailsQuery : QueryBase<MeetingGroupDetailsDto>
    {
        public GetMeetingGroupDetailsQuery(Guid meetingGroupId)
        {
            MeetingGroupId = meetingGroupId;
        }

        public Guid MeetingGroupId { get; }
    }
}