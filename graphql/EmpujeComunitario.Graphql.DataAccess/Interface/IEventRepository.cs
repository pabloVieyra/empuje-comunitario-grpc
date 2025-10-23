using EmpujeComunitario.Graphql.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.DataAccess.Interface
{
    public interface IEventRepository
    {
        Task<IEnumerable<EventParticipationSummary>> GetEventParticipationAsync(
            string userId,
            DateTime? from = null,
            DateTime? to = null,
            bool? hasDonations = null);
    }
        
}
