using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Entities
{
    public class CancelledRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RequestId { get; set; }
        public string OrgId { get; set; }
        public DateTime CancelledAt { get; set; } = DateTime.UtcNow;
    }
}
