using Microsoft.AspNetCore.Mvc;
using tsm_back.Services;
using tsm_back.RequestModels;
using LoginRequest = tsm_back.RequestModels.LoginRequest;
using RegisterRequest = tsm_back.RequestModels.RegisterRequest;
using Microsoft.AspNetCore.Authorization;

namespace tsm_back.Controllers
{
    [Route("TSM/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var userDTO = await _userService.Login(loginRequest.Login, loginRequest.Password);
                return Ok(userDTO);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPost("reg")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest regRequest)
        {
            try
            {
                var userDTO = await _userService.Register(regRequest.NickName, regRequest.Login, regRequest.Password);
                return Ok(userDTO);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPatch("nickname")]
        [Authorize]
        public async Task<IActionResult> UpdateUserName([FromHeader] int userID, [FromBody] UserUpdateRequest userUpdateRequest)
        {
            try
            {
                await _userService.UpdateUserName(userID, userUpdateRequest.NickName);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet("validate-token")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            return Ok(new {message = "Токен актуален"});
        }
    }
}
