using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Common.Model.MessagesRabbitMq
{
    public class OfferDonationModel
    {
        public Guid OfferId { get; set; }
        public string  DonationOrganizationId { get; set; }
        public List<DonationItemModel> Donations { get; set; } = new List<DonationItemModel>();
    }
}
