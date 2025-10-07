using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.Client.Common.Model.DonationDtos;
using EmpujeComunitario.Client.Common.Model.MessagesRabbitMq;
using EmpujeComunitario.Client.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using Newtonsoft;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        [HttpPost("request/donations")]
        public async Task<IActionResult> RequestDonation([FromBody] RequestDonationModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            //var authorization = HttpContext.Items["Authorization"]?.ToString();
            
            var response = _rabbitMqService.Publish(Constants.ExchangeRequestDonation, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("offers/donations")]
        public async Task<IActionResult> OffersDonations([FromBody] OfferDonationModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Constants.ExchangeOffersDonations, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("events/solidary")]
        public async Task<IActionResult> EventsSolidary([FromBody] SolidaryEventModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Constants.ExchangeEventsSolidary, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("events/volunteer")]
        public async Task<IActionResult> EventsVolunteer([FromBody] VolunteerAdhesionModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Constants.ExchangeEventsVolunteer, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("transfers/confirm")]
        public async Task<IActionResult> TransfersConfirm([FromBody] TransferDonationModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Constants.ExchangeTransfersConfirm, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("requests/cancel")]
        public async Task<IActionResult> RequestsCancel([FromBody] CancelRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Constants.ExchangeRequestsCancel, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("events/cancel")]
        public async Task<IActionResult> EventsCancel([FromBody] CancelEventModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Constants.ExchangeEventsCancel, JsonConvert.SerializeObject(request));
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
