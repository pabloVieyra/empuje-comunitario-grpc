using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.Common.Model
{
    public class DonationSummaryGroup
    {
        public string Category { get; set; }
        public bool IsCancelled { get; set; }
        public int TotalQuantity { get; set; }
        public List<DonationSummaryItem> Items { get; set; } // detalle de cada item
    }
    public class DonationSummaryItem
    {
        public Guid? RequestId { get; set; }
        public string? DonationOrganizationId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int Quantity { get; set; }
    }
}
