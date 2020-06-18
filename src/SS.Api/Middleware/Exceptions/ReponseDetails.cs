using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SS.Api.Middleware.Exceptions
{
    public class ReponseDetails
    {
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
        public uint ErrorCode { get; set; }
    }
}
