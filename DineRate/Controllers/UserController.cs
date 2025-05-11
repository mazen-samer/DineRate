using AutoMapper;
using DineRate.DTO;
using DineRate.Models;
using DineRate.Repositories.UserRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DineRate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository repo;
        TokenService tokenService;
        IMapper mapper;

        public UserController(IUserRepository _repo, TokenService _tokenService, IMapper _mapper)
        {
            repo = _repo;
            tokenService = _tokenService;
            mapper = _mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO dto)
        {
            var user = mapper.Map<User>(dto);
            var success = await repo.RegisterAsync(user);
            if (!success) return BadRequest("Username or email already exists.");

            var token = tokenService.CreateToken(user);
            var response = mapper.Map<AuthResponseDTO>(user);
            response.Token = token;

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO dto)
        {
            var existing = await repo.AuthenticateAsync(dto.Username, dto.Password);
            if (existing == null) return Unauthorized("Invalid Username/Password");

            var token = tokenService.CreateToken(existing);
            var response = mapper.Map<AuthResponseDTO>(existing);
            response.Token = token;

            return Ok(response);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var user = await repo.GetByIdAsync(id);
            if (user == null) return NotFound();
            var response = mapper.Map<UserDTO>(user);
            return Ok(response);
        }
    }
}
