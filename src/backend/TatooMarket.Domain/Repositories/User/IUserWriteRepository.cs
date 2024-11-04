using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Identity;

namespace TatooMarket.Domain.Repositories.User
{
    public interface IUserWriteRepository
    {
        public Task Add(UserEntity user);
        public void Update(UserEntity user);
    }
}
