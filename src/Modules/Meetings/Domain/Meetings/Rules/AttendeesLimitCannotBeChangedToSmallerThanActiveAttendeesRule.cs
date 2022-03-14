
using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    internal class AttendeesLimitCannotBeChangedToSmallerThanActiveAttendeesRule : IBusinessRule
    {
        private readonly int? _attendeesLimit;

        private readonly int _allActiveAttendeesWithGuestsNumber;

        internal AttendeesLimitCannotBeChangedToSmallerThanActiveAttendeesRule(
            MeetingLimits meetingLimits,
            int allActiveAttendeesWithGuestsNumber)
        {
            this._attendeesLimit = meetingLimits.AttendeesLimit;
            this._allActiveAttendeesWithGuestsNumber = allActiveAttendeesWithGuestsNumber;
        }

        public bool IsBroken() => _attendeesLimit.HasValue && _attendeesLimit.Value < _allActiveAttendeesWithGuestsNumber;

        public string Message => "Attendees limit cannot be change to smaller than active attendees number";
    }
}