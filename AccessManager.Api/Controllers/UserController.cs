using AccessManager.Api.Common;
using AccessManager.Api.DTOs;
using AccessManager.Api.Models;
using AccessManager.Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccessManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            ResponseDTO response = new ResponseDTO();

            if (!ModelState.IsValid)
            {
                response.IsFailure("Invalid user data.", StatusCodes.Status400BadRequest.ToString(), ModelState);

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            
            Result<User> result = _userService.Create(userDTO);

            if(!result.IsSuccess)
            {
                int statusCode = MapErrorToStatusCode(result.ErrorCode);

                response.IsFailure(result.ErrorMessage, statusCode.ToString(), userDTO);
                return StatusCode(statusCode, response);
            }

            response.IsSucess("User created successfully.", StatusCodes.Status201Created.ToString(), result.Data);

            return StatusCode(StatusCodes.Status201Created, response);
        }

        private int MapErrorToStatusCode(ErrorCode errorCode)
        {
            return errorCode switch
            {
                ErrorCode.USER_ALREADY_EXISTS => StatusCodes.Status409Conflict,
                ErrorCode.INVALID_EMAIL => StatusCodes.Status422UnprocessableEntity,
                ErrorCode.USER_NOT_FOUND => StatusCodes.Status404NotFound,
                ErrorCode.DATABASE_ERROR or ErrorCode.EXTERNAL_SERVICE_UNAVAILABLE => StatusCodes.Status503ServiceUnavailable,
                _ => StatusCodes.Status400BadRequest // padrao para outros erros
            };
        }
    }
}
