using EmpujeComunitario.Graphql.Common.Model;


namespace EmpujeComunitario.Graphql.Service.Interface
{
    public interface IGraphqlReportService
    {
        Task<IEnumerable<DonationSummaryGroup>> GetSummaryAsync(string category, DateTime? from, DateTime? to, bool? isCancelled);
        Task<IEnumerable<EventParticipationSummary>> GetEventParticipationAsync(
                   string userId,
                   DateTime? from = null,
                   DateTime? to = null,
                   bool? donationGiven = null);
    }
}
