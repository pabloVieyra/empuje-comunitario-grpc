using EmpujeComunitario.Client.Common.Model;
using Grpc;

namespace EmpujeComunitario.Client.Services.Interface
{
    public interface IUserManagerServices
    {
        Task<BaseObjectResponse<GenericResponse>> CreateUserAsync(CreateUserRequestDto createUserRequest);
        Task<BaseObjectResponse<ListUsersResponse>> ListUsersAsync(ListUsersRequestDto requestListUser);
    }
}
