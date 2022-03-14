using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;

using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MemberSubscriptions.ChangeSubscriptionExpirationDateForMember
{
    public class ChangeSubscriptionExpirationDateForMemberCommand : InternalCommandBase
    {
        [JsonConstructor]
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