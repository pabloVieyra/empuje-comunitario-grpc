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
        public GraphqlReportService(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }
        public async Task<IEnumerable<DonationSummaryGroup>> GetSummaryAsync(string category, DateTime? from, DateTime? to, bool? isCancelled)
        {
            return await _donationRepository.GetDonationSummaryAsync(category, from, to, isCancelled);
        }
    }
}
