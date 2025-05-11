namespace DineRate.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string CreatedAt { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }  // Add this for identification
        public int RestaurantId { get; set; }  // Add RestaurantId to link the review to a specific restaurant
        public string RestaurantName { get; set; }  // Add the restaurant's name for easier reference
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
    }


    public class CreateReviewDTO
    {
        public int RestaurantId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        // No UserId here — it comes from the token
    }
}
