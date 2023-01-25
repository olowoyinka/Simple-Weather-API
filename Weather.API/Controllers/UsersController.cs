using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weather.Domain.Interfaces;
using Weather.Core.Requests;
using Weather.Core.Responses;
using Weather.Core.Utilities;

namespace Weather.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private ILogger<UsersController> _logger { get; }
        public IUserService UserService { get; }

        public UsersController(ILogger<UsersController> logger,
                               IUserService userService)
        {
            this._logger = logger;
            this.UserService = userService;
        }

        /// <summary>register new account endpoint</summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("v1/users/register")]
        [ProducesResponseType(typeof(DataResponseDTO<LoginResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            try
            {
                _logger.LogInformation("UsersController Register method called");

                if (!ModelState.IsValid)
                {
                    return BadRequest(new ModelStateErrorResponseDTO(HttpStatusCode.BadRequest,
                        ModelState));
                }

                var newRegister = await this.UserService.Register(model);

                if (newRegister.status.Equals(HttpStatusCode.NotFound))
                {
                    return NotFound(new ErrorResponseDTO(HttpStatusCode.NotFound,
                                                              new string[] { newRegister.response }));
                }

                if (newRegister.status.Equals(HttpStatusCode.BadRequest))
                {
                    return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { newRegister.response }));
                }

                return Ok(newRegister.response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occur while register");

                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                    new string[] { OtherConstants.DEFAULT_ERROR_MESSAGE }));
            }
        }



        /// <summary>login endpoint</summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("v1/users/login")]
        [ProducesResponseType(typeof(DataResponseDTO<LoginResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            try
            {
                _logger.LogInformation("UsersController Login method called");

                if (!ModelState.IsValid)
                {
                    return BadRequest(new ModelStateErrorResponseDTO(HttpStatusCode.BadRequest,
                        ModelState));
                }

                var userLogin = await this.UserService.Login(model);

                if (userLogin.status.Equals(HttpStatusCode.NotFound))
                {
                    return NotFound(new ErrorResponseDTO(HttpStatusCode.NotFound,
                                                              new string[] { userLogin.response }));
                }

                if (userLogin.status.Equals(HttpStatusCode.BadRequest))
                {
                    return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { userLogin.response }));
                }

                return Ok(userLogin.response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occur while login");

                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                    new string[] { OtherConstants.DEFAULT_ERROR_MESSAGE }));
            }
        }


        /// <summary>refresh token endpoint</summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("v1/users/refresh-token")]
        [ProducesResponseType(typeof(DataResponseDTO<LoginResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO model)
        {
            try
            {
                _logger.LogInformation("UsersController RefreshToken method called");

                if (!ModelState.IsValid)
                {
                    return BadRequest(new ModelStateErrorResponseDTO(HttpStatusCode.BadRequest,
                        ModelState));
                }

                var userAccount = await this.UserService.RefreshToken(GetEmail(), model);

                if (userAccount.status.Equals(HttpStatusCode.NotFound))
                {
                    return NotFound(new ErrorResponseDTO(HttpStatusCode.NotFound,
                                                              new string[] { userAccount.response }));
                }

                if (userAccount.status.Equals(HttpStatusCode.BadRequest))
                {
                    return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { userAccount.response }));
                }

                return Ok(userAccount.response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occur while refresh token");

                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                    new string[] { OtherConstants.DEFAULT_ERROR_MESSAGE }));
            }
        }


        /// <summary>get user details endpoint</summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <returns></returns>
        [HttpGet("v1/users")]
        [ProducesResponseType(typeof(DataResponseDTO<UserResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                _logger.LogInformation("UsersController GetUser method called");

                var userAccount = await this.UserService.GetUser(GetEmail());

                if (userAccount.status.Equals(HttpStatusCode.NotFound))
                {
                    return NotFound(new ErrorResponseDTO(HttpStatusCode.NotFound,
                                                              new string[] { userAccount.response }));
                }

                if (userAccount.status.Equals(HttpStatusCode.BadRequest))
                {
                    return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { userAccount.response }));
                }

                return Ok(new DataResponseDTO<UserResponseDTO>(userAccount.response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occur while get user");

                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                    new string[] { OtherConstants.DEFAULT_ERROR_MESSAGE }));
            }
        }

        private string GetEmail()
        {
            return HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
        }
    }
}