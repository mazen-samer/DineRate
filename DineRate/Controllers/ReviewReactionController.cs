using System.Security.Claims;
using AutoMapper;
using DineRate.DTO;
using DineRate.Repositories.ReviewReactionRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DineRate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewReactionController : ControllerBase
    {
        private readonly IReviewReactionRepository repo;
        private readonly IMapper mapper;

        public ReviewReactionController(IReviewReactionRepository _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }

        [Authorize]
        [HttpPost("react")]
        public async Task<ActionResult> ReactToReview([FromBody] CreateReviewReactionDTO dto)
        {
            var userId = GetUserIdFromClaims();

            var success = await repo.AddOrUpdateReaction(userId, dto.ReviewId, dto.IsLike);
            if (!success) return BadRequest("Invalid review or user.");

            return Ok(new { message = "Reaction saved." });
        }

        [Authorize]
        [HttpDelete("{reviewId}")]
        public async Task<ActionResult> RemoveReaction(int reviewId)
        {
            var userId = GetUserIdFromClaims();
            var result = await repo.RemoveReaction(userId, reviewId);

            if (!result) return NotFound("Reaction not found.");

            return Ok(new { message = "Reaction removed." });
        }

        [HttpGet("{reviewId}/count")]
        public async Task<ActionResult> GetReactionCounts(int reviewId)
        {
            var likeCount = await repo.GetLikeCount(reviewId);
            var dislikeCount = await repo.GetDislikeCount(reviewId);

            return Ok(new { Likes = likeCount, Dislikes = dislikeCount });
        }

        [Authorize]
        [HttpGet("{reviewId}/me")]
        public async Task<ActionResult> GetMyReaction(int reviewId)
        {
            var userId = GetUserIdFromClaims();
            var reaction = await repo.GetUserReaction(userId, reviewId);

            if (reaction == null) return Ok(null); // User hasn't reacted yet

            var dto = mapper.Map<ReviewReactionDTO>(reaction);
            return Ok(dto);
        }

        private int GetUserIdFromClaims()
        {
            return int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
