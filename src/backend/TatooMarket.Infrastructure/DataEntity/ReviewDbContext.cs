using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Communication;
using TatooMarket.Domain.Repositories.Review;

namespace TatooMarket.Infrastructure.DataEntity
{
    public class ReviewDbContext : IReviewReadOnly, IReviewWriteOnly
    {
        private readonly ProjectDbContext _dbContext;

        public ReviewDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(ReviewEntity review)
        {
            await _dbContext.Reviews.AddAsync(review);
        }

        public void Delete(ReviewEntity review)
        {
            _dbContext.Reviews.Remove(review);
        }

        public async Task<ReviewEntity?> ReviewById(long Id)
        {
            return await _dbContext.Reviews.SingleOrDefaultAsync(review => review.Id == Id);
        }
    }
}
