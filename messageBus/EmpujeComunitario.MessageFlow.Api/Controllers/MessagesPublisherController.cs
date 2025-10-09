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
        //punto 1
        [HttpPost("solicitud-donaciones")]
        public async Task<IActionResult> RequestDonation([FromBody] RequestDonationModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            //var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Exchanges.RoutingKeyRequestDonation, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }

        //punto 2
        [HttpPost("/transferencia-donaciones/{IdOrganizacionSolicitante}")]
        [ProducesResponseType(typeof(TransferDonationModel), 200)]
        public async Task<IActionResult> TransfersDonation([FromBody] TransferDonationModel request, [FromRoute]string IdOrganizacionSolicitante)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(string.Format(Exchanges.RoutingKeyTransferDonation, IdOrganizacionSolicitante), JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }
        //punto 3
        [HttpPost("oferta-donaciones")]
        public async Task<IActionResult> OffersDonations([FromBody] OfferDonationModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Exchanges.RoutingKeyOfferDonation, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }

        //punto 4
        [HttpPost("baja-solicitud-donaciones")]
        public async Task<IActionResult> RequestsCancel([FromBody] CancelRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Exchanges.RoutingKeyRequestCancel, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }


        //punto 5 
        [HttpPost("eventossolidarios")]
        public async Task<IActionResult> EventsSolidary([FromBody] SolidaryEventModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Exchanges.RoutingKeyEventSolidary, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }

        //punto 6 
        [HttpPost("baja-evento-solidario")]
        public async Task<IActionResult> EventsCancel([FromBody] CancelEventModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(Exchanges.RoutingKeyEventCancel, JsonConvert.SerializeObject(request));
            return StatusCode(response.StatusCode, response);
        }

        //punto 7
        [HttpPost("adhesion-evento/{idOrganizador}")]
        public async Task<IActionResult> EventsVolunteer([FromBody] VolunteerAdhesionModel request,[FromRoute] string idOrganizador)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = _rabbitMqService.Publish(string.Format(Exchanges.RoutingKeyEventVolunteer,idOrganizador), JsonConvert.SerializeObject(request));
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
