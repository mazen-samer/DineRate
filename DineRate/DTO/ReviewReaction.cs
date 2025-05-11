namespace DineRate.DTO
{
    public class ReviewReactionDTO
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } // helpful for UI
        public bool IsLike { get; set; }
    }


    public class CreateReviewReactionDTO
    {
        public int ReviewId { get; set; }
        public bool IsLike { get; set; } // true = like, false = dislike
    }

    public class ReviewReactionSummaryDTO
    {
        public int ReviewId { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
    }


}
