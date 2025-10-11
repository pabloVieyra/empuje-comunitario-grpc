using EmpujeComunitario.MessageFlow.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmpujeComunitario.MessageFlow.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExternalDataController : ControllerBase
    {
        private readonly IExternalDataService _externalDataService;
        public ExternalDataController(IExternalDataService externalDataService)
        {
            _externalDataService = externalDataService;
        }

        [HttpGet(nameof(GetAllEvents))]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _externalDataService.GetAllEvents(); 
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("GetAllVolunteerByEvent/{eventId}")]
        public async Task<IActionResult> GetAllVolunteerByEvent([FromRoute] string eventId)
        {
            var result = await _externalDataService.GetAllVolunteerByEvent(eventId);

            return StatusCode(result.StatusCode, result);
        }



        [HttpGet(nameof(GetAllOfferDonation))]
        public async Task<IActionResult> GetAllOfferDonation()
        {
            var result = await _externalDataService.GetAllOfferDonation();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet(nameof(GetAllRequestsDonation))]
        public async Task<IActionResult> GetAllRequestsDonation()
        {
            var result = await _externalDataService.GetAllRequestsDonation();
            return StatusCode(result.StatusCode, result);
        }
    }
}
