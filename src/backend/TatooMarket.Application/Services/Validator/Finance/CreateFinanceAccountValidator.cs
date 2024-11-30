using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.Finance;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.Services.Validator.Finance
{
    public class CreateFinanceAccountValidator : AbstractValidator<RequestCreateFinanceAccount>
    {
        public CreateFinanceAccountValidator()
        {
            RuleFor(d => d.CurrencyType).IsInEnum().WithMessage(ResourceExceptMessages.CURRENCY_OUT_ENUM);
            RuleFor(d => d.Email).NotEmpty().WithMessage(ResourceExceptMessages.EMAIL_IS_EMPTY);
            When(d => string.IsNullOrEmpty(d.Email), () =>
            {
                RuleFor(d => d.Email).EmailAddress().WithMessage(ResourceExceptMessages.EMAIL_FORMAT);
            });
        }
    }
}
