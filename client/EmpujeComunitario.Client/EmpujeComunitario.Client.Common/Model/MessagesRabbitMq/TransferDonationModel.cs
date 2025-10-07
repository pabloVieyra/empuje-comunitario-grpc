using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Common.Model.MessagesRabbitMq
{
    public class TransferDonationModel
    {
        public Guid RequestId { get; set; }
        public Guid DonationOrgId {  get; set; }
        public List<DonationItemModel> Donations = new List<DonationItemModel>();
    }
}
