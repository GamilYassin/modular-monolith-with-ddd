
using DomainPack.DomainEvents;

using System;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events
{
    public class MeetingGroupProposalAcceptedDomainEvent : DomainEventBase
    {
        public MeetingGroupProposalAcceptedDomainEvent(Guid meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        public Guid MeetingGroupProposalId { get; }
    }
}