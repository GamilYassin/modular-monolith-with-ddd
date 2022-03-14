using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeAddedEmail
{
    public class MeetingAttendeeAddedNotification : DomainNotificationBase<MeetingAttendeeAddedDomainEvent>
    {
        public MeetingAttendeeAddedNotification(MeetingAttendeeAddedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}