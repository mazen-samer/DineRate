namespace DineRate.Repositories.ReviewRepo
{
    using global::DineRate.Models;

    namespace DineRate.Repositories.ReviewRepo
    {
        public interface IReviewRepository
        {
            Task<List<Review>> GetReviewsByRestaurantId(int restaurantId);
            Task<Review?> GetReviewById(int id);
            Task<bool> AddReview(Review review);
            Task<bool> DeleteReview(int id, int userId); // only allow user to delete their own review
        }
    }


}
