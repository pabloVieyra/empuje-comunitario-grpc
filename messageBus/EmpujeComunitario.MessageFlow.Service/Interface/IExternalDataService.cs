using EmpujeComunitario.MessageFlow.Common.Model;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Service.Interface
{
    public interface IExternalDataService
    {
        Task<BaseObjectResponse<List<SolidaryEventModel>>> GetAllEvents();
        Task<BaseObjectResponse<List<VolunteerAdhesionModel>>> GetAllVolunteerByEvent(string eventId);
        Task<BaseObjectResponse<List<OfferDonationModel>>> GetAllOfferDonation();
    }
}
