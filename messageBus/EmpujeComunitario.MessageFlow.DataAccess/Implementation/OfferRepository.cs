using EmpujeComunitario.MessageFlow.DataAccess.Context;
using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using EmpujeComunitario.MessageFlow.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Implementation
{
    public class OfferRepository : IOfferRepository
    {
        private readonly MessageFlowDbContext _context;

        public OfferRepository(MessageFlowDbContext context)
        {
            _context = context;
        }

        public async Task AddOfferDonationAsync(DonationOffer offer)
        {
            await _context.DonationOffers.AddAsync(offer);
            await _context.SaveChangesAsync();
        }

        public async Task<DonationOffer?> GetByIdAsync(Guid offerId)
        {
            return await _context.DonationOffers
                .Include(o => o.Donations)
                .FirstOrDefaultAsync(o => o.OfferId == offerId);
        }

        public async Task<List<DonationOffer>> GetAllOffersAsync()
        {
            return await _context.DonationOffers
                .Include(o => o.Donations)
                .ToListAsync();
        }
    }
}
