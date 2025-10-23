using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Entities
{
    public class CancelledEvent
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string OrgId { get; set; }
        public DateTime CancelledAt { get; set; } = DateTime.UtcNow;
    }
}
