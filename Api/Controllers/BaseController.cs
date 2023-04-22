using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaseController : ControllerBase
	{
		[HttpGet("GetUserId")]
		[Authorize]
		public long GetUserId()
		{
			long.TryParse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value,
				out var userId);
			return userId;
		}
	}
}
