using ClosedXML.Excel;
using EmpujeComunitario.Graphql.Common;
using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.DataAccess.Interface;
using EmpujeComunitario.Graphql.Service.Interface;
using OfficeOpenXml;
using System.ComponentModel;
using LicenseContext = OfficeOpenXml.LicenseContext;


namespace EmpujeComunitario.Graphql.Service.Implementation
{
    public class ReportService : IReportService
    {
        private readonly IDonationRepository _donationRepository;
        public ReportService(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }
        public async Task<BaseObjectResponse<ExcelGenerate>> GenerateExcel(FilterDonation filter)
        {
            var response = new BaseObjectResponse<ExcelGenerate>();
            try
            {
                // Traemos los datos
                var result = await _donationRepository.GetDonationExcel(filter.category, filter.from, filter.to, filter.isCancelled);

                using var workbook = new XLWorkbook();

                foreach (var group in result)
                {
                    var ws = workbook.Worksheets.Add(group.Category);

                    // Encabezado
                    ws.Cell(1, 1).Value = "Fecha de Alta";
                    ws.Cell(1, 2).Value = "Descripcion";
                    ws.Cell(1, 3).Value = "Cantidad";
                    ws.Cell(1, 4).Value = "Eliminado";
                    ws.Cell(1, 5).Value = "Usuario Alta";
                    ws.Cell(1, 6).Value = "Usuario Modificación";

                    int row = 2;
                    foreach (var item in group.Items)
                    {
                        ws.Cell(row, 1).Value = item.CreatedAt?.ToString("yyyy-MM-dd");
                        ws.Cell(row, 2).Value = item.Description;
                        ws.Cell(row, 3).Value = item.Quantity;
                        ws.Cell(row, 4).Value = group.IsCancelled ? "Sí" : "No";
                        ws.Cell(row, 5).Value = item.User; // Usuario (Email o Id, según tengas)
                        ws.Cell(row, 6).Value = "";        // Usuario Modificación
                        row++;
                    }

                    // Opcional: Auto-ajustar columnas
                    ws.Columns().AdjustToContents();
                }

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);

                response.OkWithData(new ExcelGenerate
                {
                    Content = stream.ToArray(),
                    FileName = "Donaciones.xlsx"
                });

                return response;
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData(ex.Message);
            }
        }
    }
}
