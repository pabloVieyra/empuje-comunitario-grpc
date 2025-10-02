using AutoMapper;
using EmpujeComunitario.Client.Common.Model.EventDtos;
using Grpc;
using System.Globalization;


namespace EmpujeComunitario.Client.Services.Infrastructure
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<CreateEventDto, CreateEventRequest>()
                .ForMember(dest => dest.EventDateTime,
                           opt => opt.MapFrom(src => src.EventDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.ActorId,
                           opt => opt.MapFrom(src => src.CreationUserId));
            //.ForMember(dest => dest.EventDateTime, opt => opt.MapFrom(src => DateTime.Parse(src.EventDateTime)));

            CreateMap<UpdateEventRequest, EventDto>()
            .ForMember(dest => dest.ModificationUser, opt => opt.MapFrom(src => src.Event.ModificationUser))
            //.ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Event.Id))
            .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.EventName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Event.Description))
            .ForMember(dest => dest.EventDateTime, opt => opt.MapFrom(src => src.Event.EventDateTime)).ReverseMap();


            CreateMap<DeleteEventRequest, DeleteEventDto>()
                .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.ActorId))
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId)).ReverseMap();
            CreateMap<FindEventByIdRequest, FindEventByIdDto>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId)).ReverseMap();

            CreateMap<AddUserToEventRequest, AddUserToEventDto>()
            .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.ActorId)).ReverseMap();

            CreateMap<RemoveUserFromEventRequest, RemoveUserFromEventDto>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.ActorId)).ReverseMap();


            CreateMap<RegisterDonationRequest, RegisterDonationDto>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.DonationId, opt => opt.MapFrom(src => src.DonationId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.ActorId)).ReverseMap();

        }
    }
}
