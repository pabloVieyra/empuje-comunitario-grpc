using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.DataAccess.Interface;
using EmpujeComunitario.Graphql.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.Service.Implementation
{
    public class GraphqlReportService : IGraphqlReportService
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IEventRepository _eventRepository;
        public GraphqlReportService(IDonationRepository donationRepository, IEventRepository eventRepository)
        {
            _donationRepository = donationRepository;
            _eventRepository = eventRepository;
        }
        public async Task<IEnumerable<DonationSummaryGroup>> GetSummaryAsync(string category, DateTime? from, DateTime? to, bool? isCancelled)
        {
            return await _donationRepository.GetDonationSummaryAsync(category, from, to, isCancelled);
        }
        public async Task<IEnumerable<EventParticipationSummary>> GetEventParticipationAsync(
                    string userId,
                    DateTime? from = null,
                    DateTime? to = null,
                    bool? donationGiven = null)
        {
            var events = await _eventRepository.GetEventParticipationAsync(userId, from, to, donationGiven);
            return events;
        }
    }
}
