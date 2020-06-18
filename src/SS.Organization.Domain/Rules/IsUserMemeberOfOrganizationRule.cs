using SS.Common.BuldingBlocks;
using SS.Common.Exceptions;
using SS.Organizations.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SS.Organizations.Domain.Rules
{
    public class IsUserMemeberOfOrganizationRule : IBusinessRule
    {
        private readonly HashSet<User> _users;
        private readonly Guid _id;
        public IsUserMemeberOfOrganizationRule(HashSet<User> users, Guid id)
        {
            _users = users;
            _id = id;
        }

        public SSException Exception => new OrganizationException("User is not a member of organization.",HttpErrorCodes.ResourceNotFound, InternalErrorCodes.UserIsNotMemberOfOrganization);

        public bool isBroken()
        {
            var existingUser = _users.FirstOrDefault(p => p.Id == _id);
            if (existingUser == null)
            {
                return true;
            }
            return false;
        }
    }
}
