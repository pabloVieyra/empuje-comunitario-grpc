using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.Client.Common.Model.DonationDtos;
using EmpujeComunitario.Client.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;
        private const string errorId = "El Id de usuario es obligatorio.";
        public MessagesController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost(nameof(PublishDonation))]
        public async Task<IActionResult> PublishDonation([FromBody] dynamic request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = await _rabbitMqService.Publish(request, authorization);
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
