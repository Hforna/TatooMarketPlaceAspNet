using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.Tattoo;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.Services.Validator.Tattoo
{
    public class CreateTattooStylePriceValidator : AbstractValidator<RequestCreateTattooStylePrice>
    {
        public CreateTattooStylePriceValidator()
        {
            RuleFor(d => d.CurrencyType).IsInEnum().WithMessage(ResourceExceptMessages.CURRENCY_OUT_ENUM);
            RuleFor(d => d.TattooStyle).IsInEnum().WithMessage(ResourceExceptMessages.TATTOO_STYLE_OUT_ENUM);
        }
    }
}
