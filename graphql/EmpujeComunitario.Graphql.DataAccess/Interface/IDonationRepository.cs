using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.DataAccess.Entities;

namespace EmpujeComunitario.Graphql.DataAccess.Interface
{
    public interface IDonationRepository
    {
        Task<IEnumerable<DonationSummaryGroup>> GetDonationSummaryAsync(
                string category = null,
                DateTime? from = null,
                DateTime? to = null,
                bool? isCancelled = null);
        Task<IEnumerable<DonationSummaryGroup>> GetDonationExcel(
            string category = null,
            DateTime? from = null,
            DateTime? to = null,
            bool? isCancelled = null);


    }
}
