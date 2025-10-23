using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.DataAccess.Entities
{
    public class CancelledDonation
    {
        public Guid RequestId { get; set; }
        public Guid OrgId { get; set; }
        public DateTime CancelledAt { get; set; } = DateTime.UtcNow;
    }
}
