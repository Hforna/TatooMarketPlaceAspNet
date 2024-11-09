using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.User;

namespace TatooMarket.Application.UseCases.Repositories.User
{
    public interface IUpdateUser
    {
        public Task Execute(RequestUpdateUser request);
    }
}
