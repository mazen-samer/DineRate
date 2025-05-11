namespace DineRate.Models;

public class Review
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }

    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<ReviewReaction> Reactions { get; set; } = new List<ReviewReaction>();
}
