using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;



namespace CompanyName.MyMeetings.Modules.Meetings.Application.MemberSubscriptions.ChangeSubscriptionExpirationDateForMember
{
    public class ChangeSubscriptionExpirationDateForMemberCommand : InternalCommandBase
    {
        
        public ChangeSubscriptionExpirationDateForMemberCommand(
            Guid id,
            Guid memberId,
            DateTime expirationDate)
            : base(id)
        {
            MemberId = memberId;
            ExpirationDate = expirationDate;
        }

        public Guid MemberId { get; }

        public DateTime ExpirationDate { get; }
    }
}