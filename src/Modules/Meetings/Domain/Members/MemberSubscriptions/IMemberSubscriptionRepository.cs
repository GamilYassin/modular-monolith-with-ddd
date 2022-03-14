namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions
{
    public interface IMemberSubscriptionRepository
    {
        Task<MemberSubscription> GetByIdOptionalAsync(Guid memberSubscriptionId);

        Task AddAsync(MemberSubscription memberSubscription);
    }
}