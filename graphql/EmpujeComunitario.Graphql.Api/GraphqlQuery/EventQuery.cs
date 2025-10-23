using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.Service.Interface;
using HotChocolate;

namespace EmpujeComunitario.Graphql.Api.GraphqlQuery
{
    public class EventQuery
    {
        [GraphQLName("participationSummary")]
        public async Task<IEnumerable<EventParticipationSummary>> GetEventParticipationAsync(
            [Service] IGraphqlReportService service,
            string userId,               // filtro obligatorio
            DateTime? from = null,     // rango de fechas opcional
            DateTime? to = null,
            bool? donationGiven = null // filtro opcional: sí/no/ambos
        )
        {
            // Llamada al servicio que implementará la lógica de repositorio
            return await service.GetEventParticipationAsync(userId, from, to, donationGiven);
        }
    }
}
