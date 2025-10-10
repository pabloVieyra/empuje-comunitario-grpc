using EmpujeComunitario.MessageFlow.DataAccess.Context;
using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using EmpujeComunitario.MessageFlow.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public async Task<List<SolidaryEvent>> GetAllEventAsync()
        {
            try
            {
                return await _context.SolidaryEvents.Where(x=> x.IsCancelled == false).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task CancelEventAsync(Guid eventId)
        {
            var evt = await _context.SolidaryEvents.FirstOrDefaultAsync(x=> x.EventId == eventId);
            if (evt != null)
            {
                evt.IsCancelled = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task CreateRequestCancelEvent(CancelledEvent eventCancel)
        {
            await _context.CancelledEvents.AddAsync(eventCancel);
            await _context.SaveChangesAsync();
        }
        public async Task<CancelledEvent> FindRequestCancel(Guid id)
        {
            try
            {
                return await _context.CancelledEvents.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
