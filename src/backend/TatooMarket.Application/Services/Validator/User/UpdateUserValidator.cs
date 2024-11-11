using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.User;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.Services.Validator.User
{
    public class UpdateUserValidator : AbstractValidator<RequestUpdateUser>
    {
        public UpdateUserValidator()
        {
            RuleFor(d => d.UserName).NotEmpty().WithMessage(ResourceExceptMessages.USERNAME_IS_EMPTY);
            RuleFor(d => d.Email).NotEmpty().WithMessage(ResourceExceptMessages.EMAIL_IS_EMPTY);
            When(d => string.IsNullOrEmpty(d.Email) == false, () =>
            {
                RuleFor(d => d.Email).EmailAddress().WithMessage(ResourceExceptMessages.EMAIL_FORMAT);
            });
        }
    }
}
