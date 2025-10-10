using AutoMapper;
using EmpujeComunitario.MessageFlow.Common.Constants;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using EmpujeComunitario.MessageFlow.DataAccess.Interface;
using EmpujeComunitario.MessageFlow.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Service.Implementation
{
    public class EventSolidaryService : IEventSolidaryService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public EventSolidaryService(IEventRepository eventRepository, IMapper mapper) 
        { 
            _eventRepository = eventRepository;
            _mapper = mapper;

        }
        public async Task CreateEvent(SolidaryEventModel request)
        {
            try
            {
                if(request.OrgId != Organization.Id)
                {
                    var requestCancel = await _eventRepository.FindRequestCancel(request.EventId);
                    if (requestCancel == null)
                    {
                        var eventSolidary = _mapper.Map<SolidaryEvent>(request);
                        await _eventRepository.AddSolidaryEventAsync(eventSolidary);
                    }else
                    {
                        Console.WriteLine("Evento cancelado");
                    }
                }
                else
                {
                    Console.WriteLine("No se guardan eventos propios");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public async Task CreateRequestCancellEvent(CancelEventModel request)
        {
            try
            {
                var requestCancel = _mapper.Map<CancelledEvent>(request);
                await _eventRepository.CreateRequestCancelEvent(requestCancel);
                await _eventRepository.CancelEventAsync(request.EventId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
