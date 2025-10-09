using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.EntitiesEmpujeComunitario
{
    public class Event
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventName { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string? ModificationUserId { get; set; }

        public User? ModificationUser { get; set; }
        public ICollection<EventDonation> EventDonations { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}
