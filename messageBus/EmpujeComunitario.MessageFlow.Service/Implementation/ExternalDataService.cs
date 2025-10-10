using AutoMapper;
using EmpujeComunitario.MessageFlow.Common.Model;
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
    public class ExternalDataService : IExternalDataService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper _mapper;
        public ExternalDataService(IEventRepository eventRepository,
            IVolunteerRepository volunteerRepository,
            IOfferRepository offerRepository,
            IMapper mapper) 
        {
            _eventRepository = eventRepository;
            _volunteerRepository = volunteerRepository;
            _offerRepository = offerRepository;
            _mapper = mapper;
        }

        public async Task<BaseObjectResponse<List<SolidaryEventModel>>> GetAllEvents()
        {
            var response = new BaseObjectResponse<List<SolidaryEventModel>>();
            try
            {
                var result = await _eventRepository.GetAllEventAsync();
                var events = _mapper.Map<List<SolidaryEventModel>>(result);
                return response.OkWithData(events);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return response.BadRequestWithoutData(ex.Message);
            }
        }

        public async Task<BaseObjectResponse<List<VolunteerAdhesionModel>>> GetAllVolunteerByEvent( string eventId)
        {
            var response = new BaseObjectResponse<List<VolunteerAdhesionModel>>();
            try
            {
                var result = await _volunteerRepository.GetByEventAsync(Guid.Parse(eventId));
                var volunteer = _mapper.Map<List<VolunteerAdhesionModel>>(result);
                return response.OkWithData(volunteer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return response.BadRequestWithoutData(ex.Message);
            }
        }



        public async Task<BaseObjectResponse<List<OfferDonationModel>>> GetAllOfferDonation()
        {
            var response = new BaseObjectResponse<List<OfferDonationModel>>();
            try
            {
                var result = await _offerRepository.GetAllOffersAsync();
                var offer = _mapper.Map<List<OfferDonationModel>>(result);
                return response.OkWithData(offer);
            }catch (Exception e)
            { 
                Console.WriteLine(e.Message); 
                return response.BadRequestWithoutData(e.Message); 
            }
            
        }
    }
}
