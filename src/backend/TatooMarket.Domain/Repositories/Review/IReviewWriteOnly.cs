using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Communication;

namespace TatooMarket.Domain.Repositories.Review
{
    public interface IReviewWriteOnly
    {
        public Task Add(ReviewEntity review);
        public void Delete(ReviewEntity review);
    }

}
