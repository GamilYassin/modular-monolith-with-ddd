﻿using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;

using System.Collections.Generic;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Authorization.GetUserPermissions
{
    public class GetUserPermissionsQuery : QueryBase<List<UserPermissionDto>>
    {
        public GetUserPermissionsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}