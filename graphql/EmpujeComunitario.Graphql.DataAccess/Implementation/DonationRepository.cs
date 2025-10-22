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
                        Description = x.Description,
                        User = x.Offer != null ? x.Offer.User.Email  : x.Request.User.Email,
                        DonationOrganizationId = x.Offer != null ? x.Offer.DonationOrganizationId : null,
                        CreatedAt = x.Request != null ? x.Request.CreatedAt : x.Offer.CreatedAt,
                        Quantity = x.Quantity
                    }).ToList()
                }).ToListAsync();


            return grouped;
        }

        public async Task<IEnumerable<DonationSummaryGroup>> GetDonationExcel(
            string category = null,
            DateTime? from = null,
            DateTime? to = null,
            bool? isCancelled = null)
        {
            var query = _context.DonationItems
                .Include(d => d.Request)
                .ThenInclude(d=> d.User)
                .Include(d => d.Offer)
                .ThenInclude(d => d.User)
                .Where(d => d.TransferId == null)
                .AsQueryable();

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
                    (d.Request == null && isCancelled == false));

            // Agrupamos por categoría para generar una hoja por categoría
            var grouped = await query
                .GroupBy(d => d.Category)
                .Select(g => new DonationSummaryGroup
                {
                    Category = g.Key,
                    Items = g.Select(x => new DonationSummaryItem
                    {
                        RequestId = x.RequestId,
                        Description = x.Description,
                        DonationOrganizationId = x.Offer != null ? x.Offer.DonationOrganizationId : x.Request.RequesterOrgId,
                        User = x.Offer != null ? x.Offer.User.Email : x.Request.User.Email,
                        CreatedAt = x.Request != null ? x.Request.CreatedAt : x.Offer.CreatedAt,
                        Quantity = x.Quantity,
                        
                    }).ToList()
                }).ToListAsync();

            return grouped;
        }

        
    }
}
