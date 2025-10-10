using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ
{
    public class RequestDonationModel
    {

        public Guid RequesterOrgId { get; set; }
        public Guid RequestId { get; set; }
        public List<DonationItemModel> Donations { get; set; } = new List<DonationItemModel>();
    }
}
