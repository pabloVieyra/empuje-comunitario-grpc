using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Common.Model.MessagesRabbitMq
{
    public class SolidaryEventModel
    {
        public Guid OrgId { get; set; }
        public Guid EventId { get; set; }
        public string NameEvent { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeEvent {  get; set; }
    }
}
