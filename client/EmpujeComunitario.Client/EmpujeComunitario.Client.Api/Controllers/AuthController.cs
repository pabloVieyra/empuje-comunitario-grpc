using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.Client.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthManagerServices _authManagerServices;
        public AuthController(IAuthManagerServices authManagerServices)
        {
            _authManagerServices = authManagerServices;
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var result = await _authManagerServices.Login(loginRequest);
            return StatusCode(result.StatusCode, result);
        }

    }
}
