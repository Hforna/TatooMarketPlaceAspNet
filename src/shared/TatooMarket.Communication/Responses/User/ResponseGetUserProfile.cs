using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Responses.Studio;

namespace TatooMarket.Communication.Responses.User
{
    public class ResponseGetUserProfile
    {
        public string UserName { get; set; }
        public string? UserImage { get; set; }
        public string Email { get; set; }
        public ResponseShortStudio? UserStudio { get; set; }
    }
}
