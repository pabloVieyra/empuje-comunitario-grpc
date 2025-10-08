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
        Task<SolidaryEvent?> GetByIdAsync(Guid eventId);
        Task<List<SolidaryEvent>> GetActiveExternalEventsAsync(string myOrgId);
        Task CancelEventAsync(Guid eventId);
    }
}
