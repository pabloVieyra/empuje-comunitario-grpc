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
    public class EventRepository : IEventRepository
    {
        private readonly MessageFlowDbContext _context;

        public EventRepository(MessageFlowDbContext context)
        {
            _context = context;
        }

        public async Task AddSolidaryEventAsync(SolidaryEvent evt)
        {
            await _context.SolidaryEvents.AddAsync(evt);
            await _context.SaveChangesAsync();
        }

        public async Task<SolidaryEvent?> GetByIdAsync(Guid eventId)
        {
            return await _context.SolidaryEvents
                .Include(e => e.Adhesions)
                .FirstOrDefaultAsync(e => e.EventId == eventId);
        }

        public async Task<List<SolidaryEvent>> GetActiveExternalEventsAsync(string myOrgId)
        {
            return await _context.SolidaryEvents
                .Where(e => e.OrgId != myOrgId && !e.IsCancelled && e.DateTimeEvent >= DateTime.UtcNow)
                .Include(e => e.Adhesions)
                .ToListAsync();
        }

        public async Task CancelEventAsync(Guid eventId)
        {
            var evt = await _context.SolidaryEvents.FindAsync(eventId);
            if (evt != null)
            {
                evt.IsCancelled = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
