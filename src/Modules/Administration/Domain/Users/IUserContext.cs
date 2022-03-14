using System;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.Users
{
    public interface IUserContext
    {
        Guid UserId { get; }
    }
}