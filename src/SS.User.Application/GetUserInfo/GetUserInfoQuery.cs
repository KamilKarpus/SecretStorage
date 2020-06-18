using SS.Users.Application.Configuration.Queries;
using SS.Users.Application.ReadModels;
using System;

namespace SS.Users.Application.GetUserInfo
{
    public class GetUserInfoQuery : IQuery<UserInfo>
    {
        public Guid Id { get; set; }
    }
}
