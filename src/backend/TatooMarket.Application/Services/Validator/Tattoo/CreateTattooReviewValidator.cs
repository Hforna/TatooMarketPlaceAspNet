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
    public class CreateTattooReviewValidator : AbstractValidator<RequestCreateTattooReview>
    {
        public CreateTattooReviewValidator()
        {
            RuleFor(d => d.Comment).MaximumLength(500).WithMessage(ResourceExceptMessages.COMMENT_LENGTH_GREATER_THAN_500);
            RuleFor(d => d.Note).LessThanOrEqualTo(5).WithMessage(ResourceExceptMessages.REVIEW_NOTE_GREATER_THAN_5);
        }
    }
}
