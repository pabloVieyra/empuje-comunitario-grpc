using AutoMapper;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using EmpujeComunitario.MessageFlow.DataAccess.Implementation;
using EmpujeComunitario.MessageFlow.DataAccess.Interface;
using EmpujeComunitario.MessageFlow.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Service.Implementation
{
    public class RequestDonationService : IRequestDonationService
    {
        private readonly IDonationRequestRepository _donationRequestRepository;
        private readonly ICancelledDonationRepository _cancelDonationRequestRepository;
        private readonly IMapper _mapper;
        public RequestDonationService(IDonationRequestRepository donationRequestRepository,
            ICancelledDonationRepository cancelledDonationRepository,
            IMapper mapper) 
        {
            _mapper = mapper;
            _cancelDonationRequestRepository = cancelledDonationRepository;
            _donationRequestRepository = donationRequestRepository;
        }
        public async Task CreateRequest(RequestDonationModel request)
        {
            try
            {
                var entitySolicitud = _mapper.Map<DonationRequest>(request);
                var isCanceled = await _cancelDonationRequestRepository.Find(request.RequesterOrgId, request.RequestId);
                if (isCanceled == null)
                {
                    await _donationRequestRepository.AddDonationRequestAsync(entitySolicitud);
                }else
                {
                    Console.WriteLine("La solicitud de donación fue cancelada");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
