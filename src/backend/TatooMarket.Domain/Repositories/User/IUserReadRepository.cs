using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Identity;

namespace TatooMarket.Domain.Repositories.User
{
    public interface IUserReadRepository
    {
        public Task<bool> EmailExists(string email);
        public Task<bool> UserNameExists(string username);
        public Task<UserEntity?> LoginByEmailAndPassword(string email);
        public Task<UserEntity?> UserByUid(Guid uid);
        public Task<UserEntity?> UserById(long id);
    }
}
