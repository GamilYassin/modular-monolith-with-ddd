namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events
{
    public class MeetingGroupMemberLeftGroupDomainEvent : DomainEventBase
    {
        public MeetingGroupMemberLeftGroupDomainEvent(Guid meetingGroupId, Guid memberId)
        {
            MeetingGroupId = meetingGroupId;
            MemberId = memberId;
        }

        public Guid MeetingGroupId { get; }

        public Guid MemberId { get; }
    }
}