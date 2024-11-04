using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.Extensions;
using TatooMarket.Application.Services.Validator.User;
using TatooMarket.Application.UseCases.Repositories.User;
using TatooMarket.Communication.Requests;
using TatooMarket.Communication.Responses;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Azure;
using TatooMarket.Domain.Repositories.Security.Cryptography;
using TatooMarket.Domain.Repositories.User;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.User
{
    public class CreateUser : ICreateUser
    {
        private readonly IUserWriteRepository _userWrite;
        private readonly IUserReadRepository _userRead;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly IPasswordCryptography _cryptography;
        private readonly IAzureStorageService _storageService;
        private readonly UserManager<UserEntity> _userManager;

        public CreateUser(IUserWriteRepository userWrite, IUserReadRepository userRead,
            IUnitOfWork uof, IMapper mapper,
            IPasswordCryptography cryptography, IAzureStorageService storageService,
            UserManager<UserEntity> userManager)
        {
            _userWrite = userWrite;
            _userRead = userRead;
            _uof = uof;
            _mapper = mapper;
            _cryptography = cryptography;
            _storageService = storageService;
            _userManager = userManager;
        }

        public async Task<ResponseCreateUser> Execute(RequestCreateUser request)
        {
            await Validate(request);

            var user = _mapper.Map<UserEntity>(request);

            if (request.UserImage != null)
            {
                var image = request.UserImage!.OpenReadStream();

                (bool isValid, string fileName) = ImageValidator.Validate(image);

                if (!isValid)
                    throw new UserException(ResourceExceptMessages.FILE_FORMAT);

                await _storageService.UploadUser(user, image, fileName);

                user.UserImage = fileName;
            }

            var password = _cryptography.Cryptography(request.Password);
            user.Password = password;

            await _userWrite.Add(user);
            await _uof.Commit();

            var role = "customer";

            if (user.IsSeller)
                role = "seller";

            await _userManager.AddToRoleAsync(user, role);

            return _mapper.Map<ResponseCreateUser>(user);
        }

        private async Task Validate(RequestCreateUser request)
        {
            var validator = new CreateUserValidator();
            var result = validator.Validate(request);

            if (request.Password != request.RepeatPassword)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceExceptMessages.REPEAT_PASSWORD_ERROR));

            if (await _userRead.EmailExists(request.Email))
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceExceptMessages.EMAIL_EXISTS));

            if (await _userRead.UserNameExists(request.UserName))
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceExceptMessages.USERNAME_EXISTS));

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(d => d.ErrorMessage).ToList();
            }
        }
    }
}
