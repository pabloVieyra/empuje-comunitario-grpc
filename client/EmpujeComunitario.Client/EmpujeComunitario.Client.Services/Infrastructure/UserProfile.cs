using AutoMapper;
using EmpujeComunitario.Client.Common.Model;
using Grpc;
namespace EmpujeComunitario.Client.Services.Infrastructure
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //loggin
            CreateMap<LoginRequest, LoginRequestDto>().ReverseMap();
            //create
            CreateMap<CreateUserRequestDto, CreateUserProto>();
            CreateMap<CreateUserRequestDto, CreateUserRequest>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));
            //update
            CreateMap<UpdateUserRequestDto, UserProto>();
            CreateMap<UpdateUserRequestDto, UpdateUserRequest>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));
        }
    }
}
