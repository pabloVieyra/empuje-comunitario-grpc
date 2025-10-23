using System;
using System.Collections.Generic;

namespace EmpujeComunitario.Graphql.Common.Model
{
    // Resumen de participación por mes (Nivel 1: Agrupación por Mes)
    public class EventParticipationSummary
    {
        public int Year { get; set; }
        public int Month { get; set; }
        // Se corrige para listar directamente los eventos de ese mes.
        public List<MonthlyEventDetail> Events { get; set; } = new List<MonthlyEventDetail>();
    }

    // Detalle de un evento dentro del mes (Nivel 2: El Evento)
    // Se renombra para reflejar mejor que es un evento dentro del reporte mensual
    public class MonthlyEventDetail
    {
        public int Day { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        // Mantiene la lista de donaciones asociadas al evento.
        public List<EventDonationDto> Donations { get; set; } = new List<EventDonationDto>();

        // **NUEVO:** Se añade una lista de participantes para el evento.
        // Esto resuelve la información del usuario que estaba en la clase eliminada.
        public List<EventParticipantDto> Participants { get; set; } = new List<EventParticipantDto>();
    }

    // Información del participante (Usuario)
    public class EventParticipantDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        // Si fuera necesario un detalle de la participación de ese usuario 
        // en este evento (ej. hora de llegada), se añadiría aquí.
    }

    // Información de las donaciones de un evento (sin cambios, ya que está correcta)
    public class EventDonationDto
    {
        public string DonationId { get; set; }
        public int Quantity { get; set; }
    }
}