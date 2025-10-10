using AutoMapper;
using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.Client.Services.Interface;
using EmpujeComunitario.MessageFlow.Common.Model;
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

        public async Task<BaseObjectResponse<GenericResponse>> CreateUserAsync(CreateUserRequestDto createUserRequest, string token)
        {
            BaseObjectResponse<GenericResponse> response = new BaseObjectResponse<GenericResponse>();
            try
            {

                var request = _mapper.Map<CreateUserRequest>(createUserRequest);
                request.Token = token;
                var result = await _userClient.CreateUserAsync(request);

                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);

            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }
        public async Task<BaseObjectResponse<ListUsersResponse>> ListUsersAsync(string token)
        {
            BaseObjectResponse<ListUsersResponse> response = new BaseObjectResponse<ListUsersResponse>();
            try
            {
                ListUsersRequest request = new ListUsersRequest { Token = token };
                var result = await _userClient.ListUsersAsync(request);
                if (result.Success)
                {
                    return response.OkWithData(result);
                }
                else
                {
                    return response.BadRequestWithoutData(result.Message);
                }

            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la obtención de datos: {ex.Message} {ex?.InnerException?.Message}");
            }
        }

        public async Task<BaseObjectResponse<UpdateUserResponse>> UpdateUserAsync(UpdateUserRequestDto updateUserRequestDto, string token)
        {
            BaseObjectResponse<UpdateUserResponse> response = new BaseObjectResponse<UpdateUserResponse>();
            try
            {
                var updateUserRequest = _mapper.Map<UpdateUserRequest>(updateUserRequestDto);
                updateUserRequest.Token = token;

                var result = await _userClient.UpdateUserAsync(updateUserRequest);
                return result.Success
                            ? response.OkWithData(result)
                            : response.BadRequestWithoutData(result.Message);

            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la obtención de datos: {ex.Message} {ex?.InnerException?.Message}");
            }
        }

        public async Task<BaseObjectResponse<dynamic>> DisableUserAsync(string id, string token)
        {
            BaseObjectResponse<dynamic> response = new BaseObjectResponse<dynamic>();
            try
            {
                var disableUserRequest = new DisableUserRequest { Id = id, Token = token };
                var result = await _userClient.DisableUserAsync(disableUserRequest);
                return result.Success
                            ? response.OkWithData(result)
                            : response.BadRequestWithoutData(result.Message);

            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la obtención de datos: {ex.Message} {ex?.InnerException?.Message}");
            }
        }
    }
}
