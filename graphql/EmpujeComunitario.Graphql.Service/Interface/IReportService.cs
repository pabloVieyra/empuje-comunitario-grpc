using EmpujeComunitario.Graphql.Common;
using EmpujeComunitario.Graphql.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.Service.Interface
{
    public interface IReportService
    {
        Task<BaseObjectResponse<ExcelGenerate>> GenerateExcel(FilterDonation filter);
    }
}
