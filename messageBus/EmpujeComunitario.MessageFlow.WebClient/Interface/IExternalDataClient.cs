using EmpujeComunitario.MessageFlow.Common.Model;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using Refit;

namespace EmpujeComunitario.MessageFlow.WebClient.Interface
{
    public interface IExternalDataClient
    {
        [Get("/ExternalData/GetAllEvents")]
        Task<BaseObjectResponse<List<SolidaryEventModel>>> GetAllEvents();
        [Get("/ExternalData/GetAllVolunteerByEvent/{eventId}")]
        Task<BaseObjectResponse<List<VolunteerAdhesionModel>>> GetAllVolunteerByEvent(string eventId);
        [Get("/ExternalData/GetAllOfferDonation")]
        Task<BaseObjectResponse<List<OfferDonationModel>>> GetAllOfferDonation();
    }
}
