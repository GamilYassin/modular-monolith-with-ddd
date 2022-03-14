using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

using DomainPack.Entities;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingWaitlistMember : EntityObjectBase<Guid>
    {
        internal Guid MemberId { get; private set; }

        internal Guid MeetingId { get; private set; }

        internal DateTime SignUpDate { get; private set; }

        private bool _isSignedOff;

        private DateTime? _signOffDate;

        private bool _isMovedToAttendees;

        private DateTime? _movedToAttendeesDate;

        private MeetingWaitlistMember(Guid meetingId, Guid memberId)
        {
            this.MemberId = memberId;
            this.MeetingId = meetingId;
            this.SignUpDate = SystemClock.Now;
            _isMovedToAttendees = false;

            this.AddDomainEvent(new MeetingWaitlistMemberAddedDomainEvent(this.MeetingId, this.MemberId));
        }

        internal static MeetingWaitlistMember CreateNew(Guid meetingId, Guid memberId)
        {
            return new MeetingWaitlistMember(meetingId, memberId);
        }

        internal void MarkIsMovedToAttendees()
        {
            _isMovedToAttendees = true;
            _movedToAttendeesDate = SystemClock.Now;
        }

        internal bool IsActiveOnWaitList(Guid memberId)
        {
            return this.MemberId == memberId && this.IsActive();
        }

        internal bool IsActive()
        {
            return !_isSignedOff && !_isMovedToAttendees;
        }

        internal void SignOff()
        {
            _isSignedOff = true;
            _signOffDate = SystemClock.Now;

            this.AddDomainEvent(new MemberSignedOffFromMeetingWaitlistDomainEvent(this.MeetingId, this.MemberId));
        }
    }
}