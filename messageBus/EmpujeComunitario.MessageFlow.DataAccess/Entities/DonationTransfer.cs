using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Entities
{
    public class DonationTransfer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RequestId { get; set; }
        public DonationRequest Request { get; set; }

        public string DonationOrgId { get; set; }

        public DateTime TransferDate { get; set; } = DateTime.UtcNow;

        public ICollection<DonationItem> Donations { get; set; }
    }

}
