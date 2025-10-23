using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.DataAccess.Entities
{
    public class DonationItem
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public Guid? RequestId { get; set; }
        public DonationRequest Request { get; set; }

        public Guid? OfferId { get; set; }
        public DonationOffer Offer { get; set; }

        public Guid? TransferId { get; set; }
        public DonationTransfer Transfer { get; set; }
    }
}
