using SS.Common.BuldingBlocks;
using SS.Common.Exceptions;
using SS.Organizations.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace SS.Organizations.Domain.Rules
{
    public class IsApplicationExistsRule : IBusinessRule
    {
        private readonly HashSet<App> _applications;
        private readonly string _name;
        public SSException Exception => new AppException("Application already exists.",HttpErrorCodes.ResourceExists, InternalErrorCodes.ApplicationExists);

        public IsApplicationExistsRule(string name, HashSet<App> applications)
        {
            _applications = applications;
            _name = name;
        }
        public bool isBroken()
        {
            var application = _applications.FirstOrDefault(p => p.Name == _name);
            if(application == null)
            {
                return false;
            }
            return true;
        }
    }
}
