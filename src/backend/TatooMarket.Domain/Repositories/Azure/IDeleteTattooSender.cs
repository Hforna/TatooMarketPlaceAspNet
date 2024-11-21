using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Domain.Repositories.Azure
{
    public interface IDeleteTattooSender
    {
        public Task SendMessage(long id);
    }
}
