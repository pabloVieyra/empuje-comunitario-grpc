using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.EntitiesEmpujeComunitario
{
    public class EventDonation
    {
        public int EventId { get; set; }
        public string DonationId { get; set; }
        public int Quantity { get; set; }

        public Event Event { get; set; }
        public Donation Donation { get; set; }
    }
}
