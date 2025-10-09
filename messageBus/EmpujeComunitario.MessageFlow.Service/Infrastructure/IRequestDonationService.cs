using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;

namespace EmpujeComunitario.MessageFlow.Service.Infrastructure
{
    public interface IRequestDonationService
    {
        Task CreateRequest(RequestDonationModel request);
    }
}
