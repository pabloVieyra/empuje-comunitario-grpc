using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.EntitiesEmpujeComunitario
{
    public class UserEvent
    {
        public string UserId { get; set; }
        public int EventId { get; set; }

        public User User { get; set; }
        public Event Event { get; set; }
    }
}
