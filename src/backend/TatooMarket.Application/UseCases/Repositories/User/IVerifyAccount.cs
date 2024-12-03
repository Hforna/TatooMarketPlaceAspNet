using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Application.UseCases.Repositories.User
{
    public interface IVerifyAccount
    {
        public Task Execute(string code, string email);
    }
}
