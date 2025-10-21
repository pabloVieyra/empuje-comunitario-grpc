using EmpujeComunitario.MessageFlow.Common.Model;

namespace EmpujeComunitario.MessageFlow.Service.Interface
{
    public interface IRabbitMqService
    {
        BaseObjectResponse<string> Publish(string routingKey, string message, string userId = "");
    }
}
