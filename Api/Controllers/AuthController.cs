using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Context;
using Core.Extensions;
using Entities.Dtos;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IConfiguration _configuration;
		private readonly BestApiDbContext _db;
		public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, BestApiDbContext bestApiDbContext)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
			_db = bestApiDbContext;
		}
		[Authorize(Roles = "Admin")]
		[HttpPost("admin")]
		public async Task<IActionResult> UserCreate(string userName, string password, string city, string email)
		{
			var appUser = new AppUser
			{
				City = city,
				UserName = userName,
				Email = email

			};
			var identityResult = await _userManager.CreateAsync(appUser, password);
			if (identityResult.Succeeded)
			{
				await _userManager.AddToRoleAsync(appUser, "Admin");
				return Ok(new SuccessResult());
			}
			else
			{
				return BadRequest(new ErrorResult(string.Join(",", identityResult.Errors)));
			}
		}
		[Authorize(Roles = "Admin")]
		[HttpPost("customer")]
		public async Task<IActionResult> CustomerCreate(string userName, string password, string city, string email)
		{
			var appUser = new AppUser
			{
				City = city,
				UserName = userName,
				Email = email

			};
			var identityResult = await _userManager.CreateAsync(appUser, password);
			if (identityResult.Succeeded)
			{
				await _userManager.AddToRoleAsync(appUser, "Customer");
				_db.Wallet.Add(new Wallet { CreatedDate=DateTime.UtcNow.ToTurkishLocal(),IsActive= true,AppUserId=appUser.Id});
				_db.SaveChanges();
				return Ok(new SuccessResult());
			}
			else
			{
				return BadRequest(new ErrorResult(string.Join(",", identityResult.Errors)));
			}
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);
			if (result.Succeeded)
			{
				var appUser = await _userManager.Users.SingleOrDefaultAsync(r => r.UserName == loginDto.UserName);
				var rolesAsync = await _userManager.GetRolesAsync(appUser);

				var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Role, rolesAsync.FirstOrDefault()),
				new Claim("UserId",appUser.Id.ToString())
			};
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:secret"]));
				var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
				var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:accessExpiration"]));
				var token = new JwtSecurityToken(
					_configuration["Jwt:issuer"],
					_configuration["Jwt:audience"],
					claims,
					expires: expires,
					signingCredentials: credentials
				);


				var generateJwtToken = new JwtSecurityTokenHandler().WriteToken(token);
				return Ok(new SuccessDataResult<string>(generateJwtToken));
			}
			else
			{
				return BadRequest(new ErrorResult());
			}

		}
	}
}
