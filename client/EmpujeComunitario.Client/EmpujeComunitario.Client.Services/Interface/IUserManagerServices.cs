using EmpujeComunitario.Client.Common.Model;
using Grpc;

namespace EmpujeComunitario.Client.Services.Interface
{
    public interface IUserManagerServices
    {
        Task<BaseObjectResponse<GenericResponse>> CreateUserAsync(CreateUserRequestDto createUserRequest, string token);
        Task<BaseObjectResponse<ListUsersResponse>> ListUsersAsync(string token);
        Task<BaseObjectResponse<UpdateUserResponse>> UpdateUserAsync(UpdateUserRequestDto updateUserRequestDto, string token);
        Task<BaseObjectResponse<dynamic>> DisableUserAsync(string id, string token);
    }
}
