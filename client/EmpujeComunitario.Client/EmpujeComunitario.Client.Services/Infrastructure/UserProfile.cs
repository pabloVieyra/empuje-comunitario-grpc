using AutoMapper;
using EmpujeComunitario.Client.Common.Model;
using Grpc;
namespace EmpujeComunitario.Client.Services.Infrastructure
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<CreateUserRequestDto, CreateUserRequest>()
                // .ForMember() mapea campo por campo.
                // En este caso, mapea la propiedad Username del DTO al campo Username del modelo gRPC.
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<CreateUserRequestDto, CreateUserRequest>();
            CreateMap<ListUsersRequest, ListUsersRequestDto>().ReverseMap(); // de User a UserDto
        }
    }
}
