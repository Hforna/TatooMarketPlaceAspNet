using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Enums;
using TatooMarket.Communication.Requests.Tattoo;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.Services.Validator.Tattoo
{
    public class CreateTattooValidator : AbstractValidator<RequestCreateTattoo>
    {
        public CreateTattooValidator()
        {
            RuleFor(d => d.Style).IsInEnum().WithMessage(ResourceExceptMessages.TATTOO_STYLE_OUT_ENUM);
            RuleFor(d => d.Size).IsInEnum().WithMessage(ResourceExceptMessages.TATTOO_SIZE_OUT_ENUM);
            RuleFor(d => d.BodyPlacement).IsInEnum().WithMessage(ResourceExceptMessages.TATTOO_PLACEMENT_OUT_ENUM);
        }
    }
}
