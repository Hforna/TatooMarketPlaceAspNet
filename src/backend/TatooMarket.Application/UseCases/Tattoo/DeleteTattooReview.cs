using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Review;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.Tattoo;

using TatooMarket.Exception.Exceptions;
namespace TatooMarket.Application.UseCases.Tattoo
{
    public class DeleteTattooReview : IDeleteTattooReview
    {
        private readonly IGetUserByToken _userByToken;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReviewWriteOnly _reviewWrite;
        private readonly IReviewReadOnly _reviewRead;
        private readonly ITattooReadOnly _tattooRead;
        private readonly ITattooWriteOnly _tattooWrite;

        public DeleteTattooReview(IGetUserByToken userByToken, IUnitOfWork unitOfWork, 
            IReviewWriteOnly reviewWrite, IReviewReadOnly reviewRead, 
            ITattooReadOnly tattooRead, ITattooWriteOnly tattooWrite)
        {
            _userByToken = userByToken;
            _unitOfWork = unitOfWork;
            _reviewWrite = reviewWrite;
            _reviewRead = reviewRead;
            _tattooRead = tattooRead;
            _tattooWrite = tattooWrite;
        }

        public async Task Execute(long id)
        {
            var user = await _userByToken.GetUser();

            var review = await _reviewRead.ReviewById(id);

            if (review is null || review.CustomerId != user!.Id)
                throw new ReviewException(ResourceExceptMessages.REVIEW_DOESNT_EXISTS);

            var tattoo = review.Tattoo;
            var tattooReviews = await _tattooRead.GetTattooReviews(tattoo);
            var sumNotes = 0;

            foreach(var reviewf in tattooReviews)
            {
                if (reviewf.Id != review.Id)
                    sumNotes += reviewf.Note;
            }

            tattoo.Note = sumNotes / tattooReviews.Count;

            _reviewWrite.Delete(review);
            _tattooWrite.Update(tattoo);

            await _unitOfWork.Commit();
        }
    }
}
