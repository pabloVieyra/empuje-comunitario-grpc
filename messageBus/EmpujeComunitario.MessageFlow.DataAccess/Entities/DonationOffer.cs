using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Entities
{
    public class DonationOffer
    {
        public Guid OfferId { get; set; }
        public string DonationOrganizationId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<DonationItem> Donations { get; set; }
        public Guid Create_user_id { get; set; }  
    }
}
