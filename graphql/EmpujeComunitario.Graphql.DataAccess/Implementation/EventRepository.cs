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
                .Include(e => e.Donations) // Incluir Donaciones
                .Include(e => e.UserEvents) // Incluir la tabla pivote de participación
                    .ThenInclude(ue => ue.User).AsQueryable(); // Incluir los detalles del Usuario
                //.Where(e => e. == false);

            // 🔒 Filtro de usuario obligatorio
            if (!isAdmin)
            {
                query = query.Where(e => e.UserEvents.Any(ue => ue.UserId == userId));
            }

            // 📅 Filtro por rango de fechas
            if (from.HasValue)
                query = query.Where(e => e.EventDateTime >= from.Value);
            if (to.HasValue)
                query = query.Where(e => e.EventDateTime <= to.Value);

            // 🎁 Filtro por reparto de donaciones
            if (hasDonations.HasValue)
            {
                if (hasDonations.Value)
                    query = query.Where(e => e.Donations.Any());
                else
                    query = query.Where(e => !e.Donations.Any());
            }

            var monthlyEventDetails = await query
            .OrderBy(e => e.EventDateTime) // Opcional, pero bueno para la presentación
            .Select(e => new
            {
                // Propiedades para la Agrupación (Mes/Año)
                Year = e.EventDateTime.Year,
                Month = e.EventDateTime.Month,

                // Propiedades del Evento (MonthlyEventDetail)
                Day = e.EventDateTime.Day,
                e.EventName,
                e.Description,

                // Donaciones (mapeo directo a DTO)
                Donations = e.Donations.Select(d => new EventDonationDto
                {
                    DonationId = d.DonationId, // Asumo que DonationId es una propiedad válida de EventDonation
                    Quantity = d.Quantity
                }).ToList(),

                // Participantes (mapeo directo a DTO)
                Participants = e.UserEvents.Select(ue => new EventParticipantDto
                {
                    UserId = ue.UserId,
                    UserName = ue.User.Name + " " + ue.User.LastName
                }).ToList()
            })
            .ToListAsync(); // Traemos los datos una sola vez a memoria

                    // 5. Agrupación final en memoria (Agrupar por Año/Mes)
            var result = monthlyEventDetails
                .GroupBy(x => new { x.Year, x.Month })
                .Select(g => new EventParticipationSummary
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    // Mapeamos los eventos dentro del grupo
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
