using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Communication.Requests.Studio
{
    public class RequestCreateStudio
    {
        public string StudioName { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile? ImageStudio { get; set; }
    }
}
