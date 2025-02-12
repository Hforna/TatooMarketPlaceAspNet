﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Communication;
using TatooMarket.Domain.Entities.Tattoo;
using TatooMarket.Domain.Enum;
using X.PagedList;

namespace TatooMarket.Domain.Repositories.Tattoo
{
    public interface ITattooReadOnly
    {
        public Task<IList<TattooEntity>> GetRecentTattoss(Studio studio);
        public Task<TattooEntity?> TattooById(long id);
        public Task<IList<ReviewEntity>> GetTattooReviews(TattooEntity tattoo);
        public Task<IList<TattooEntity>> WeeksTattoos(DateTime date);
        public Task<IPagedList<TattooEntity>> GetStudioTattoos(Studio studio, int pageNumbers);
        public Task<TattooPlacePriceEntity?> TattooPlacePriceById(long id);
        public Task<TattooStylePriceEntity?> TattooStylePriceById(long id);
        public Task<TattooPlacePriceEntity?> TattooPlacePriceByStudio(Studio studio);
        public Task<List<TattooPlacePriceEntity>?> TattooPlacesPriceByStudio(Studio studio);
        public Task<List<TattooStylePriceEntity>?> TattooStylesPriceByStudio(Studio studio);
        public Task<TattooStylePriceEntity?> TattooStylePriceByStudio(Studio studio);
        public Task<List<TattooEntity>> TattoosFromStudio(Studio studio);
        public Task<TattooPlacePriceEntity?> TattooPlacePriceByEnum(BodyPlacementEnum bodyPlacement, long studioId);
        public Task<TattooStylePriceEntity?> TattooStylePriceByEnum(TattooStyleEnum styleEnum, long studioId);
        public Task<bool> StudioIsOwnTattoo(Studio studio, long Id);
    }
}
