using DomainPack.DomainEvents.EventBus;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationEvents
{
    public class MeetingFeePaidIntegrationEvent : IntegrationEvent
    {
        public MeetingFeePaidIntegrationEvent(
            Guid id,
            DateTime occurredOn,
            Guid payerId,
            Guid meetingId)
            : base(id, occurredOn)
        {
            PayerId = payerId;
            MeetingId = meetingId;
        }

        public Guid PayerId { get; }

        public Guid MeetingId { get; }
    }
}