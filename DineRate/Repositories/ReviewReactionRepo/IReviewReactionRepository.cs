using DineRate.Models;

namespace DineRate.Repositories.ReviewReactionRepo
{
    public interface IReviewReactionRepository
    {
        Task<bool> AddOrUpdateReaction(int userId, int reviewId, bool isLike);
        Task<bool> RemoveReaction(int userId, int reviewId);
        Task<List<ReviewReaction>> GetReactionsByReviewId(int reviewId);
        Task<ReviewReaction?> GetUserReaction(int userId, int reviewId);

        Task<int> GetLikeCount(int reviewId);
        Task<int> GetDislikeCount(int reviewId);
    }
}
