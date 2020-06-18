using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS.Common.Exceptions;
using SS.Users.Application;
using SS.Users.Application.Authenticate;
using SS.Users.Application.Configuration;
using SS.Users.Application.ReadModels;
using SS.Users.Application.RefreshToken;
using SS.Users.Application.Register;
using Swashbuckle.AspNetCore.Annotations;

namespace SS.Api.Controllers
{
    [ApiController, Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserModule _module;
        public UserController(IUserModule module)
        {
            _module = module;
        }

        [HttpPost, Route("register")]
        [SwaggerOperation(Summary = "Register new user")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody]ApiCommands.V1.RegisterUser user)
        {
            var userId = Guid.NewGuid();
            await _module.ExecuteCommand(new UserRegisterCommand()
            {
                Id = userId,
                Email = user.Email,
                Password = user.Password,
                DisplayName = user.DisplayName
            });
            return Created("api/users/",new { id = userId });
        }
        [HttpPost, Route("connect/token")]
        [SwaggerOperation(Summary = "Create credentials for existing user")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Auth([FromBody]ApiCommands.V1.LoginUser user)
        {
            var token = await _module.ExecuteCommandAsync<TokenResponse>(new AuthCommand()
            {
                Email = user.Email,
                Password = user.Password
            });
            return Ok(token);
        }

        [HttpPost, Route("connect/token/refresh")]
        [SwaggerOperation(Summary = "Refresh Token")]
        [ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody]ApiCommands.V1.RefreshToken refreshToken)
        {
            var token = await _module.ExecuteCommandAsync<RefreshTokenResponse>(new RefreshTokenCommand()
            {
                RefreshToken = refreshToken.Token

            });
            return Ok(token);
        }
    }
}