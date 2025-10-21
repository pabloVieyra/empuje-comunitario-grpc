using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.DataAccess.Context;
using EmpujeComunitario.Graphql.DataAccess.Entities;
using EmpujeComunitario.Graphql.DataAccess.Interface;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;


namespace EmpujeComunitario.Graphql.DataAccess.Implementation
{
    public class DonationRepository : IDonationRepository
    {
        private readonly MessageFlowDbContext _context;

        public DonationRepository(MessageFlowDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DonationSummaryGroup>> GetDonationSummaryAsync(
                    string category = null,
                    DateTime? from = null,
                    DateTime? to = null,
                    bool? isCancelled = null)
        {
            // punto de partida
            var query = _context.DonationItems
                .Include(d => d.Request)
                .Include(d => d.Offer)
                .Where(d => d.TransferId == null) 
                .AsQueryable();

            // filtros opcionales
            if (!string.IsNullOrEmpty(category))
                query = query.Where(d => d.Category.ToUpper() == category.ToUpper());

            if (from.HasValue)
            {
                var fromUtc = DateTime.SpecifyKind(from.Value, DateTimeKind.Utc);
                query = query.Where(d =>
                    (d.Request != null && d.Request.CreatedAt >= fromUtc) ||
                    (d.Offer != null && d.Offer.CreatedAt >= fromUtc));
            }

            if (to.HasValue)
            {
                var toUtc = DateTime.SpecifyKind(to.Value, DateTimeKind.Utc);
                query = query.Where(d =>
                    (d.Request != null && d.Request.CreatedAt <= toUtc) ||
                    (d.Offer != null && d.Offer.CreatedAt <= toUtc));
            }


            if (isCancelled.HasValue)
                query = query.Where(d =>
                    (d.Request != null && d.Request.IsCancelled == isCancelled) ||
                    (d.Offer != null && isCancelled == false));

            // agrupación y proyección
            var grouped = await query
                .GroupBy(d => new { d.Category, IsCancelled = d.Request != null ? d.Request.IsCancelled : false })
                .Select(g => new DonationSummaryGroup
                {
                    Category = g.Key.Category,
                    IsCancelled = g.Key.IsCancelled,
                    TotalQuantity = g.Sum(x => x.Quantity),
                    Items = g.Select(x => new DonationSummaryItem
                    {
                        RequestId = x.RequestId,
                        DonationOrganizationId = x.Offer != null ? x.Offer.DonationOrganizationId : null,
                        CreatedAt = x.Request != null ? x.Request.CreatedAt : x.Offer.CreatedAt,
                        Quantity = x.Quantity
                    }).ToList()
                }).ToListAsync();


            return grouped;
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
