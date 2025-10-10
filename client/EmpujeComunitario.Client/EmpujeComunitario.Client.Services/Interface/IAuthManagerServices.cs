using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.MessageFlow.Common.Model;
using Grpc;

namespace EmpujeComunitario.Client.Services.Interface
{
    public interface IAuthManagerServices
    {
        Task<BaseObjectResponse<LoginResponse>> Login(LoginRequestDto loginRequest);
    }
}
