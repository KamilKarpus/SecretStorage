using SS.Common.BuldingBlocks;
using SS.Common.Exceptions;
using SS.Organizations.Domain.Exceptions;
using SS.Organizations.Domain.Roles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SS.Organizations.Domain.Rules
{
    public class CanUserBeDeletedRule : IBusinessRule
    {
        private readonly HashSet<User> _users;
        private string _message;
        private Guid _userId;
        public SSException Exception => throw new RoleException(_message, HttpErrorCodes.NotAllowed, InternalErrorCodes.AtLeastOneOwner);
        public CanUserBeDeletedRule(HashSet<User> users, Guid userId)
        {
            _users = users;
            _userId = userId;
        }
        public bool isBroken()
        {
            var user = _users.FirstOrDefault(p => p.Id == _userId);
            var owners = _users.Count(p => p.Role == Role.Owner);
            if (owners <= 1 && user.Role == Role.Owner)
            {
                _message = "At least must be one owner in organization!";
                return true;
            }
            return false;
        }
    }
}
