using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ
{
    public class CancelRequestModel
    {
        public Guid RequestOrgId { get; set; }
        public Guid RequestId { get; set; }
    }
}
