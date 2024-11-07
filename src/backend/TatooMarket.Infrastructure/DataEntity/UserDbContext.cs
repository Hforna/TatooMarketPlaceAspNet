using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Repositories.User;

namespace TatooMarket.Infrastructure.DataEntity
{
    public class UserDbContext : IUserReadRepository, IUserWriteRepository
    {
        private readonly ProjectDbContext _dbContext;

        public UserDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(UserEntity user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<UserEntity?> LoginByEmailAndPassword(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(d => d.Active && d.Email == email);
        }

        public void Update(UserEntity user)
        {
            _dbContext.Users.Update(user);
        }

        public async Task<UserEntity?> UserByUid(Guid uid)
        {
            return await _dbContext.Users.Include(d => d.Studio).FirstOrDefaultAsync(d => d.UserIdentifier == uid && d.Active);
        }

        public async Task<bool> UserNameExists(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.UserName == username);
        }
    }
}
