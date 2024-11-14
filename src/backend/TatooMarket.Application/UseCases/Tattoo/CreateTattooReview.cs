using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.Services.Validator.Tattoo;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Communication.Requests.Tattoo;
using TatooMarket.Domain.Entities.Communication;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Review;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.StudioRepository;
using TatooMarket.Domain.Repositories.Tattoo;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Tattoo
{
    public class CreateTattooReview : ICreateTattooReview
    {
        private readonly IGetUserByToken _userByToken;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReviewWriteOnly _reviewWrite;
        private readonly ITattooReadOnly _tattooRead;
        private readonly ITattooWriteOnly _tattooWrite;
        private readonly IStudioReadOnly _studioRead;
        private readonly SqidsEncoder<long> _sqidsEncoder;
        private readonly IMapper _mapper;

        public CreateTattooReview(IGetUserByToken userByToken, IUnitOfWork unitOfWork, 
            IReviewWriteOnly reviewWrite, ITattooReadOnly tattooRead, 
            ITattooWriteOnly tattooWrite, IStudioReadOnly studioRead, 
            SqidsEncoder<long> sqidsEncoder, IMapper mapper)
        {
            _userByToken = userByToken;
            _unitOfWork = unitOfWork;
            _reviewWrite = reviewWrite;
            _tattooRead = tattooRead;
            _tattooWrite = tattooWrite;
            _studioRead = studioRead;
            _sqidsEncoder = sqidsEncoder;
            _mapper = mapper;
        }

        public async Task Execute(RequestCreateTattooReview request)
        {
            Validate(request);

            var user = await _userByToken.GetUser();

            var tattoo = await _tattooRead.TattooById(_sqidsEncoder.Decode(request.TattooId).Single());

            if (tattoo is null)
                throw new TattooException(ResourceExceptMessages.TATTOO_DOESNT_EXISTS);

            var userReview = _mapper.Map<ReviewEntity>(request);

            userReview.CustomerId = user.IsAnonymous ? user.Id : null;
            userReview.StudioId = tattoo.StudioId;
            userReview.TattooId = tattoo.Id;

            await _reviewWrite.Add(userReview);

            await _unitOfWork.Commit();

            var reviews = await _tattooRead.GetTattooReviews(tattoo);

            if (reviews.Count > 0)
            {
                var notesTotal = 0;

                foreach (var review in reviews)
                {
                    notesTotal += review.Note;
                }

                tattoo.Note = notesTotal / reviews.Count;

                _tattooWrite.Update(tattoo);

                await _unitOfWork.Commit();
            }   
        }

        private void Validate(RequestCreateTattooReview request)
        {
            var validator = new CreateTattooReviewValidator();
            var result = validator.Validate(request);

            if(!result.IsValid)
            {
                var errorMessages = result.Errors.Select(d => d.ErrorMessage).ToList();
                throw new TattooException(errorMessages);
            }
        }
    }
}
