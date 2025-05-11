
using DineRate.Models;
namespace DineRate.Repositories.RestaurantRepo
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> GetAllRestaurants();
        Task<List<Restaurant>> SearchRestaurants(string keyword);
        Task<List<Restaurant>> GetRestaurantsByCuisineType(string cuisineType);
        Task<Restaurant?> GetRestaurantById(int id);
        Task AddRestaurant(Restaurant restaurant);
        Task<bool> UpdateRestaurant(int id, Restaurant restaurant);
        Task<bool> DeleteRestaurant(int id);
    }

}
