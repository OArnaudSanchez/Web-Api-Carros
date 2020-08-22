using CarrosData.Models;
using CarrosServices.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiCarros.Controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController:ControllerBase
    {

        private readonly IConfiguration _configuration;
        public IAccountUserRepository _accountUser { get; }

        public AccountController(IAccountUserRepository accountUser,
            IConfiguration configuration)
        {
            _accountUser = accountUser;
            _configuration = configuration;
        }

        

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInfo user)
        {
            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser { UserName = user.Email, Email = user.Email };

                var result = await _accountUser.Create(newUser, user);

                if (result)
                {
                    return BuildToken(user);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserInfo user)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountUser.Login(user);

                if (result)
                {
                    return BuildToken(user);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult BuildToken(UserInfo user)
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret-Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var fechaExpiracion = DateTime.UtcNow.AddHours(24);

            JwtSecurityToken token = new JwtSecurityToken
                (
                    issuer: "MyDomain.com",
                    audience: "MyDomain.com",
                    claims: claims,
                    expires: fechaExpiracion,
                    signingCredentials: creds
                );

            return Ok(new
            {
                 Token = new JwtSecurityTokenHandler().WriteToken(token),
                 Expiration = fechaExpiracion
            });
        }
    }
}
