﻿using System;


namespace CompanyName.MyMeetings.Modules.Payments.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value)
            : base(value)
        {
        }
    }
}