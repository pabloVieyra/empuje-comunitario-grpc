using EmpujeComunitario.Client.Services.Interface;
using EmpujeComunitario.MessageFlow.Common.Model;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using EmpujeComunitario.MessageFlow.WebClient.Interface;


namespace EmpujeComunitario.Client.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly IMessagesPublisherClient _messagesPublisherClient;
        public MessageService(IMessagesPublisherClient messagesPublisherClient)
        {
            _messagesPublisherClient = messagesPublisherClient;
        }

        private async Task<BaseObjectResponse<string>> SafeCall(Func<Task<BaseObjectResponse<string>>> func)
        {
            try
            {
                var response = await func();


                if (response.StatusCode != 200)
                    return new BaseObjectResponse<string>().ExceptionWithData(response.Message);

                if (response.Errors != null && response.Errors.Any())
                    return new BaseObjectResponse<string>().BadRequestWithoutData(
                        "Errores: " + string.Join("; ", response.Errors.Select(e => $"{e.Field}: {e.Message}"))
                    );

                return response.OkWithData(response.Data, response.Message);
            }
            catch (Exception ex)
            {
                return new BaseObjectResponse<string>().ExceptionWithData(ex.Message);
            }
        }

        public Task<BaseObjectResponse<string>> RequestDonationAsync(RequestDonationModel request, string userid) =>
            SafeCall(() => _messagesPublisherClient.RequestDonation(request, userid));

        public Task<BaseObjectResponse<string>> TransfersDonationAsync(TransferDonationModel request, string idOrganizacionSolicitante, string userid) =>
            SafeCall(() => _messagesPublisherClient.TransfersDonation(request, idOrganizacionSolicitante, userid));

        public Task<BaseObjectResponse<string>> OffersDonationsAsync(OfferDonationModel request, string userid) =>
            SafeCall(() => _messagesPublisherClient.OffersDonations(request, userid));

        public Task<BaseObjectResponse<string>> RequestsCancelAsync(CancelRequestModel request) =>
            SafeCall(() => _messagesPublisherClient.RequestsCancel(request));

        public Task<BaseObjectResponse<string>> EventsSolidaryAsync(SolidaryEventModel request) =>
            SafeCall(() => _messagesPublisherClient.EventsSolidary(request));

        public Task<BaseObjectResponse<string>> EventsCancelAsync(CancelEventModel request) =>
            SafeCall(() => _messagesPublisherClient.EventsCancel(request));

        public Task<BaseObjectResponse<string>> EventsVolunteerAsync(VolunteerAdhesionModel request, string idOrganizador) =>
            SafeCall(() => _messagesPublisherClient.EventsVolunteer(request, idOrganizador));
    }
}
