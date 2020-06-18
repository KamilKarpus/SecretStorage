using SS.Common.BuldingBlocks;
using SS.Common.Exceptions;
using SS.Organizations.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SS.Organizations.Domain.Rules
{
    public class IsUserNotMemberOfOrganization : IBusinessRule
    {
        private HashSet<User> _users;
        private Guid _id;
        public IsUserNotMemberOfOrganization(HashSet<User> users, Guid id)
        {
            _users = users;
            _id = id;
        }
        public SSException Exception => throw new OrganizationException("Cannot find user", HttpErrorCodes.ResourceNotFound, InternalErrorCodes.CannotFindUser);

        public bool isBroken()
        {
            var user = _users.FirstOrDefault(p => p.Id == _id);
            if(user != null)
            {
                return true;
            }
            return false;
        }
    }
}
