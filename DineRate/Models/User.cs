namespace DineRate.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual ICollection<ReviewReaction> ReviewReactions { get; set; } = new List<ReviewReaction>();
}
