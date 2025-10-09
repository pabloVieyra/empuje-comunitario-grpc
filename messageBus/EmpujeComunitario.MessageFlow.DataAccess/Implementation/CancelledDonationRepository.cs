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
    public class CancelledDonationRepository : ICancelledDonationRepository
    {
        private readonly MessageFlowDbContext _context;
        public CancelledDonationRepository(MessageFlowDbContext context) 
        {
            _context = context;
        }
        public async Task CreateCancelledRequest(Guid OrganizationId, Guid donationId)
        {
            try
            {
                await _context.CancelledDonation.AddAsync(new Entities.CancelledDonation { OrgId = OrganizationId, RequestId = donationId });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public async Task<CancelledDonation> Find(Guid OrganizationId, Guid donationId)
        {
            try
            {
                var result = await _context.CancelledDonation.FirstOrDefaultAsync(x => x.OrgId == OrganizationId && x.RequestId == donationId);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
