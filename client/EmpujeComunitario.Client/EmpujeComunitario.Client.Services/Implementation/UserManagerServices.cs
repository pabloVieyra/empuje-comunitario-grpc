using AutoMapper;
using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.Client.Services.Interface;
using Grpc;


namespace EmpujeComunitario.Client.Services.Implementation
{
    public class UserManagerServices : IUserManagerServices
    {
        private readonly UserService.UserServiceClient _userClient;
        private readonly IMapper _mapper;

        public UserManagerServices(UserService.UserServiceClient userClient, IMapper mapper)
        {
            _userClient = userClient;
            _mapper = mapper;
        }

        public async Task<BaseObjectResponse<GenericResponse>> CreateUserAsync(CreateUserRequestDto createUserRequest)
        {
            BaseObjectResponse<GenericResponse> response = new BaseObjectResponse<GenericResponse>();
            try
            {
                CreateUserRequest request = _mapper.Map<CreateUserRequest>(createUserRequest);

                var result = await _userClient.CreateUserAsync(request);
                if (result.Success)
                {
                    return response.OkWithData(result);
                }
                else
                {
                    return response.BadRequestWithData(result.Message);
                }

            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }
        public async Task<BaseObjectResponse<ListUsersResponse>> ListUsersAsync(ListUsersRequestDto requestListUser)
        {
            BaseObjectResponse<ListUsersResponse> response = new BaseObjectResponse<ListUsersResponse>();
            try
            {
                ListUsersRequest request = _mapper.Map<ListUsersRequest>(requestListUser);

                var result = await _userClient.ListUsersAsync(request);
                if (result.Success)
                {
                    return response.OkWithData(result);
                }
                else
                {
                    return response.BadRequestWithData(result.Message);
                }

            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la obtención de datos: {ex.Message} {ex?.InnerException?.Message}");
            }
        }
    }
}
