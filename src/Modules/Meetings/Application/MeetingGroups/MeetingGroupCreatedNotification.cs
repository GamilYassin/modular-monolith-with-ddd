using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;



namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups
{
    public class MeetingGroupCreatedNotification : DomainNotificationBase<MeetingGroupCreatedDomainEvent>
    {
        
        internal MeetingGroupCreatedNotification(MeetingGroupCreatedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}