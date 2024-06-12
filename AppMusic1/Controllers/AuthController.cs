using AppMusic1.DataTransferObjects;
using AppMusic1.Exceptions;
using AppMusic1.Models;
using AppMusic1.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppMusic1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly Jwt _jwt;
        private readonly RepositoryContext _context;

        public AuthController(IOptions<Jwt> jwt, RepositoryContext context)
        {
            _jwt = jwt.Value;
            _context = context;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TokenDto>> Login([FromBody]UserDto userdto)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync
                (u => u.Username == userdto.Username && 
                u.Password == userdto.Password);
            if (user is null) 
                throw new UnAuthorizedException($"Unauthorized Request!");

            var token = GenerateJwtToken(user);
            return Ok(new TokenDto { Token=token});
        }
        private string GenerateJwtToken(User user)
        {
            if (_jwt.Key is null)
                throw new Exception("Key Couldn't be empty.");

            var securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var claimArray = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username!),
                new Claim(ClaimTypes.Role,user.Role!)
            };
            var token = new JwtSecurityToken(
                _jwt.Issuer,
                _jwt.Audience,
                claimArray,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
