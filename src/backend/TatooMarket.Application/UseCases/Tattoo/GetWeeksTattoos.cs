using AutoMapper;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Communication.Requests.Tattoo;
using TatooMarket.Communication.Responses.Studio;
using TatooMarket.Communication.Responses.Tatto;
using TatooMarket.Communication.Responses.Tattoo;
using TatooMarket.Domain.Repositories.Azure;
using TatooMarket.Domain.Repositories.Tattoo;

namespace TatooMarket.Application.UseCases.Tattoo
{
    public class GetWeeksTattoos : IGetWeeksTattoos
    {
        private readonly ITattooReadOnly _tattooRead;
        private readonly IMapper _mapper;
        private readonly IAzureStorageService _storageService;
        private readonly SqidsEncoder<long> _sqids;

        public GetWeeksTattoos(ITattooReadOnly tattooRead, IMapper mapper, 
            IAzureStorageService storageService, SqidsEncoder<long> sqidsEncoder)
        {
            _tattooRead = tattooRead;
            _mapper = mapper;
            _storageService = storageService;
            _sqids = sqidsEncoder;
        }

        public async Task<ResponseWeeksTattoos> Execute(RequestSelectDate request)
        {
            var tattoos = await _tattooRead.WeeksTattoos(request.Date);

            var tattoosList = tattoos.Select(async tattoo =>
            {
                var response = _mapper.Map<ResponseShortTatto>(tattoo);
                if (tattoo.CustomerId is not null)
                    response.CustomerId = _sqids.Encode((long)tattoo.CustomerId);
                response.StudioId = _sqids.Encode(tattoo.StudioId);
                response.TattoImage = string.IsNullOrEmpty(tattoo.TattooImage) == false ? await _storageService.GetImage(tattoo.Id.ToString(), tattoo.TattooImage) : null;

                return response;
            });

            var listTask = await Task.WhenAll(tattoosList);

            var tattoosResponse = _mapper.Map<IList<ResponseShortTatto>>(tattoos);

            var response = new ResponseWeeksTattoos() { Tattoos = listTask};

            return response;
        }
    }
}
