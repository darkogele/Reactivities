using System.Security.Claims;
using Application.Account;
using Application.Account.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager,TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return HandleResult(await Mediator.Send(new Login.Query(loginDto)));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            return HandleResult(await Mediator.Send(new Register.Command(registerDto)));
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            return HandleResult(await Mediator.Send(new GetCurrentUser.Query(User)));

            //var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            //return Ok(await CreateUserObjectAsync(user));
        }

        //private async Task<UserDto> CreateUserObjectAsync(AppUser user)
        //{
        //    return new UserDto
        //    {
        //        DisplayName = user.DisplayName,
        //        Image = null,
        //        Token = await _tokenService.CreateTokenAsync(user),
        //        UserName = user.UserName
        //    };
        //}
    }
}