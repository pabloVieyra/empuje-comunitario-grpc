using EmpujeComunitario.Graphql.Common;
using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.Service.Implementation;
using EmpujeComunitario.Graphql.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmpujeComunitario.Graphql.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;

        }
        [HttpPost(nameof(GenerateExcel))]
        public async Task<IActionResult> GenerateExcel([FromBody] FilterDonation request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }
            var userId = HttpContext.Request.Headers["UserId"];
            var response = await _reportService.GenerateExcel(request);

            return File(
                response.Data.Content,                     // contenido del Excel
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", // MIME type
                response.Data.FileName                      // nombre del archivo
            );
        }
    }
}
