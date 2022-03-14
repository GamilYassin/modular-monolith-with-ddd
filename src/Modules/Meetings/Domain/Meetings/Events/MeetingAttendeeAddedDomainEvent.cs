using System;

using DomainPack.DomainEvents;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingAttendeeAddedDomainEvent : DomainEventBase
    {
        public MeetingAttendeeAddedDomainEvent(
            Guid meetingId,
            Guid attendeeId,
            DateTime rsvpDate,
            string role,
            int guestsNumber,
            decimal? feeValue,
            string feeCurrency)
        {
            MeetingId = meetingId;
            AttendeeId = attendeeId;
            RSVPDate = rsvpDate;
            Role = role;
            GuestsNumber = guestsNumber;
            FeeValue = feeValue;
            FeeCurrency = feeCurrency;
        }

        public Guid MeetingId { get; }

        public Guid AttendeeId { get; }

        public DateTime RSVPDate { get; }

        public string Role { get; }

        public int GuestsNumber { get; }

        public decimal? FeeValue { get; }

        public string FeeCurrency { get; }
    }
}