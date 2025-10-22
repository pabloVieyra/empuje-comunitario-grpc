using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.DataAccess.Entities
{
    public class DonationOffer
    {
        public Guid OfferId { get; set; }
        public string DonationOrganizationId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid Creation_user_id { get; set; }
        public User User { get; set; }
        public ICollection<DonationItem> Donations { get; set; }
    }
}
