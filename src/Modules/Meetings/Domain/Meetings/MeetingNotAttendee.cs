using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingNotAttendee : EntityObjectBase<Guid>
    {
        internal Guid MemberId { get; private set; }

        internal Guid MeetingId { get; private set; }

        private DateTime _decisionDate;

        private bool _decisionChanged;

        private DateTime? _decisionChangeDate;

        private MeetingNotAttendee(Guid meetingId, Guid memberId) : base(default)
        {
            this.MemberId = memberId;
            this.MeetingId = meetingId;
            _decisionDate = DateTime.UtcNow;

            this.AddDomainEvent(new MeetingNotAttendeeAddedDomainEvent(this.MeetingId, this.MemberId));
        }

        internal static MeetingNotAttendee CreateNew(Guid meetingId, Guid memberId)
        {
            return new MeetingNotAttendee(meetingId, memberId);
        }

        internal bool IsActiveNotAttendee(Guid memberId)
        {
            return !this._decisionChanged && this.MemberId == memberId;
        }

        internal void ChangeDecision()
        {
            _decisionChanged = true;
            _decisionChangeDate = SystemClock.Now;

            this.AddDomainEvent(new MeetingNotAttendeeChangedDecisionDomainEvent(this.MemberId, this.MeetingId));
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}