using EmpujeComunitario.Client.Common.Model.EventDtos;
using EmpujeComunitario.Client.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : BaseController
    {
        private readonly IEventManagerServices _eventManagerServices;
        private const string errorId = "El Id de usuario es obligatorio.";
        public EventController(IEventManagerServices eventManagerServices)
        {
            _eventManagerServices = eventManagerServices;
        }

        [HttpPost(nameof(CreateEventAsync))]
        public async Task<IActionResult> CreateEventAsync([FromBody] CreateEventDto request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = await _eventManagerServices.CreateEventAsync(request, authorization);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut(nameof(UpdateEventAsync))]
        public async Task<IActionResult> UpdateEventAsync([FromBody] EventDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var auth = HttpContext.Items["Authorization"]?.ToString();
            var response = await _eventManagerServices.UpdateEventAsync(request, auth);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete(nameof(DeleteEventAsync))]
        public async Task<IActionResult> DeleteEventAsync([FromBody] DeleteEventDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var auth = HttpContext.Items["Authorization"]?.ToString();
            var response = await _eventManagerServices.DeleteEventAsync(request, auth);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet(nameof(FindEventByIdAsync))]
        public async Task<IActionResult> FindEventByIdAsync([FromQuery] FindEventByIdDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var auth = HttpContext.Items["Authorization"]?.ToString();
            var response = await _eventManagerServices.DeleteEventAsync(request, auth);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet(nameof(ListEventsAsync))]
        public async Task<IActionResult> ListEventsAsync()
        {
            var auth = HttpContext.Items["Authorization"]?.ToString();
            var response = await _eventManagerServices.ListEventsAsync(auth);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost(nameof(AddUserToEventAsync))]
        public async Task<IActionResult> AddUserToEventAsync([FromBody] AddUserToEventDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var auth = HttpContext.Items["Authorization"]?.ToString();
            var response = await _eventManagerServices.AddUserToEventAsync(request, auth);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost(nameof(RemoveUserFromEventAsync))]
        public async Task<IActionResult> RemoveUserFromEventAsync([FromBody] RemoveUserFromEventDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var auth = HttpContext.Items["Authorization"]?.ToString();
            var response = await _eventManagerServices.RemoveUserFromEventAsync(request, auth);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost(nameof(RegisterDonationToEventAsync))]
        public async Task<IActionResult> RegisterDonationToEventAsync([FromBody] RegisterDonationDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(BuildValidationErrorResponse<object>(ModelState));

            var auth = HttpContext.Items["Authorization"]?.ToString();
            var response = await _eventManagerServices.RegisterDonationToEventAsync(request, auth);
            return StatusCode(response.StatusCode, response);
        }


    }
}