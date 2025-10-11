using EmpujeComunitario.Client.Services.Infrastructure;
using EmpujeComunitario.MessageFlow.Common.Model;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using EmpujeComunitario.MessageFlow.WebClient.Interface;

namespace EmpujeComunitario.Client.Services.Implementation
{
    public class ExternalDataService : IExternalDataService
    {
        private readonly IExternalDataClient _externalDataClient;

        public ExternalDataService(IExternalDataClient externalDataClient)
        {
            _externalDataClient = externalDataClient;
        }

        private async Task<BaseObjectResponse<T>> SafeCall<T>(Func<Task<BaseObjectResponse<T>>> func)
        {
            try
            {
                var response = await func();

                if (response.StatusCode != 200)
                    return new BaseObjectResponse<T>().ExceptionWithData(response.Message);

                if (response.Errors != null && response.Errors.Any())
                    return new BaseObjectResponse<T>().BadRequestWithoutData(
                        "Errores: " + string.Join("; ", response.Errors.Select(e => $"{e.Field}: {e.Message}"))
                    );

                return response.OkWithData(response.Data, response.Message);
            }
            catch (Exception ex)
            {
                return new BaseObjectResponse<T>().ExceptionWithData(ex.Message);
            }
        }

        public Task<BaseObjectResponse<List<SolidaryEventModel>>> GetAllEvents() =>
            SafeCall(() => _externalDataClient.GetAllEvents());

        public Task<BaseObjectResponse<List<VolunteerAdhesionModel>>> GetAllVolunteerByEvent(string eventId) =>
            SafeCall(() => _externalDataClient.GetAllVolunteerByEvent(eventId));

        public Task<BaseObjectResponse<List<OfferDonationModel>>> GetAllOfferDonation() =>
            SafeCall(() => _externalDataClient.GetAllOfferDonation());

        public Task<BaseObjectResponse<List<RequestDonationModel>>> GetAllRequestsDonation() =>
            SafeCall(() => _externalDataClient.GetAllRequestsDonation());

    }
}
