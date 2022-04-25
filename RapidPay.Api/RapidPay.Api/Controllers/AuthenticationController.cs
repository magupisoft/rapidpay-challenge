using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidPay.AuthenticationService;
using RapidPay.Domain.Requests;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RapidPay.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest model)
        {
            var (authenticatedUser, user) = await _userService.AuthenticateAsync(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Username or password is incorrect" });

            return Ok(authenticatedUser);
        }
    }
}
