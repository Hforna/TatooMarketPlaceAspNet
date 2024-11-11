using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.Extensions;
using TatooMarket.Application.Services.Validator.User;
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
        private readonly IUserReadRepository _userRead;

        public UpdateUser(IUserWriteRepository userWrite, IGetUserByToken userByToken, 
            IUnitOfWork unitOfWork, IUserReadRepository userRead)
        {
            _userWrite = userWrite;
            _userByToken = userByToken;
            _unitOfWork = unitOfWork;
            _userRead = userRead;
        }

        public async Task Execute(RequestUpdateUser request)
        {
            await Validate(request);

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

        private async Task Validate(RequestUpdateUser request)
        {
            var validator = new UpdateUserValidator();
            var result = validator.Validate(request);

            if (await _userRead.EmailExists(request.Email))
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceExceptMessages.EMAIL_EXISTS));

            if(await _userRead.UserNameExists(request.UserName))
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceExceptMessages.USERNAME_EXISTS));

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(d => d.ErrorMessage).ToList();
                throw new UserException(errorMessages);
            }
        }
    }
}
