using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace TatooMarket.Infrastructure.Services
{
    public class CurrencyExchangeService
    {
        private readonly string _apiKey;

        public CurrencyExchangeService(string apiKey) => _apiKey = apiKey;

        public async Task<Dictionary<string, float>> ConvertCurrency(string currency)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://v6.exchangerate-api.com/v6/{_apiKey}/latest/{currency}");

            var response = new Dictionary<string, float>();

            using (var client = new HttpClient())
            {
                var responseRequest = await client.SendAsync(request);
                responseRequest.EnsureSuccessStatusCode();

                var jsonResponse = await responseRequest.Content.ReadAsStringAsync();

                var toJson = JsonSerializer.Deserialize<Dictionary<string,  object>>(jsonResponse);

                if(toJson.ContainsKey("conversion_rates"))
                {
                    var toString = toJson["conversion_rates"].ToString();

                    var currencyJson = JsonSerializer.Deserialize<Dictionary<string,  float>>(toString);

                    foreach(var currencyCurrent in currencyJson)
                    {
                        response[currencyCurrent.Key] = currencyCurrent.Value;
                    }
                }
            }

            return response;
        }
    }
}
