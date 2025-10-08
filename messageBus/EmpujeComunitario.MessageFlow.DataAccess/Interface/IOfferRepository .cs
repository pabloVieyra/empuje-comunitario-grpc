using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Interface
{
    public interface IOfferRepository
    {
        Task AddOfferDonationAsync(DonationOffer offer);
        Task<DonationOffer?> GetByIdAsync(Guid offerId);
        Task<List<DonationOffer>> GetAllOffersAsync();
    }
}
