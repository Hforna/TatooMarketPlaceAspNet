using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Exception.Exceptions
{
    public class ResponseErrorJson : BaseException
    {
        public ResponseErrorJson(List<string> errors) : base(string.Empty) => Errors = errors;
        public ResponseErrorJson(string message) : base(message) => Errors.Add(message);
        public override string GetErrorMessage() => Message;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
