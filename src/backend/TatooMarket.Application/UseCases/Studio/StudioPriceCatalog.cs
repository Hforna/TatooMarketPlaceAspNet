using AutoMapper;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Studio;
using TatooMarket.Communication.Enums;
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

            var tattooPlacePrices = await _tattooRead.TattooPlacesPriceByStudio(studio);

            var tattooStylePrices = await _tattooRead.TattooStylesPriceByStudio(studio);

            var currencyTattoo = tattooPlacePrices.Select(d => d.CurrencyType).First();

            var acceptedCurrencys = Enum.GetValues(typeof(CurrencyEnum)).Cast<CurrencyEnum>().Select(d => d.ToString()).ToList();

            var currencyExchange = await _exchangeService.CurrencyConvert(nameof(currencyTattoo));

            var currencyAcceptedExchange = currencyExchange
            .Where(d => acceptedCurrencys.Contains(d.Key) &&
            tattooPlacePrices.Select(d => d.CurrencyType.ToString()).Contains(d.Key) == false);

            var response = new ResponseFullStudioPriceCatalog();
            var catalogsResponse = new List<ResponseStudioPriceCatalogShort>();

            foreach(var value in currencyAcceptedExchange)
            {
                var responseCurrency = new ResponseStudioPriceCatalogShort() { CurrencyType = value.Key, PlaceCatalog = [], StyleCatalog = [] };

                foreach(var tattooPrice in tattooPlacePrices)
                {
                    if (value.Key == tattooPrice.CurrencyType.ToString())
                        break;

                    var responseTattooPrice = _mapper.Map<ResponseTattooPlacePrice>(tattooPrice);

                    responseTattooPrice.Price *= value.Value;

                    responseTattooPrice.Id = _sqidsEncoder.Encode(tattooPrice.Id);
                    responseTattooPrice.StudioId = _sqidsEncoder.Encode(tattooPrice.StudioId);

                    responseCurrency.PlaceCatalog.Add(responseTattooPrice);
                }
                foreach(var tattooPrice in tattooStylePrices)
                {
                    if (value.Key == tattooPrice.CurrencyType.ToString())
                        break;

                    var responseStyleTattooPrice = _mapper.Map<ResponseTattooStylePrice>(tattooPrice);

                    responseStyleTattooPrice.Price *= value.Value;

                    responseStyleTattooPrice.Id = _sqidsEncoder.Encode(tattooPrice.Id);
                    responseStyleTattooPrice.StudioId = _sqidsEncoder.Encode(tattooPrice.StudioId);

                    responseCurrency.StyleCatalog.Add(responseStyleTattooPrice);
                }
                catalogsResponse.Add(responseCurrency);
            }

            response.Catalogs = catalogsResponse;

            return response;
        }
    }
}
