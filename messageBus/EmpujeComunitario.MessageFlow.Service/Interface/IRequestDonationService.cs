using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;

namespace EmpujeComunitario.MessageFlow.Service.Interface
{
    public interface IRequestDonationService
    {
        Task CreateRequest(RequestDonationModel request);
    }
}
