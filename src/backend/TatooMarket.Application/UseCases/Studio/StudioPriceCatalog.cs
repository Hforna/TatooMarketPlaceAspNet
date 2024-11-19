using AutoMapper;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Studio;
using TatooMarket.Communication.Responses.Studio;
using TatooMarket.Communication.Responses.Tatto;
using TatooMarket.Communication.Responses.Tattoo;
using TatooMarket.Domain.Repositories.Services;
using TatooMarket.Domain.Repositories.StudioRepository;
using TatooMarket.Domain.Repositories.Tattoo;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Studio
{
    public class StudioPriceCatalog : IStudioPriceCatalog
    {
        private readonly ICurrencyExchangeService _exchangeService;
        private readonly IStudioReadOnly _studioRead;
        private readonly ITattooReadOnly _tattooRead;
        private readonly IMapper _mapper;
        private readonly SqidsEncoder<long> _sqidsEncoder;

        public StudioPriceCatalog(ICurrencyExchangeService currencyExchange, IStudioReadOnly studioRead,
            ITattooReadOnly tattooRead, IMapper mapper, SqidsEncoder<long> sqidsEncoder)
        {
            _exchangeService = currencyExchange;
            _studioRead = studioRead;
            _mapper = mapper;
            _sqidsEncoder = sqidsEncoder;
            _tattooRead = tattooRead;
        }

        public async Task<ResponseFullStudioPriceCatalog> Execute(long studioId)
        {
            var studio = await _studioRead.StudioById(studioId);

            if (studio == null)
                throw new StudioException(ResourceExceptMessages.STUDIO_DOESNT_EXISTS);

            var tattooPrices = await _tattooRead.TattooByStudio(studio);

            var currencyTattoo = tattooPrices.Select(d => d.CurrencyType).First();

            var currencyExchange = await _exchangeService.CurrencyConvert(nameof(currencyTattoo));

            var response = new ResponseFullStudioPriceCatalog();
            var catalogsResponse = new List<ResponseStudioPriceCatalogShort>();

            foreach(var value in currencyExchange)
            {
                var responseCurrency = new ResponseStudioPriceCatalogShort() { CurrencyType = value.Key, Catalog = [] };

                foreach(var tattooPrice in tattooPrices)
                {
                    var responseTattooPrice = _mapper.Map<ResponseTattooPrice>(tattooPrice);

                    responseTattooPrice.Price *= value.Value;

                    responseTattooPrice.Id = _sqidsEncoder.Encode(tattooPrice.Id);
                    responseTattooPrice.StudioId = _sqidsEncoder.Encode(tattooPrice.StudioId);

                    responseCurrency.Catalog.Add(responseTattooPrice);
                }
                catalogsResponse.Add(responseCurrency);
            }

            response.Catalogs = catalogsResponse;

            return response;
        }
    }
}
