using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;
using WebAPI.Repository.Interfaces;

namespace API.Controllers
{
	[AllowAnonymous]
	[Route("api/users")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUnitOfWork unitOfWork;

		public UsersController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		[HttpPost]
		[Route("authentication")]
		public async Task<IActionResult> Authenticate(UserLoginDTO user)
		{
			Token? token;
			try
			{
				token = await unitOfWork.Users.GetTokenForUser(user);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			if (token == null)
			{
				return Unauthorized();
			}

			return Ok(token);
		}

		[HttpPost]
		[Route("register")]
		public async Task<ActionResult<UserRegisterDTO>> PostUser(UserRegisterDTO user)
		{
			User newUser;

			try
			{
				newUser = await unitOfWork.Users.GetRegisteredUser(user);
				await unitOfWork.Users.Create(newUser);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Ok();
		}
	}
}



