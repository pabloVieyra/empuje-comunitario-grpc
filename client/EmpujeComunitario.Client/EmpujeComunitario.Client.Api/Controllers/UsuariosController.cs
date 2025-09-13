using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.Client.Services.Interface;
using Grpc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUserManagerServices _userManagerServices;
        public UsuariosController(IUserManagerServices userManagerServices)
        {
            _userManagerServices = userManagerServices;
        }

        [HttpPost(nameof(CreateUserAsync))]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var response = await _userManagerServices.CreateUserAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost(nameof(ListUsersAsync))]
        //[SwaggerOperation(Summary = "Lista usuarios según filtros")]
        //[SwaggerResponse(200, "Lista de usuarios obtenida correctamente", typeof(ListUsersResponse))]
        //[SwaggerResponse(400, "Errores de validación")]
        public async Task<IActionResult> ListUsersAsync(ListUsersRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var response = await _userManagerServices.ListUsersAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        private BaseObjectResponse<T> BuildValidationErrorResponse<T>(ModelStateDictionary modelState)
        {
            var validationErrors = new List<ValidationErrorResponse>();
            foreach (var key in modelState.Keys)
            {
                var value = modelState[key];
                foreach (var error in value.Errors)
                {
                    validationErrors.Add(new ValidationErrorResponse
                    {
                        Field = key,
                        Message = error.ErrorMessage
                    });
                }
            }

            var response = new BaseObjectResponse<T>
            {
                StatusCode = 400,
                Message = "Errores de validación",
                Errors = validationErrors
            };

            return response;
        }
    }
}