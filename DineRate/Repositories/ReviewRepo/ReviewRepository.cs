using DineRate.Data;
using DineRate.Models;
using DineRate.Repositories.ReviewRepo.DineRate.Repositories.ReviewRepo;
using Microsoft.EntityFrameworkCore;

namespace DineRate.Repositories.ReviewRepo
{
    public class ReviewRepository : IReviewRepository
    {
        public AppDbContext context;
        public ReviewRepository(AppDbContext _context)
        {
            context = _context;
        }


        public async Task<bool> AddReview(Review review)
        {
            var restaurant = await context.Restaurants.FindAsync(review.RestaurantId);
            if (restaurant == null) return false;

            var user = await context.Users.FindAsync(review.UserId);
            if (user == null) return false;

            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReview(int id, int userId)
        {
            var review = await context.Reviews.FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
            if (review == null) return false;

            context.Reviews.Remove(review);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Review?> GetReviewById(int id)
        {
            return await context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Review>> GetReviewsByRestaurantId(int restaurantId)
        {
            return await context.Reviews
                            .Where(r => r.RestaurantId == restaurantId)
                            .ToListAsync();
        }
    }
}
