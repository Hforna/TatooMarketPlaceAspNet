using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Login;
using TatooMarket.Communication.Requests.Login;
using TatooMarket.Communication.Responses.Login;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Repositories.Security.Cryptography;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.User;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Login
{
    public class LoginByApplication : ILoginByApplication
    {
        private IUserReadRepository _userRead;
        private IPasswordCryptography _passwordCryptography;
        private ITokenGenerator _tokenGenerator;
        private UserManager<UserEntity> _userManager;

        public LoginByApplication(IUserReadRepository userRead, IPasswordCryptography passwordCryptography, ITokenGenerator tokenGenerator, UserManager<UserEntity> userManager)
        {
            _userRead = userRead;
            _passwordCryptography = passwordCryptography;
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
        }

        public async Task<ResponseLogin> Execute(RequestLogin request)
        {
            var user = await _userRead.LoginByEmailAndPassword(request.Email);

            if (user is null || !_passwordCryptography.IsValid(request.Password, user.Password))
                throw new UserException(ResourceExceptMessages.EMAIL_PASSWORD_INVALID);

            if (!user.EmailConfirmed)
                throw new UserException(ResourceExceptMessages.EMAIL_NOT_VERIFIED);

            var claims = new List<Claim>();

            foreach(var role in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = _tokenGenerator.GenerateToken(user.UserIdentifier, claims);


            return new ResponseLogin() { AccessToken = token };
        }
    }
}
