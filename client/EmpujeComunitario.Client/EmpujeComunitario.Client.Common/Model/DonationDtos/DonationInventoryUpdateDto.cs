using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Common.Model.DonationDtos
{
    public class DonationInventoryUpdateDto
    {
        public string Id { get; set; } = string.Empty;
        public DonationCategoryDto Category { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
