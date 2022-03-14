namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events
{
    internal class MeetingGroupProposalRejectedDomainEvent : DomainEventBase
    {
        internal MeetingGroupProposalRejectedDomainEvent(Guid meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        internal Guid MeetingGroupProposalId { get; }
    }
}