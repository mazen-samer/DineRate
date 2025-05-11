using System.Security.Claims;
using AutoMapper;
using DineRate.DTO;
using DineRate.Models;
using DineRate.Repositories.ReviewRepo.DineRate.Repositories.ReviewRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DineRate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository repo;
        private readonly IMapper mapper;

        public ReviewController(IReviewRepository _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }

        [HttpGet("restaurant/{id:int}")]
        public async Task<ActionResult> GetReviewsByRestaurantId(int id)
        {
            var reviews = await repo.GetReviewsByRestaurantId(id);
            var reviewDTOs = mapper.Map<List<ReviewDTO>>(reviews);
            return Ok(reviewDTOs); // Return 200 with the reviews
        }

        [HttpPost]
        [Authorize] // User must be authenticated
        public async Task<ActionResult> AddReview([FromBody] CreateReviewDTO reviewDTO)
        {
            var userId = GetUserIdFromClaims(); // Get user ID from the JWT claim
            var review = mapper.Map<Review>(reviewDTO);
            review.UserId = userId;

            await repo.AddReview(review);
            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, reviewDTO); // Return 201 with the review data
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetReviewById(int id)
        {
            var review = await repo.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }

            var reviewDTO = mapper.Map<ReviewDTO>(review);
            return Ok(reviewDTO);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteReview(int id)
        {
            var userId = GetUserIdFromClaims();

            var result = await repo.DeleteReview(id, userId);
            if (!result) return NotFound();
            return NoContent();
        }


        private int GetUserIdFromClaims()
        {
            return int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

    }
}
