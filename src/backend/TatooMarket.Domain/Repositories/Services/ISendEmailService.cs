using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Domain.Repositories.Services
{
    public interface ISendEmailService
    {
        public Task SendEmail(string email, string subject, string body, string name);
    }
}
