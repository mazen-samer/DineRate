using AutoMapper;
using DineRate.DTO;
using DineRate.Models;
using DineRate.Repositories.RestaurantRepo;
using Microsoft.AspNetCore.Mvc;

namespace DineRate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        public IRestaurantRepository repo;
        private readonly IMapper mapper;

        public RestaurantController(IRestaurantRepository _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllRestaurants()
        {
            var restaurants = await repo.GetAllRestaurants();
            var restsDTO = mapper.Map<List<RestaurantDTO>>(restaurants);
            return Ok(restsDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetRestaurantById(int id)
        {
            var restaurant = await repo.GetRestaurantById(id);
            if (restaurant == null) return NotFound();
            var restDTO = mapper.Map<RestaurantDTO>(restaurant);
            return Ok(restDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRestaurant(CreateRestaurantDTO rDTO)
        {
            var rest = mapper.Map<Restaurant>(rDTO);
            await repo.AddRestaurant(rest);
            var readDto = mapper.Map<RestaurantDTO>(rest);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = rest.Id }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRestaurant(int id, UpdateRestaurantDTO dto)
        {
            var existing = await repo.GetRestaurantById(id);
            if (existing == null) return NotFound();

            mapper.Map(dto, existing); // Updates fields in-place

            var result = await repo.UpdateRestaurant(id, existing);
            if (!result) return BadRequest(); // unlikely but safe

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRestaurant(int id)
        {
            var result = await repo.DeleteRestaurant(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("by-cuisine/{cuisine}")]
        public async Task<ActionResult> GetByCuisine(string cuisine)
        {
            var filtered = await repo.GetRestaurantsByCuisineType(cuisine);
            var dto = mapper.Map<List<RestaurantDTO>>(filtered);
            return Ok(dto);
        }

        [HttpGet("search/{key}")]
        public async Task<ActionResult> SearchRestaurant(string key)
        {
            var rests = await repo.SearchRestaurants(key);
            var restsDTO = mapper.Map<List<RestaurantDTO>>(rests);
            return Ok(restsDTO);
        }
    }
}
