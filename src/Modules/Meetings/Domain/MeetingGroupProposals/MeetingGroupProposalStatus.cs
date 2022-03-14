namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals
{
    public class MeetingGroupProposalStatus : ValueObjectBase
    {
        public string Value { get; }

        internal static MeetingGroupProposalStatus InVerification => new MeetingGroupProposalStatus("InVerification");

        internal static MeetingGroupProposalStatus Accepted => new MeetingGroupProposalStatus("Accepted");

        internal bool IsAccepted => Value == "Accepted";

        private MeetingGroupProposalStatus(string value)
        {
            Value = value;
        }
    }
}