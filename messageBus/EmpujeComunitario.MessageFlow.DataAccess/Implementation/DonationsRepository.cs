using EmpujeComunitario.MessageFlow.DataAccess.Context;
using EmpujeComunitario.MessageFlow.DataAccess.EntitiesEmpujeComunitario;
using EmpujeComunitario.MessageFlow.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Implementation
{
    public class DonationsRepository: IDonationsRepository
    {
        private readonly EmpujeComunitarioContext _context;
        public DonationsRepository(EmpujeComunitarioContext context) 
        {
            _context = context;
        }
        public async Task<Donation> GetDonation(string description, string category) 
        {
            var result = await _context.Donations.FirstOrDefaultAsync(x => x.Category == category && x.Description == description);
            return result;
        }

        public async Task<bool> UppdateDonation(string Id, int quantity)
        {
            try
            {
                var result = await _context.Donations.FirstOrDefaultAsync(x => x.Id == Id);
                result.Quantity += quantity;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


    }
}
