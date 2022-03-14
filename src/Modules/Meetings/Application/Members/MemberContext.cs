using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members
{
    public class MemberContext : IMemberContext
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public MemberContext(IExecutionContextAccessor executionContextAccessor)
        {
            this._executionContextAccessor = executionContextAccessor;
        }

        public Guid MemberId => _executionContextAccessor.UserId;
    }
}