using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestTask.API.DTOs;
using TestTask.API.Exceptions;
using TestTask.Auth.Interfaces;

namespace TestTask.API.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AccountController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Registers new user.
        /// </summary>
        /// <param name="registerDto">RegisterDto instance</param>
        [HttpPost("register")]
        [SwaggerOperation(
            Description = "Registers new user.",
            Summary = "Registers new user.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _authorizationService.UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken!");
            } 

            return Ok(await _authorizationService.Register(registerDto));
        }

        /// <summary>
        /// Logs into user`s account.
        /// </summary>
        /// <param name="loginDto">LoginDto instance</param>
        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Logs into user`s account.",
            Description = "Logs into user`s account."
            )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            return Ok(await _authorizationService.Login(loginDto));
        }
    }
}
