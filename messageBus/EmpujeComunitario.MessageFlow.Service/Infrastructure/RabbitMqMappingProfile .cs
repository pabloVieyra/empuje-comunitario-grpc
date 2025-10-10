using AutoMapper;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using EmpujeComunitario.MessageFlow.DataAccess.Entities;


namespace EmpujeComunitario.MessageFlow.Service.Infrastructure
{
    public class RabbitMqMappingProfile : Profile
    {
        public RabbitMqMappingProfile()
        {
            CreateMap<RequestDonationModel, DonationRequest>().ReverseMap();
            CreateMap<OfferDonationModel, DonationOffer>().ReverseMap();
            CreateMap<DonationItemModel, DonationItem>().ReverseMap();
            CreateMap<CancelEventModel, CancelledEvent>().ReverseMap();
            CreateMap<SolidaryEventModel, SolidaryEvent>().ReverseMap();
            CreateMap<VolunteerAdhesionModel, VolunteerAdhesion>().ReverseMap();
            CreateMap<TransferDonationModel, DonationTransfer>().ReverseMap();
        }
    }
}
