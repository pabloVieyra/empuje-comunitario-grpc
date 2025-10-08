using EmpujeComunitario.MessageFlow.DataAccess.Context;
using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using EmpujeComunitario.MessageFlow.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.DataAccess.Implementation
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly MessageFlowDbContext _context;

        public VolunteerRepository(MessageFlowDbContext context)
        {
            _context = context;
        }

        public async Task AddVolunteerAdhesionAsync(VolunteerAdhesion volunteer)
        {
            await _context.VolunteerAdhesions.AddAsync(volunteer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<VolunteerAdhesion>> GetByEventAsync(Guid eventId)
        {
            return await _context.VolunteerAdhesions
                .Where(v => v.EventId == eventId)
                .ToListAsync();
        }
    }
}
