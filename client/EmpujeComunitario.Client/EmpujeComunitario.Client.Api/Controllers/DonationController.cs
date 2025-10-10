using EmpujeComunitario.Client.Common.Model.DonationDtos;
using EmpujeComunitario.Client.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DonationController : BaseController
    {
        private readonly IDonationManagerService _donationManagerService;
        private const string errorId = "El Id de usuario es obligatorio.";
        public DonationController(IDonationManagerService donationManagerService)
        {
            _donationManagerService = donationManagerService;
        }

        [HttpPost(nameof(CreateDonationInventoryAsync))]
        public async Task<IActionResult> CreateDonationInventoryAsync([FromBody] DonationInventoryCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = await _donationManagerService.CreateDonationInventoryAsync(request, authorization);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch(nameof(UpdateDonationInventoryAsync))]
        public async Task<IActionResult> UpdateDonationInventoryAsync([FromBody] DonationInventoryUpdateDto request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = await _donationManagerService.UpdateDonationInventoryAsync(request, authorization);
            return StatusCode(response.StatusCode, response);
        }


        [HttpDelete(nameof(DeleteDonationInventoryAsync))]
        public async Task<IActionResult> DeleteDonationInventoryAsync([FromBody] DonationInventoryDeleteDto request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = await _donationManagerService.DeleteDonationInventoryAsync(request, authorization);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet(nameof(ListDonationInventoryAsync))]
        public async Task<IActionResult> ListDonationInventoryAsync()
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var authorization = HttpContext.Items["Authorization"]?.ToString();

            var response = await _donationManagerService.ListDonationInventoryAsync(authorization);
            return StatusCode(response.StatusCode, response);
        }
    }
}
