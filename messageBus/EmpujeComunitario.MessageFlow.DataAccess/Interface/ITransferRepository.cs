using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Interface
{
    public interface ITransferRepository
    {
        Task ConfirmTransferAsync(DonationTransfer transfer);
        Task<List<DonationTransfer>> GetByRequestAsync(Guid requestId);
    }
}
