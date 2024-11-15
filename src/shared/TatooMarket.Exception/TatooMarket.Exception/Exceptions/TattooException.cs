using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Exception.Exceptions
{
    public class TattooException : BaseException
    {
        public TattooException(string error) : base(error) => Errors.Add(error);
        public TattooException(List<string> errors) : base(string.Empty) => Errors = errors;

        public override string GetErrorMessage() => Message;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.InternalServerError;
    }
}
