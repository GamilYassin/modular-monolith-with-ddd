namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events
{
    public class MeetingGroupProposalAcceptedDomainEvent : DomainEventBase
    {
        public Guid MeetingGroupProposalId { get; }

        public MeetingGroupProposalAcceptedDomainEvent(Guid meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }
    }
}