using System;
using DomainPack.DomainEvents.EventBus;

namespace CompanyName.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals
{
    public class MeetingGroupProposalAcceptedIntegrationEvent : IntegrationEvent
    {
        public MeetingGroupProposalAcceptedIntegrationEvent(
            Guid id,
            DateTime occurredOn,
            Guid meetingGroupProposalId)
            : base(id, occurredOn)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        public Guid MeetingGroupProposalId { get; }
    }
}
