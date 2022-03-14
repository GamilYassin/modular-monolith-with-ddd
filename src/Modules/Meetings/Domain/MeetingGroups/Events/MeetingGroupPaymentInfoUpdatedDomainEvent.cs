namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events
{
    public class MeetingGroupPaymentInfoUpdatedDomainEvent : DomainEventBase
    {
        public MeetingGroupPaymentInfoUpdatedDomainEvent(Guid meetingGroupId, DateTime paymentDateTo)
        {
            MeetingGroupId = meetingGroupId;
            PaymentDateTo = paymentDateTo;
        }

        public Guid MeetingGroupId { get; }

        public DateTime PaymentDateTo { get; }
    }
}