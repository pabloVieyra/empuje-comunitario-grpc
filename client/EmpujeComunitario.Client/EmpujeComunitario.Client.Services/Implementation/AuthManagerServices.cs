using AutoMapper;
using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.Client.Services.Interface;
using EmpujeComunitario.MessageFlow.Common.Model;
using Grpc;

namespace EmpujeComunitario.Client.Services.Implementation
{
    public class AuthManagerServices : IAuthManagerServices
    {
        private readonly AuthService.AuthServiceClient _authServiceClient;
        private readonly IMapper _mapper;
        public AuthManagerServices(AuthService.AuthServiceClient authServiceClient, IMapper mapper)
        {
            _authServiceClient = authServiceClient;
            _mapper = mapper;
        }
        public async Task<BaseObjectResponse<LoginResponse>> Login(LoginRequestDto loginRequest)
        {
            BaseObjectResponse<LoginResponse> response = new BaseObjectResponse<LoginResponse>();
            try
            {
                LoginRequest request = _mapper.Map<LoginRequest>(loginRequest);
                var result = await _authServiceClient.LoginAsync(request);
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
    }
}
