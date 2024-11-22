using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Domain.Repositories.Services
{
    public interface IPostalCodeInfosService
    {
        public Task<Dictionary<string, string>> GetPostalCodeInfos(string postalCode);
    }
}
