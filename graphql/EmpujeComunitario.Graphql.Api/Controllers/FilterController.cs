using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmpujeComunitario.Graphql.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilterController : BaseController
    {
        private readonly IFilterService _filterService;
        public FilterController(IFilterService filterService)
        {
            _filterService = filterService;
        }

        [HttpPost(nameof(SaveQuery))]
        public async Task<IActionResult> SaveQuery([FromBody] QueryFilter request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var userId = HttpContext.Request.Headers["UserId"];
            var response = await _filterService.SaveUserFilter(request, userId);

            return Ok(response);
        }
        [HttpGet(nameof(GetAllFilterAsync))]
        public async Task<IActionResult> GetAllFilterAsync()
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var userId = HttpContext.Request.Headers["UserId"];
            var response = await _filterService.GetAllFilterAsync(userId);

            return Ok(response);
        }

        [HttpDelete(nameof(DeleteFilterAsync))]
        public async Task<IActionResult> DeleteFilterAsync([FromQuery]string name)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var userId = HttpContext.Request.Headers["UserId"];
            var response = await _filterService.DeleteFilterAsync(name,userId);

            return Ok(response);
        }
    }
}
