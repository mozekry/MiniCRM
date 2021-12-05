using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniCRM.API.Data;
using MiniCRM.API.Dtos;
using MiniCRM.API.Models;

namespace MiniCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
         public AuthController(IAuthRepository repo, IConfiguration config)
        { 
            _config = config;
            _repo = repo;
        }
        //[AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {
            userForRegisterDTO.username = userForRegisterDTO.username.ToLower();
            if (await _repo.UserExists(userForRegisterDTO.username))
                return BadRequest("user already exist");

         
            var userToCreate = new User
            {
                username = userForRegisterDTO.username,
                Created = userForRegisterDTO.Created

            };
           
             var CreatedUser = await _repo.Register(userToCreate, userForRegisterDTO.password);
            // var userToReturn = _mapper.Map<UserForDetailsDto>(CreatedUser);
             //return CreatedAtRoute("GetUser",new {controller = "Users",id = CreatedUser.id},userToReturn);
             return StatusCode(201);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLogin)
        {
            var UserFromRepo = await _repo.Login(userForLogin.username.ToLower(), userForLogin.password);
            if (UserFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,UserFromRepo.id.ToString()),
                new Claim(ClaimTypes.Name,UserFromRepo.username),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //var user = _mapper.Map<UserForListDto>(UserFromRepo);

            return Ok(new 
            { 
                token = tokenHandler.WriteToken(token)
                //user = user
                
             });

        }

    }
}