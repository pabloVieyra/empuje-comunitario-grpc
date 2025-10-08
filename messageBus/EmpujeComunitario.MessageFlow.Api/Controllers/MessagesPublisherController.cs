using EmpujeComunitario.MessageFlow.Common.Constants;
using EmpujeComunitario.MessageFlow.Common.Model;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using EmpujeComunitario.MessageFlow.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace EmpujeComunitario.MessageFlow.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesPublisherController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;
        public MessagesPublisherController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost(nameof(RequestDonation))]
        public async Task<IActionResult> RequestDonation([FromBody] RequestDonationModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            //var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Exchanges.ExchangeRequestDonation, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost(nameof(OffersDonations))]
        public async Task<IActionResult> OffersDonations([FromBody] OfferDonationModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Exchanges.ExchangeOffersDonations, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost(nameof(EventsSolidary))]
        public async Task<IActionResult> EventsSolidary([FromBody] SolidaryEventModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Exchanges.ExchangeEventsSolidary, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost(nameof(EventsVolunteer))]
        public async Task<IActionResult> EventsVolunteer([FromBody] VolunteerAdhesionModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Exchanges.ExchangeEventsVolunteer, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost(nameof(TransfersConfirm))]
        public async Task<IActionResult> TransfersConfirm([FromBody] TransferDonationModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Exchanges.ExchangeTransfersConfirm, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost(nameof(RequestsCancel))]
        public async Task<IActionResult> RequestsCancel([FromBody] CancelRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Exchanges.ExchangeRequestsCancel, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost(nameof(EventsCancel))]
        public async Task<IActionResult> EventsCancel([FromBody] CancelEventModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Exchanges.ExchangeEventsCancel, JsonConvert.SerializeObject(request));
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
