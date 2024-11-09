using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.Extensions;
using TatooMarket.Application.UseCases.Repositories.User;
using TatooMarket.Communication.Requests.User;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.User;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.User
{
    public class UpdateUser : IUpdateUser
    {
        private readonly IUserWriteRepository _userWrite;
        private readonly IGetUserByToken _userByToken;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUser(IUserWriteRepository userWrite, IGetUserByToken userByToken, IUnitOfWork unitOfWork)
        {
            _userWrite = userWrite;
            _userByToken = userByToken;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestUpdateUser request)
        {
            var user = await _userByToken.GetUser();

            if(request.UserImage is not null)
            {
                var image = request.UserImage.OpenReadStream();

                (bool isValid, string ext) = ImageValidator.Validate(image);

                if (!isValid)
                    throw new UserException(ResourceExceptMessages.FILE_FORMAT);

                user.UserImage = $"{Guid.NewGuid}{ext}";
            }

            user.UserName = request.UserName;
            user.Email = request.Email;

            _userWrite.Update(user);

            await _unitOfWork.Commit();
        }
    }
}
