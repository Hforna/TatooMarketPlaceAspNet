﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.User;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.Services.Validator.User
{
    public class CreateUserValidator : AbstractValidator<RequestCreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(8).WithMessage(ResourceExceptMessages.PASSWORD_GREATER_OR_EQUAL_EIGHT);
            RuleFor(u => u.Email).EmailAddress().WithMessage(ResourceExceptMessages.EMAIL_FORMAT);
            When(d => d.IsSeller == true, () =>
            {
                RuleFor(d => d.IsAnonymous).Must(d => d.Equals(false)).WithMessage(ResourceExceptMessages.NO_USER_ANONYMOUS_AND_SELLER);
            });
        }
    }
}
