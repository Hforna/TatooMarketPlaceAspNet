using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Domain.Repositories.User
{
    public interface IUserReadRepository
    {
        public Task<bool> EmailExists(string email);
        public Task<bool> UserNameExists(string username);
    }
}
