using AutoMapper;
using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.Client.Common.Model.EventDtos;
using EmpujeComunitario.Client.Services.Interface;
using Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Services.Implementation
{

    public class EventManagerServices : IEventManagerServices
    {
        private readonly EventService.EventServiceClient _eventClient;
        private readonly IMapper _mapper;
        public EventManagerServices (EventService.EventServiceClient eventClient, IMapper mapper)
        {
            _eventClient = eventClient;
            _mapper = mapper;
        }
        public async Task<BaseObjectResponse<GenericResponse>> CreateEventAsync(CreateEventDto createEvent, string auth)
        {
            BaseObjectResponse<GenericResponse> response = new BaseObjectResponse<GenericResponse>();
            try
            {
                CreateEventRequest createEventRequest = _mapper.Map<CreateEventRequest>(createEvent);
                createEventRequest.Token = auth;
                var result = await _eventClient.CreateEventAsync(createEventRequest);
                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }

        public async Task<BaseObjectResponse<UpdateEventResponse>> UpdateEventAsync(EventDto updateEvent, string auth)
        {
            BaseObjectResponse<UpdateEventResponse> response = new BaseObjectResponse<UpdateEventResponse>();
            try
            {
                UpdateEventRequest updateEventRequest= _mapper.Map<UpdateEventRequest>(updateEvent);
                updateEventRequest.Token = auth;
                //updateEventRequest.Event.ModificationDate = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.fff");
                var result = await _eventClient.UpdateEventAsync(updateEventRequest);
                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }
        public async Task<BaseObjectResponse<GenericResponse>> DeleteEventAsync(DeleteEventDto deleteEvent, string auth)
        {
            BaseObjectResponse<GenericResponse> response = new BaseObjectResponse<GenericResponse>();
            try
            {
                DeleteEventRequest deleteEventRequest = _mapper.Map<DeleteEventRequest>(deleteEvent);
                deleteEventRequest.Token = auth;
                var result = await _eventClient.DeleteEventAsync(deleteEventRequest);
                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }

        public async Task<BaseObjectResponse<FindEventByIdResponse>> DeleteEventAsync(FindEventByIdDto findEvent, string auth)
        {
            BaseObjectResponse<FindEventByIdResponse> response = new BaseObjectResponse<FindEventByIdResponse>();
            try
            {
                FindEventByIdRequest findEventByIdRequest = _mapper.Map<FindEventByIdRequest>(findEvent);
                findEventByIdRequest.Token = auth;
                var result = await _eventClient.FindEventByIdAsync(findEventByIdRequest);
                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }

        public async Task<BaseObjectResponse<ListEventsResponse>> ListEventsAsync(string auth)
        {
            BaseObjectResponse<ListEventsResponse> response = new BaseObjectResponse<ListEventsResponse>();
            try
            {
                ListEventsRequest listEventsRequest = new ListEventsRequest();
                listEventsRequest.Token = auth;
                var result = await _eventClient.ListEventsAsync(listEventsRequest);
                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }
        public async Task<BaseObjectResponse<GenericResponse>> AddUserToEventAsync(AddUserToEventDto addUser, string auth)
        {
            BaseObjectResponse<GenericResponse> response = new BaseObjectResponse<GenericResponse>();
            try
            {
                AddUserToEventRequest addUserToEventRequest = _mapper.Map<AddUserToEventRequest>(addUser);
                addUserToEventRequest.Token = auth;
                var result = await _eventClient.AddUserToEventAsync(addUserToEventRequest);
                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }
        public async Task<BaseObjectResponse<GenericResponse>> RemoveUserFromEventAsync(RemoveUserFromEventDto removeUser, string auth)
        {
            BaseObjectResponse<GenericResponse> response = new BaseObjectResponse<GenericResponse>();
            try
            {
                RemoveUserFromEventRequest removeUserFromEvent = _mapper.Map<RemoveUserFromEventRequest>(removeUser);
                removeUserFromEvent.Token = auth;
                var result = await _eventClient.RemoveUserFromEventAsync(removeUserFromEvent);
                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }
        public async Task<BaseObjectResponse<GenericResponse>> RegisterDonationToEventAsync(RegisterDonationDto register, string auth)
        {
            BaseObjectResponse<GenericResponse> response = new BaseObjectResponse<GenericResponse>();
            try
            {
                RegisterDonationRequest registerDonationRequest = _mapper.Map<RegisterDonationRequest>(register);
                registerDonationRequest.Token = auth;
                var result = await _eventClient.RegisterDonationToEventAsync(registerDonationRequest);
                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }

    }
}
