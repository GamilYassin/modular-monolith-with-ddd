namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals
{
    public class MeetingGroupProposalDecision : ValueObjectBase
    {
        private MeetingGroupProposalDecision(DateTime? date, Guid userId, string code, string rejectReason)
        {
            this.Date = date;
            this.UserId = userId;
            this.Code = code;
            this.RejectReason = rejectReason;
        }

        public DateTime? Date { get; }

        public Guid UserId { get; }

        public string Code { get; }

        public string RejectReason { get; }

        internal static MeetingGroupProposalDecision NoDecision =>
            new MeetingGroupProposalDecision(null, Guid.Empty, null, null);

        private bool IsAccepted => this.Code == "Accept";

        private bool IsRejected => this.Code == "Reject";

        internal static MeetingGroupProposalDecision AcceptDecision(DateTime date, Guid userId)
        {
            return new MeetingGroupProposalDecision(date, userId, "Accept", null);
        }

        internal static MeetingGroupProposalDecision RejectDecision(DateTime date, Guid userId, string rejectReason)
        {
            return new MeetingGroupProposalDecision(date, userId, "Reject", rejectReason);
        }

        internal MeetingGroupProposalStatus GetStatusForDecision()
        {
            if (this.IsAccepted)
            {
                return MeetingGroupProposalStatus.Verified;
            }

            if (this.IsRejected)
            {
                return MeetingGroupProposalStatus.Create("Rejected");
            }

            return MeetingGroupProposalStatus.ToVerify;
        }
    }
}