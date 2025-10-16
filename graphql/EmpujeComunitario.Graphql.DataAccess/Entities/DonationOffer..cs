using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.DataAccess.Entities
{
    public class DonationOffer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OfferId { get; set; }
        public string DonationOrganizationId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<DonationItem> Donations { get; set; }
    }
}
