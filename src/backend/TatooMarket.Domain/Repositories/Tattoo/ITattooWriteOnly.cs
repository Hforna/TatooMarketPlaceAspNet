using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Tattoo;

namespace TatooMarket.Domain.Repositories.Tattoo
{
    public interface ITattooWriteOnly
    {
        public Task Add(TattooEntity tattoo);
        public void Update(TattooEntity tattoo);
        public void Delete(TattooEntity tattoo);
        public Task AddTattooPlacePrice(TattooPlacePriceEntity tattooPrice);
        public Task AddTattooStylePrice(TattooStylePriceEntity tattooPrice);
        public void UpdateTattooPrice(TattooPlacePriceEntity tattooPrice);
    }
}
