using AutoMapper;
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
    public class VolunteerAdhesionService : IVolunteerAdhesionService
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IMapper _mapper;
        public VolunteerAdhesionService(IVolunteerRepository volunteerRepository, IMapper mapper)
        {
            _volunteerRepository = volunteerRepository;
            _mapper = mapper;
        }
        public async Task CreateVolunteerAdhesion(VolunteerAdhesionModel request)
        {
            try
            {
                var adhesion = _mapper.Map<VolunteerAdhesion>(request);
                await _volunteerRepository.AddVolunteerAdhesionAsync(adhesion);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
