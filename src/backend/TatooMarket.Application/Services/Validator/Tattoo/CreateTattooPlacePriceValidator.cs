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
    public class CreateTattooPlacePriceValidator : AbstractValidator<RequestCreateTattooPlacePrice>
    {
        public CreateTattooPlacePriceValidator()
        {
            RuleFor(d => d.CurrencyType).IsInEnum().WithMessage(ResourceExceptMessages.TATTOO_SIZE_OUT_ENUM);
            RuleFor(d => d.BodyPlacement).IsInEnum().WithMessage(ResourceExceptMessages.TATTOO_PLACEMENT_OUT_ENUM);
        }
    }
}
