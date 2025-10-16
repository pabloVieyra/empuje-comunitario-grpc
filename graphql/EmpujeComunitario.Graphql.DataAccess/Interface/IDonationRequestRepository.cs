using EmpujeComunitario.Graphql.DataAccess.Entities;

namespace EmpujeComunitario.Graphql.DataAccess.Interface
{
    public interface IDonationRequestRepository
    {
        Task AddDonationRequestAsync(DonationRequest request);
        Task<DonationRequest> GetByIdAsync(Guid requestId);
        Task CancelDonationRequestAsync(Guid requestId);
        Task<List<DonationRequest>> GetAllRequestsAsync();
    }
}
