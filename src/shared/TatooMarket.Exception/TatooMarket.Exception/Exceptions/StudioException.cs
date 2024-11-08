using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Exception.Exceptions
{
    public class StudioException : BaseException
    {
        public List<string> Errors { get; set; } = new List<string>();

        public StudioException(string message) : base(message) => Errors.Add(message);
        public StudioException(List<string> errors) : base(string.Empty) => Errors = errors;

        public override string GetErrorMessage() => Message;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
