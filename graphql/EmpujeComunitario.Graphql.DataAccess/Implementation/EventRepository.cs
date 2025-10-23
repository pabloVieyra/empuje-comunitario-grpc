using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.DataAccess.Context;
using EmpujeComunitario.Graphql.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.DataAccess.Implementation
{
    public class EventRepository : IEventRepository
    {
        private readonly MessageFlowDbContext _context;
        public EventRepository (MessageFlowDbContext context)
        {
            _context = context; 
        }
        public async Task<IEnumerable<EventParticipationSummary>> GetEventParticipationAsync(
            string userId,
            DateTime? from = null,
            DateTime? to = null,
            bool? hasDonations = null)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            bool isAdmin = user.Role.ToUpper().Equals("PRESIDENT") || user.Role.ToUpper().Equals("COORDINADOR");

            var query = _context.Events
                .AsNoTracking()
                .Include(e => e.Donations) 
                .Include(e => e.UserEvents)
                    .ThenInclude(ue => ue.User).AsQueryable();
            if (!isAdmin)
            {
                query = query.Where(e => e.UserEvents.Any(ue => ue.UserId == userId));
            }

            if (from.HasValue)
                query = query.Where(e => e.EventDateTime >= from.Value);
            if (to.HasValue)
                query = query.Where(e => e.EventDateTime <= to.Value);

            if (hasDonations.HasValue)
            {
                if (hasDonations.Value)
                    query = query.Where(e => e.Donations.Any());
                else
                    query = query.Where(e => !e.Donations.Any());
            }

            var monthlyEventDetails = await query
            .OrderBy(e => e.EventDateTime) 
            .Select(e => new
            {
                Year = e.EventDateTime.Year,
                Month = e.EventDateTime.Month,

                Day = e.EventDateTime.Day,
                e.EventName,
                e.Description,

                Donations = e.Donations.Select(d => new EventDonationDto
                {
                    DonationId = d.DonationId, 
                    Quantity = d.Quantity
                }).ToList(),

                Participants = e.UserEvents.Select(ue => new EventParticipantDto
                {
                    UserId = ue.UserId,
                    UserName = ue.User.Name + " " + ue.User.LastName
                }).ToList()
            })
            .ToListAsync(); 

            var result = monthlyEventDetails
                .GroupBy(x => new { x.Year, x.Month })
                .Select(g => new EventParticipationSummary
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Events = g.Select(e => new MonthlyEventDetail
                    {
                        Day = e.Day,
                        EventName = e.EventName,
                        Description = e.Description,
                        Donations = e.Donations,
                        Participants = e.Participants
                    }).ToList()
                })
                .ToList();

            return result;
        }
        

    }
}
