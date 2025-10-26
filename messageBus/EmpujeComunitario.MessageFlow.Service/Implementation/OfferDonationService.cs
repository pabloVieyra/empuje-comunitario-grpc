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
    public class OfferDonationService : IOfferDonationService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper _mapper;
        public OfferDonationService(IOfferRepository offerRepository, IMapper mapper) 
        { 
            _offerRepository = offerRepository;
            _mapper = mapper;
        }
        public async Task CreateOffer(OfferDonationModel donationOffer, string userId)
        {
            try
            {
                var request = _mapper.Map<DonationOffer>(donationOffer);
                request.Create_user_id = userId;
                await _offerRepository.AddOfferDonationAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
