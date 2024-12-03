using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.User;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.User;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.User
{
    public class VerifyAccount : IVerifyAccount
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUserWriteRepository _userWrite;
        private readonly IUserReadRepository _userRead;
        private readonly IUnitOfWork _unitOfWork;

        public VerifyAccount(UserManager<UserEntity> userManager, IUserWriteRepository userWrite, 
            IUserReadRepository userRead, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _userWrite = userWrite;
            _userRead = userRead;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(string code, string email)
        {
            var user = await _userRead.UserByEmail(email);

            if(user is null)
                throw new UserException(ResourceExceptMessages.USER_DOESNT_EXISTS);

            var verifyCode = await _userManager.ConfirmEmailAsync(user, code);

            if (!verifyCode.Succeeded)
                throw new UserException(ResourceExceptMessages.EMAIL_CODE_WRONG);

            user.Active = true;

            _userWrite.Update(user);
            await _unitOfWork.Commit();
        }
    }
}
