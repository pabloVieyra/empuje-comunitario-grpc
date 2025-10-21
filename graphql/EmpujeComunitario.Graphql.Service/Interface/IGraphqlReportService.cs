using EmpujeComunitario.Graphql.Common.Model;


namespace EmpujeComunitario.Graphql.Service.Interface
{
    public interface IGraphqlReportService
    {
        Task<IEnumerable<DonationSummaryGroup>> GetSummaryAsync(string category, DateTime? from, DateTime? to, bool? isCancelled);
    }
}
