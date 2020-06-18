using SS.Users.Application.Configuration.Queries;
using SS.Users.Application.ReadModels;
using System;

namespace SS.Users.Application.GetUserShortInfo
{
    public class GetUserShortInfobyEmailQuery : IQuery<UserShortInfo>
    {
        public string Email { get; set; }
    }
}
