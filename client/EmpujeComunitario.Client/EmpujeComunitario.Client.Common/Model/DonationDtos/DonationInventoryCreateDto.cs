using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Common.Model.DonationDtos
{
    public class DonationInventoryCreateDto
    {
        public DonationCategoryDto Category { get; set; }
        public string Description { get; set; } 
        public int Quantity { get; set; }
    }
}
