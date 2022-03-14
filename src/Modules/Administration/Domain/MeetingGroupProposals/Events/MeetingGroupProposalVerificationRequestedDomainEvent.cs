namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events
{
    public class MeetingGroupProposalVerificationRequestedDomainEvent : DomainEventBase
    {
        internal MeetingGroupProposalVerificationRequestedDomainEvent(Guid meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        public Guid MeetingGroupProposalId { get; }
    }
}