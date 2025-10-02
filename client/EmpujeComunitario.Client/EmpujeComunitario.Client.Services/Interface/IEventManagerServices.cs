using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.Client.Common.Model.EventDtos;
using Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Services.Interface
{
    public interface IEventManagerServices
    {
        Task<BaseObjectResponse<GenericResponse>> CreateEventAsync(CreateEventDto createEvent, string auth);
        Task<BaseObjectResponse<UpdateEventResponse>> UpdateEventAsync(EventDto updateEvent, string auth);
        Task<BaseObjectResponse<GenericResponse>> DeleteEventAsync(DeleteEventDto deleteEvent, string auth);
        Task<BaseObjectResponse<FindEventByIdResponse>> DeleteEventAsync(FindEventByIdDto findEvent, string auth);
        Task<BaseObjectResponse<ListEventsResponse>> ListEventsAsync(string auth);
        Task<BaseObjectResponse<GenericResponse>> AddUserToEventAsync(AddUserToEventDto addUser, string auth);
        Task<BaseObjectResponse<GenericResponse>> RemoveUserFromEventAsync(RemoveUserFromEventDto removeUser, string auth);
        Task<BaseObjectResponse<GenericResponse>> RegisterDonationToEventAsync(RegisterDonationDto register, string auth);
    }
}
