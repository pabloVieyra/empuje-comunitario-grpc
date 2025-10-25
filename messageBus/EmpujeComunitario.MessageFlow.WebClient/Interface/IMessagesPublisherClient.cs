using EmpujeComunitario.MessageFlow.Common.Model;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.WebClient.Interface
{
    public interface IMessagesPublisherClient
    {
        // Punto 1
        [Post("/MessagesPublisher/solicitud-donaciones")]
        Task<BaseObjectResponse<string>> RequestDonation([Body] RequestDonationModel request,[Header("UserId")] string userId);

        // Punto 2
        [Post("/MessagesPublisher/transferencia-donaciones/{IdOrganizacionSolicitante}")]
        Task<BaseObjectResponse<string>> TransfersDonation([Body] TransferDonationModel request, string IdOrganizacionSolicitante, [Header("UserId")] string userId);

        // Punto 3
        [Post("/MessagesPublisher/oferta-donaciones")]
        Task<BaseObjectResponse<string>> OffersDonations([Body] OfferDonationModel request, [Header("UserId")] string userId);

        // Punto 4
        [Post("/MessagesPublisher/baja-solicitud-donaciones")]
        Task<BaseObjectResponse<string>> RequestsCancel([Body] CancelRequestModel request);

        // Punto 5
        [Post("/MessagesPublisher/eventossolidarios")]
        Task<BaseObjectResponse<string>> EventsSolidary([Body] SolidaryEventModel request);

        // Punto 6
        [Post("/MessagesPublisher/baja-evento-solidario")]
        Task<BaseObjectResponse<string>> EventsCancel([Body] CancelEventModel request);

        // Punto 7
        [Post("/MessagesPublisher/adhesion-evento/{idOrganizador}")]
        Task<BaseObjectResponse<string>> EventsVolunteer([Body] VolunteerAdhesionModel request,string idOrganizador);
    }

}
