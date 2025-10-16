using EmpujeComunitario.Graphql.DataAccess.Context;
using EmpujeComunitario.Graphql.DataAccess.Entities;
using EmpujeComunitario.Graphql.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;


namespace EmpujeComunitario.Graphql.DataAccess.Implementation
{
    public class DonationRequestRepository : IDonationRequestRepository
    {
        private readonly MessageFlowDbContext _context;

        public DonationRequestRepository(MessageFlowDbContext context)
        {
            _context = context;
        }

        public async Task AddDonationRequestAsync(DonationRequest request)
        {
            try
            {
                await _context.DonationRequests.AddAsync(request);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<DonationRequest> GetByIdAsync(Guid requestId)
        {
            return await _context.DonationRequests
                .Include(r => r.Donations)
                .FirstOrDefaultAsync(r => r.RequestId == requestId);
        }

        public async Task<List<DonationRequest>> GetAllRequestsAsync()
        {
            return await _context.DonationRequests
                .Include(r => r.Donations)
                .Where(r => !r.IsCancelled)
                .ToListAsync();
        }

        public async Task CancelDonationRequestAsync(Guid requestId)
        {
            try
            {
                var request = await _context.DonationRequests
                    .Include(r => r.Donations)
                    .FirstOrDefaultAsync(r => r.RequestId == requestId);

                if (request != null)
                {
                    // Marcar como cancelada
                    request.IsCancelled = true;

                    // Guardar cambios
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
