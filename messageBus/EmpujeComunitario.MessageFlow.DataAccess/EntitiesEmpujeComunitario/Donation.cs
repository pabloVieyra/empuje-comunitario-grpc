using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.EntitiesEmpujeComunitario
{
    public class Donation
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int Quantity { get; set; }
        public string CreationUserId { get; set; }
        public string? ModificationUserId { get; set; }

   
        
    }
}
