using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmpujeComunitario.Graphql.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : BaseController
    {
        private readonly ISoapClientService _clientSoap;
        public OrganizationController(ISoapClientService clientSoap)
        {
            _clientSoap = clientSoap;

        }
        [HttpPost(nameof(GetAllPresident))]
        public async Task<IActionResult> GetAllPresident(List<string> request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var response = await _clientSoap.GetAllPresident(request);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost(nameof(GetAllOrganization))]
        public async Task<IActionResult> GetAllOrganization(List<string> request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var response = await _clientSoap.GetAllOrganization(request);

            return StatusCode(response.StatusCode, response);
        }
    }
}
