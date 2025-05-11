using DineRate.Data;
using DineRate.Models;
using Microsoft.EntityFrameworkCore;

namespace DineRate.Repositories.RestaurantRepo
{
    public class RestaurantRepository : IRestaurantRepository
    {
        public AppDbContext context;

        public RestaurantRepository(AppDbContext _context)
        {
            context = _context;
        }


        public async Task AddRestaurant(Restaurant restaurant)
        {
            await context.Restaurants.AddAsync(restaurant);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteRestaurant(int id)
        {
            Restaurant r = await context.Restaurants.FindAsync(id);
            if (r == null) return false;
            context.Restaurants.Remove(r);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Restaurant>> GetAllRestaurants()
        {
            return await context.Restaurants.ToListAsync();
        }

        public async Task<List<Restaurant>> GetRestaurantsByCuisineType(string cuisineType)
        {
            return await context.Restaurants
                .Where(r => r.CuisineType.ToLower() == cuisineType.ToLower())
                .ToListAsync();
        }


        public async Task<List<Restaurant>> SearchRestaurants(string keyword)
        {
            return await context.Restaurants
                .Where(r => r.Name.ToLower().Contains(keyword.ToLower()))
                .ToListAsync();
        }


        public async Task<Restaurant?> GetRestaurantById(int id)
        {
            return await context.Restaurants.FindAsync(id);
        }

        public async Task<bool> UpdateRestaurant(int id, Restaurant updatedRestaurant)
        {
            if (updatedRestaurant.Id != id)
                return false; // return false if there's a mismatch

            var exists = await context.Restaurants.FindAsync(id);
            if (exists == null)
                return false; // Restaurant doesn't exist in DB

            context.Entry(updatedRestaurant).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

    }
}
