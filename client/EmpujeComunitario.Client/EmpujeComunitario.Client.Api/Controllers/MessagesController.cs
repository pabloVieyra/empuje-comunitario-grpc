using EmpujeComunitario.Client.Services.Interface;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : BaseController
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost(nameof(RequestDonation))]
        public async Task<IActionResult> RequestDonation([FromBody] RequestDonationModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var result = await _messageService.RequestDonationAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost(nameof(TransfersDonation))]
        public async Task<IActionResult> TransfersDonation([FromBody] TransferDonationModel request, [FromQuery] string idOrganizacionSolicitante)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var result = await _messageService.TransfersDonationAsync(request, idOrganizacionSolicitante);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost(nameof(OffersDonations))]
        public async Task<IActionResult> OffersDonations([FromBody] OfferDonationModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var result = await _messageService.OffersDonationsAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost(nameof(RequestsCancel))]
        public async Task<IActionResult> RequestsCancel([FromBody] CancelRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var result = await _messageService.RequestsCancelAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost(nameof(EventsSolidary))]
        public async Task<IActionResult> EventsSolidary([FromBody] SolidaryEventModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var result = await _messageService.EventsSolidaryAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost(nameof(EventsCancel))]
        public async Task<IActionResult> EventsCancel([FromBody] CancelEventModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var result = await _messageService.EventsCancelAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost(nameof(EventsVolunteer))]
        public async Task<IActionResult> EventsVolunteer([FromBody] VolunteerAdhesionModel request, [FromQuery] string idOrganizador)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var result = await _messageService.EventsVolunteerAsync(request, idOrganizador);
            return StatusCode(result.StatusCode, result);
        }


    }
}
