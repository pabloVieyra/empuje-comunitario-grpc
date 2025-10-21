using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.DataAccess.Entities;

namespace EmpujeComunitario.Graphql.DataAccess.Interface
{
    public interface IDonationRepository
    {
        Task AddDonationRequestAsync(DonationRequest request);
        Task<DonationRequest> GetByIdAsync(Guid requestId);
        Task CancelDonationRequestAsync(Guid requestId);
        Task<List<DonationRequest>> GetAllRequestsAsync();


            Task<IEnumerable<DonationSummaryGroup>> GetDonationSummaryAsync(
                string category = null,
                DateTime? from = null,
                DateTime? to = null,
                bool? isCancelled = null);
        
    }
}
