using Grpc;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace EmpujeComunitario.Client.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpujeComunitarioController : ControllerBase
    {
        private readonly UserService.UserServiceClient _userClient;

        // Constructor que recibe el cliente gRPC inyectado.
        public EmpujeComunitarioController(UserService.UserServiceClient userClient)
        {
            _userClient = userClient;
        }

        [HttpPost(nameof(CreateUserAsync))]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequest request)
        {
            var result = await _userClient.CreateUserAsync( request);
            return Ok(result);
        }
    }
}