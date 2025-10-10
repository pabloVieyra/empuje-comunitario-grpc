using AutoMapper;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using EmpujeComunitario.MessageFlow.DataAccess.Interface;
using EmpujeComunitario.MessageFlow.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Service.Implementation
{
    public class CancellDonationService : ICancellDonationService
    {
        private readonly ICancelledDonationRepository _cancelledDonationRepository;
        private readonly IDonationRequestRepository _donationRequestRepository;
        private readonly IMapper _mapper;
        public CancellDonationService(ICancelledDonationRepository cancelledDonationRepository,
                                     IDonationRequestRepository donationRequestRepository,
                                     IMapper mapper)
        {
            _cancelledDonationRepository = cancelledDonationRepository;
            _donationRequestRepository = donationRequestRepository;
            _mapper = mapper;
        }

        public async Task RequestDonationCanceled (CancelRequestModel request)
        {
            try
            {
                await _donationRequestRepository.CancelDonationRequestAsync(request.RequestId);
                await _cancelledDonationRepository.CreateCancelledRequest(request.RequestOrgId, request.RequestId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
