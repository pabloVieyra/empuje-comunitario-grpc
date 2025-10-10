using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Interface
{
    public interface IVolunteerRepository
    {
        Task AddVolunteerAdhesionAsync(VolunteerAdhesion volunteer);
        Task<List<VolunteerAdhesion>> GetByEventAsync(Guid eventId);
    }
}
