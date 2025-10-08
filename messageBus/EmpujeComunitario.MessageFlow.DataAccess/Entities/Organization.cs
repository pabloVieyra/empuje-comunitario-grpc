using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Entities
{
    public class Organization
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<DonationRequest> DonationRequests { get; set; }
        public ICollection<DonationOffer> DonationOffers { get; set; }
        public ICollection<SolidaryEvent> SolidaryEvents { get; set; }
    }
}
