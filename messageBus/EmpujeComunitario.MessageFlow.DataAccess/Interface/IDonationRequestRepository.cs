using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Interface
{
    public interface IDonationRequestRepository
    {
        Task AddDonationRequestAsync(DonationRequest request);
        Task<DonationRequest?> GetByIdAsync(Guid requestId);
        Task CancelDonationRequestAsync(Guid requestId);
        Task<List<DonationRequest>> GetAllRequestsAsync();
    }
}
