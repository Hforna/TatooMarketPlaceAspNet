using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Identity;

namespace TatooMarket.Domain.Repositories.Security.Token
{
    public interface IGetUserByToken
    {
        public Task<UserEntity?> GetUser();
    }
}
