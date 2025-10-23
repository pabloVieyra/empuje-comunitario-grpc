using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.DataAccess.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime EventDateTime { get; set; }
        public DateTime? ModificationDate { get; set; }

        // FK a User
        public string ModificationUserId { get; set; }
        public User ModificationUser { get; set; }

        // Navegación
        public ICollection<EventDonation> Donations { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}
