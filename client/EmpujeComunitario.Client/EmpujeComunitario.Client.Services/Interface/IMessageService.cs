using EmpujeComunitario.MessageFlow.Common.Model;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;


namespace EmpujeComunitario.Client.Services.Interface
{
    public interface IMessageService
    {
        Task<BaseObjectResponse<string>> RequestDonationAsync(RequestDonationModel request);

        Task<BaseObjectResponse<string>> TransfersDonationAsync(TransferDonationModel request, string idOrganizacionSolicitante);

        Task<BaseObjectResponse<string>> OffersDonationsAsync(OfferDonationModel request);

        Task<BaseObjectResponse<string>> RequestsCancelAsync(CancelRequestModel request);

        Task<BaseObjectResponse<string>> EventsSolidaryAsync(SolidaryEventModel request);

        Task<BaseObjectResponse<string>> EventsCancelAsync(CancelEventModel request);

        Task<BaseObjectResponse<string>> EventsVolunteerAsync(VolunteerAdhesionModel request, string idOrganizador);
    }
}
