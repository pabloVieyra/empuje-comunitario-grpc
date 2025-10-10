using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ
{
    public class OfferDonationModel
    {
        public Guid OfferId { get; set; }
        public Guid DonationOrganizationId { get; set; }
        public List<DonationItemModel> Donations { get; set; } = new List<DonationItemModel>();
    }
}
