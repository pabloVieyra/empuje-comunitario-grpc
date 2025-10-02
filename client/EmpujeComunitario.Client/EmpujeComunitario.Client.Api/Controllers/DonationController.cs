using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.Client.Common.Model.DonationDtos;
using EmpujeComunitario.Client.Common.Model.EventDtos;
using EmpujeComunitario.Client.Services.Implementation;
using EmpujeComunitario.Client.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Api.Controllers
{
    public class DonationController : ControllerBase
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
