using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;



namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals
{
    public class MeetingGroupProposedNotification : DomainNotificationBase<MeetingGroupProposedDomainEvent>
    {
        
        public MeetingGroupProposedNotification(MeetingGroupProposedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}