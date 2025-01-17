﻿using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Queries;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal
{
    public class GetMeetingGroupProposalQuery : QueryBase<MeetingGroupProposalDto>
    {
        public GetMeetingGroupProposalQuery(Guid meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        public Guid MeetingGroupProposalId { get; }
    }
}