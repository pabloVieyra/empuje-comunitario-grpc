using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.Service.Interface;
using HotChocolate;

namespace EmpujeComunitario.Graphql.Api.GraphqlQuery
{
    public class DonationQuery
    {
        public async Task<IEnumerable<DonationSummaryGroup>> GetDonationSummaryAsync(
            [Service] IGraphqlReportService service,
            string category = null,
            DateTime? from = null,
            DateTime? to = null,
            bool? isCancelled = null)
        {
            return await service.GetSummaryAsync(category, from, to, isCancelled);
        }


        public async Task<IEnumerable<EventParticipationSummary>> GetEventParticipationAsync(
            [Service] IGraphqlReportService service,
            string userId,            
            DateTime? from = null,    
            DateTime? to = null,
            bool? donationGiven = null
        )
        {
            return await service.GetEventParticipationAsync(userId, from, to, donationGiven);
        }
    }
}
