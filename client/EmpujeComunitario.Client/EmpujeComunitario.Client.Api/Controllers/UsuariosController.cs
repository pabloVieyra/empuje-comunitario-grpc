using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.Client.Services.Interface;
using EmpujeComunitario.MessageFlow.Common.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : BaseController
    {
        private readonly IUserManagerServices _userManagerServices;
        private const string errorId = "El Id de usuario es obligatorio.";
        public UsuariosController(IUserManagerServices userManagerServices)
        {
            _userManagerServices = userManagerServices;
        }

        [HttpPost(nameof(CreateUserAsync))]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = await _userManagerServices.CreateUserAsync(request, authorization);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet(nameof(ListUsersAsync))]
        public async Task<IActionResult> ListUsersAsync()
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();
            var response = await _userManagerServices.ListUsersAsync(authorization);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPut(nameof(UpdateUserAsync))]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserRequestDto updateUserRequest)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();
            var response = await _userManagerServices.UpdateUserAsync(updateUserRequest, authorization);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("DisableUserAsync/{id}")]
        public async Task<IActionResult> DisableUserAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                var errorResponse = new BaseObjectResponse<object>()
                    .BadRequestWithoutData(errorId);
                errorResponse.Errors.Add(new ValidationErrorResponse { Field = "id", Message = errorId });
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();
            var response = await _userManagerServices.DisableUserAsync(id, authorization);
            return StatusCode(response.StatusCode, response);
        }


    }
}