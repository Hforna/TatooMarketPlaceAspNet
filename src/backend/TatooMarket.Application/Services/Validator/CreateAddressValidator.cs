using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.Address;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.Services.Validator
{
    public class CreateAddressValidator : AbstractValidator<RequestCreateAddress>
    {
        public CreateAddressValidator()
        {
            RuleFor(d => d.PostalCode.Length).Equal(8).WithMessage(ResourceExceptMessages.FORMAT_POSTALCODE_WRONG);
        }
    }
}
