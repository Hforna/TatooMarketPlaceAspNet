using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Communication.Responses.Studio;
using TatooMarket.Communication.Responses.Tatto;
using TatooMarket.Communication.Responses.Tattoo;
using TatooMarket.Domain.Entities.Tattoo;
using TatooMarket.Domain.Repositories.StudioRepository;
using TatooMarket.Domain.Repositories.Tattoo;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Tattoo
{
    public class GetStudioTattoos : IGetStudioTattoos
    {
        private readonly ITattooReadOnly _tattooRead;
        private readonly IStudioReadOnly _studioRead;
        private readonly IMapper _mapper;

        public GetStudioTattoos(ITattooReadOnly tattooRead, IStudioReadOnly studioRead, IMapper mapper)
        {
            _tattooRead = tattooRead;
            _studioRead = studioRead;
            _mapper = mapper;
        }

        public async Task<ResponseStudioTattoos> Execute(long studioId, int pageNumber)
        {
            var studio = await _studioRead.StudioById(studioId);

            if (studio is null)
                throw new StudioException(ResourceExceptMessages.STUDIO_DOESNT_EXISTS);

            var tattoos = await _tattooRead.GetStudioTattoos(studio, pageNumber);

            var response = new ResponseStudioTattoos()
            {
                IsFirstPage = tattoos.IsFirstPage,
                IsLastPage = tattoos.IsLastPage,
                TotalItemCount = tattoos.TotalItemCount,
                LastItemOnPage = tattoos.LastItemOnPage,
                FirstItemOnPage = tattoos.FirstItemOnPage,
                Count = tattoos.Count,
                HasNextPage = tattoos.HasNextPage,
                HasPreviousPage = tattoos.HasPreviousPage,
                PageCount = tattoos.PageCount,
                PageNumber = tattoos.PageNumber,
                PageSize = tattoos.PageSize
            };
            response.Tattoos = _mapper.Map<List<ResponseShortTatto>>(tattoos);

            return response;
        }
    }
}
