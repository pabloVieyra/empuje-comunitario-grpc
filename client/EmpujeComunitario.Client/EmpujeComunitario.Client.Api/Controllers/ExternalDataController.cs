using EmpujeComunitario.Client.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExternalDataController : BaseController
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
    }
}
