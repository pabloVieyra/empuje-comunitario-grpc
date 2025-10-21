using EmpujeComunitario.MessageFlow.DataAccess.EntitiesEmpujeComunitario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Interface
{
    public interface IDonationsRepository
    {
        Task<Donation> GetDonation(string description, string category);
        Task<bool> UppdateDonation(string Id, int quantity);
        Task<bool> CreateDonationAsync(string category, string description, int quantity, string userId);
    }
}
