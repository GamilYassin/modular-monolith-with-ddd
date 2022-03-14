using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

using System.Collections.Generic;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.GetMeetingFees
{
    public class GetMeetingFeesQuery : QueryBase<List<MeetingFeeDto>>
    {
        public GetMeetingFeesQuery(Guid meetingId)
        {
            MeetingId = meetingId;
        }

        public Guid MeetingId { get; }
    }
}