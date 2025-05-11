namespace DineRate.DTO
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string CuisineType { get; set; }
        public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
    }

    public class CreateRestaurantDTO
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string CuisineType { get; set; }
    }

    public class UpdateRestaurantDTO
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string CuisineType { get; set; }
    }
}
