using EmpujeComunitario.MessageFlow.Common.Model;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;

namespace EmpujeComunitario.Client.Services.Infrastructure
{
    public interface IExternalDataService
    {
        Task<BaseObjectResponse<List<SolidaryEventModel>>> GetAllEvents();

        Task<BaseObjectResponse<List<VolunteerAdhesionModel>>> GetAllVolunteerByEvent(string eventId);

        Task<BaseObjectResponse<List<OfferDonationModel>>> GetAllOfferDonation();
    }
}
