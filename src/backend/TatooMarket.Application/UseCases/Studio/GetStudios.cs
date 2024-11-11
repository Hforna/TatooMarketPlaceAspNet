using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Studio;
using TatooMarket.Communication.Responses.Studio;
using TatooMarket.Domain.Repositories.Azure;
using TatooMarket.Domain.Repositories.StudioRepository;

namespace TatooMarket.Application.UseCases.Studio
{
    public class GetStudios : IGetStudios
    {
        private readonly IStudioReadOnly _studioRead;
        private readonly IMapper _mapper;
        private readonly SqidsEncoder<long> _sqidsEncoder;
        private readonly IAzureStorageService _azureStorage;

        public GetStudios(IStudioReadOnly studioRead, IMapper mapper, SqidsEncoder<long> sqidsEncoder, IAzureStorageService azureStorage)
        {
            _studioRead = studioRead;
            _mapper = mapper;
            _sqidsEncoder = sqidsEncoder;
            _azureStorage = azureStorage;
        }

        public async Task<ResponseStudioPagination> Execute(int page)
        {
            var studios = _studioRead.GetStudios(page);

            var studiosList = studios.Select(async d =>
            {
                var response = _mapper.Map<ResponseShortStudio>(d);

                response.OwnerId = _sqidsEncoder.Encode(d.OwnerId);

                response.ImageStudio = await _azureStorage.GetImage(d.Id.ToString(), d.ImageStudio);

                return response;
            });

            var studiosListTask = await Task.WhenAll(studiosList);

            var response = new ResponseStudioPagination()
            {
                IsFirstPage = studios.IsFirstPage,
                IsLastPage = studios.IsLastPage,
                TotalItemCount = studios.TotalItemCount,
                LastItemOnPage = studios.LastItemOnPage,
                FirstItemOnPage = studios.FirstItemOnPage,
                Count = studios.Count,
                HasNextPage = studios.HasNextPage,
                HasPreviousPage = studios.HasPreviousPage,
                PageCount = studios.PageCount,
                PageNumber = studios.PageNumber,
                PageSize = studios.PageSize
            };

            response.Studios = studiosListTask;

            return response;
        }
    }
}
