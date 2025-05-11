namespace DineRate.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string CuisineType { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
