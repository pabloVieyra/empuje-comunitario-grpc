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
    public class TransferRepository : ITransferRepository
    {
        private readonly MessageFlowDbContext _context;

        public TransferRepository(MessageFlowDbContext context)
        {
            _context = context;
        }

        public async Task ConfirmTransferAsync(DonationTransfer transfer)
        {
            await _context.DonationTransfers.AddAsync(transfer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DonationTransfer>> GetByRequestAsync(Guid requestId)
        {
            return await _context.DonationTransfers
                .Include(t => t.Donations)
                .Where(t => t.RequestId == requestId)
                .ToListAsync();
        }
    }
}
