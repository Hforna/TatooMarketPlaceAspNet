using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Communication.Requests.User
{
    public class RequestCreateUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public bool IsSeller { get; set; }
        public bool IsAnonymous { get; set; }
        public string Email { get; set; }
        public IFormFile? UserImage { get; set; }
    }
}
