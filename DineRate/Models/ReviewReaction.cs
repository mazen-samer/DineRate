namespace DineRate.Models;

public class ReviewReaction
{
    public int Id { get; set; }
    public int ReviewId { get; set; }
    public virtual Review Review { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }

    public bool IsLike { get; set; }
}
