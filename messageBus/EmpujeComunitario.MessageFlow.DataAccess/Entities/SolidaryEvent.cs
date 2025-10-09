using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Entities
{
    public class SolidaryEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EventId { get; set; }
        public string OrgId { get; set; }
        public string NameEvent { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeEvent { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsCancelled { get; set; } = false;

        public ICollection<VolunteerAdhesion> Adhesions { get; set; }
    }
}
