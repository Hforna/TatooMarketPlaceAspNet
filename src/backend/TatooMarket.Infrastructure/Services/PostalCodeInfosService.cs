using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TatooMarket.Domain.Repositories.Services;

namespace TatooMarket.Infrastructure.Services
{
    public class PostalCodeInfosService : IPostalCodeInfosService
    {
        public async Task<Dictionary<string, string>> GetPostalCodeInfos(string postalCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://brasilapi.com.br/api/cep/v1/{postalCode}");

            var response = new Dictionary<string, string>();

            using(var client  = new HttpClient())
            {
                var send = await client.SendAsync(request);
                send.EnsureSuccessStatusCode();

                var toJson = JsonSerializer.Deserialize<Dictionary<string, string>>(await send.Content.ReadAsStringAsync());

                return toJson!;
            }
        }
    }
}
