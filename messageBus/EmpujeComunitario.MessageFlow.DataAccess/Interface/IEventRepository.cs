using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Interface
{
    public interface IEventRepository
    {
        Task AddSolidaryEventAsync(SolidaryEvent evt);
        Task CreateRequestCancelEvent(CancelledEvent eventCancel);
        Task CancelEventAsync(Guid eventId);
        Task<CancelledEvent> FindRequestCancel(Guid id);
        Task<List<SolidaryEvent>> GetAllEventAsync();

    }
}
