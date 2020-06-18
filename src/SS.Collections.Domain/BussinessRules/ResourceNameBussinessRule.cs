using SS.Collections.Domain.Exceptions;
using SS.Common.BuldingBlocks;
using SS.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace SS.Collections.Domain.BussinessRules
{
    public class ResourceNameBussinessRule : IBusinessRule
    {
        private readonly HashSet<Resource> _resources;
        private string _name;
        public ResourceNameBussinessRule(HashSet<Resource> resources, string name)
        {
            _resources = resources;
            _name = name;
        }
        public SSException Exception => new ResourceException($"[{_name}] is already taken.", HttpCodes.Conflict, ExceptionCode.CollestionNameIsTaken);

        public bool isBroken()
        {
            var resource = _resources.FirstOrDefault(p => p.Name == _name);
            if(resource != null)
            {
                return true;
            }
            return false;
        }
    }
}
