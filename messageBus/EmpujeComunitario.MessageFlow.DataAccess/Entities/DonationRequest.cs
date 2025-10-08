using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Entities
{
    public class DonationRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RequestId { get; set; }
        public string RequesterOrgId { get; set; }
        //public Organization RequesterOrg { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsCancelled { get; set; } = false;

        public ICollection<DonationItem> Donations { get; set; }
    }
}
