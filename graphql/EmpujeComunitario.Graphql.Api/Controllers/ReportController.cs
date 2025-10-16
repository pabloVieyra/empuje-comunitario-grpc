using EmpujeComunitario.Graphql.Common;
using EmpujeComunitario.Graphql.Service.Implementation;
using EmpujeComunitario.Graphql.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmpujeComunitario.Graphql.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IGraphqlReportService _graphqlReportService;
        private readonly ISoapClientService _soapClientService;
        public ReportController (ISoapClientService soapClientService, IReportService reportService, IGraphqlReportService graphqlReportService)
        {
            _reportService = reportService;
            _graphqlReportService = graphqlReportService;
            _soapClientService = soapClientService;
        }

        [HttpPost(nameof(ReporteDeEjemplo))]
        public IActionResult ReporteDeEjemplo([FromBody] dynamic request)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = BuildValidationErrorResponse<object>(ModelState);
                return BadRequest(errorResponse);
            }

            //var response = reportService.Algo(request);
            //return StatusCode(response.StatusCode, response);
            return Ok();
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
