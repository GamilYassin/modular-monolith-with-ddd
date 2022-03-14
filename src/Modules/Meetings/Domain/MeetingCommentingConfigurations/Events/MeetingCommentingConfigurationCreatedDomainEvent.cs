
using DomainPack.DomainEvents;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events
{
    public class MeetingCommentingConfigurationCreatedDomainEvent : DomainEventBase
    {
        public Guid MeetingId { get; }

        public bool IsEnabled { get; }

        public MeetingCommentingConfigurationCreatedDomainEvent(Guid meetingId, bool isEnabled)
        {
            MeetingId = meetingId;
            IsEnabled = isEnabled;
        }
    }
}