using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ
{
    public class CancelEventModel
    {
        public Guid OrgId { get; set; }
        public Guid EventId { get; set; }
    }
}
