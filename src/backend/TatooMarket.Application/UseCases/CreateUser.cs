using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.Extensions;
using TatooMarket.Application.Services.Validator.User;
using TatooMarket.Application.UseCases.Repositories;
using TatooMarket.Communication.Requests;
using TatooMarket.Communication.Responses;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Security.Cryptography;
using TatooMarket.Domain.Repositories.User;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases
{
    public class CreateUser : ICreateUser
    {
        private readonly IUserWriteRepository _userWrite;
        private readonly IUserReadRepository _userRead;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly IPasswordCryptography _cryptography;

        public async Task<ResponseCreateUser> Execute(RequestCreateUser request)
        {
            await Validate(request);

            var user = _mapper.Map<UserEntity>(request);

            if (request.UserImage != null)
            {
                var image = request.UserImage!.OpenReadStream();

                (bool isValid, string fileName) = ImageValidator.Validate(image);

                if (!isValid)
                    throw new UserException("File must be a image");

                user.UserImage = fileName;
            }

            var password = _cryptography.Cryptography(request.Password);
            user.Password = password;

            await _userWrite.Add(user);
            await _uof.Commit();

            return _mapper.Map<ResponseCreateUser>(user);
        }

        private async Task Validate(RequestCreateUser request)
        {
            var validator = new CreateUserValidator();
            var result = validator.Validate(request);

            if (await _userRead.EmailExists(request.Email))
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "E-mail already exists"));

            if (await _userRead.UserNameExists(request.UserName))
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Username already exists"));

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(d => d.ErrorMessage).ToList();
            }
        }
    }
}
