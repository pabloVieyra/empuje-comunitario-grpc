using AutoMapper;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using EmpujeComunitario.MessageFlow.DataAccess.Entities;


namespace EmpujeComunitario.MessageFlow.Service.Infrastructure
{
    public class RabbitMqMappingProfile : Profile
    {
        public RabbitMqMappingProfile()
        {
            CreateMap<RequestDonationModel, DonationRequest>();
            CreateMap<OfferDonationModel, DonationOffer>();
            CreateMap<DonationItemModel, DonationItem>();
            CreateMap<SolidaryEventModel, SolidaryEvent>();
            CreateMap<VolunteerAdhesionModel, VolunteerAdhesion>();
            CreateMap<TransferDonationModel, DonationTransfer>();
        }
    }
}
