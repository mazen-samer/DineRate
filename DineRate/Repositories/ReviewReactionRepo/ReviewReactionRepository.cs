using DineRate.Data;
using DineRate.Models;
using Microsoft.EntityFrameworkCore;

namespace DineRate.Repositories.ReviewReactionRepo
{
    public class ReviewReactionRepository : IReviewReactionRepository
    {
        public AppDbContext context;
        public ReviewReactionRepository(AppDbContext _context)
        {
            context = _context;
        }


        public async Task<bool> AddOrUpdateReaction(int userId, int reviewId, bool isLike)
        {
            var existing = await context.ReviewReactions
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ReviewId == reviewId);

            if (existing != null)
            {
                if (existing.IsLike == isLike)
                    return true; // No change needed

                existing.IsLike = isLike;
                context.ReviewReactions.Update(existing);
            }
            else
            {
                var review = await context.Reviews.FindAsync(reviewId);
                var user = await context.Users.FindAsync(userId);

                if (review == null || user == null) return false;

                var reaction = new ReviewReaction
                {
                    UserId = userId,
                    ReviewId = reviewId,
                    IsLike = isLike
                };

                await context.ReviewReactions.AddAsync(reaction);
            }

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetDislikeCount(int reviewId)
        {
            return await context.ReviewReactions
                .CountAsync(r => r.ReviewId == reviewId && !r.IsLike);
        }

        public async Task<int> GetLikeCount(int reviewId)
        {
            return await context.ReviewReactions
                .CountAsync(r => r.ReviewId == reviewId && r.IsLike);
        }

        public async Task<List<ReviewReaction>> GetReactionsByReviewId(int reviewId)
        {
            return await context.ReviewReactions
                .Where(r => r.ReviewId == reviewId)
                .ToListAsync();
        }

        public async Task<ReviewReaction?> GetUserReaction(int userId, int reviewId)
        {
            return await context.ReviewReactions
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ReviewId == reviewId);
        }


        public async Task<bool> RemoveReaction(int userId, int reviewId)
        {
            var reaction = await context.ReviewReactions.FirstOrDefaultAsync(r => r.UserId == userId && r.ReviewId == reviewId);

            if (reaction == null) return false;

            context.ReviewReactions.Remove(reaction);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
