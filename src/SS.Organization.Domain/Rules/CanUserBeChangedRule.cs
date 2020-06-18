using SS.Common.BuldingBlocks;
using SS.Common.Exceptions;
using SS.Organizations.Domain.Exceptions;
using SS.Organizations.Domain.Roles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SS.Organizations.Domain.Rules
{
    public class CanUserBeChangedRule : IBusinessRule
    {
        private readonly HashSet<User> _users;
        private readonly Guid _id;
        private readonly Role _role;
        public CanUserBeChangedRule(HashSet<User> users, Guid id, Role role)
        {
            _users = users;
            _id = id;
            _role = role;
        }

        public SSException Exception => new RoleException("You cannot change the user role to the same role",HttpErrorCodes.NotAllowed ,InternalErrorCodes.Userhasthesamerole);

        public bool isBroken()
        {
            var user = _users.FirstOrDefault(p => p.Id == _id);
            if(user.Role != _role)
            {
                return false;
            }
            return true;
        }
    }
}
