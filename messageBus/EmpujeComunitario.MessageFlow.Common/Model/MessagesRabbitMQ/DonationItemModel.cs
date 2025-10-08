using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ
{
    public class DonationItemModel
    {
        public string Category { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
