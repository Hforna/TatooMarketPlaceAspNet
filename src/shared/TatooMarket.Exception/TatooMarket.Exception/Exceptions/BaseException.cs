using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Exception.Exceptions
{
    public abstract class BaseException : SystemException
    {
        public List<string> Errors { get; set; } = [];
        public BaseException(string error) : base(error) { }

        public abstract string GetErrorMessage();

        public abstract HttpStatusCode GetStatusCode();
    }
}
