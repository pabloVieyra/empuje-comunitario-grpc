using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.DataAccess.Entities
{
    public class DonationRequest
    {
        public Guid RequestId { get; set; }
        public string RequesterOrgId { get; set; }
        //public Organization RequesterOrg { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsCancelled { get; set; } = false;
        public string Creation_user_id { get; set; }
        public User User { get; set; }
        public ICollection<DonationItem> Donations { get; set; }
    }
}
