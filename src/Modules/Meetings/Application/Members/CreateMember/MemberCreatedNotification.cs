using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;



namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members.CreateMember
{
    public class MemberCreatedNotification : DomainNotificationBase<MeetingCreatedDomainEvent>
    {
        
        public MemberCreatedNotification(MeetingCreatedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}