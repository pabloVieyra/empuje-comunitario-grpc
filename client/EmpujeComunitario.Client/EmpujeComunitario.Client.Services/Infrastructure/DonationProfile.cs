using AutoMapper;
using EmpujeComunitario.Client.Common.Model.DonationDtos;
using Grpc;

namespace EmpujeComunitario.Client.Services.Infrastructure
{
    public class DonationProfile : Profile
    {
        public DonationProfile()
        {
            CreateMap<DonationCategory, DonationCategoryDto>().ReverseMap();

            CreateMap<DonationInventoryCreateDto, CreateDonationInventoryRequest>()
                .ForMember(dest => dest.Inventory, opt => opt.MapFrom(src => src));

            CreateMap<DonationInventory, DonationInventoryDto>().ReverseMap();

            CreateMap<DonationInventoryCreateRequest, DonationInventoryCreateDto>().ReverseMap();

            CreateMap<UpdateDonationInventoryRequest, DonationInventoryUpdateDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ReverseMap();

            CreateMap<DonationInventoryDeleteDto, DeleteDonationInventoryRequest>()
                .ForMember(dest => dest.Token, opt => opt.Ignore()) // lo cargas manual
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
