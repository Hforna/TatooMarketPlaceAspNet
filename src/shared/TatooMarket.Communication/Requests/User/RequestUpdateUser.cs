using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Communication.Requests.User
{
    public class RequestUpdateUser
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public IFormFile? UserImage { get; set; }
    }
}
