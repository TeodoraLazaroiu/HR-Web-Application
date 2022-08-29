using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;
using WebAPI.Repository.Interfaces;
using WebAPI.Services;

namespace API.Controllers
{
	[AllowAnonymous]
	[Route("api/users")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAuthenticationService _service;

		public UsersController(IUnitOfWork unitOfWork, IAuthenticationService service)
		{
			_unitOfWork = unitOfWork;
			_service = service;
		}

		[HttpPost]
		[Route("authentication")]
		public async Task<IActionResult> Authenticate(UserLoginDTO user)
		{
			Token? token;
			try
			{
				token = await _service.Authenticate(user);
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
				newUser = await _service.Register(user);
				await _unitOfWork.Users.Create(newUser);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Ok();
		}
	}
}



