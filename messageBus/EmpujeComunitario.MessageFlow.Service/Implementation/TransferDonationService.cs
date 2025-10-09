using AutoMapper;
using EmpujeComunitario.MessageFlow.Common.Constants;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using EmpujeComunitario.MessageFlow.DataAccess.Context;
using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using EmpujeComunitario.MessageFlow.DataAccess.Implementation;
using EmpujeComunitario.MessageFlow.DataAccess.Interface;
using EmpujeComunitario.MessageFlow.Service.Infrastructure;
using EmpujeComunitario.MessageFlow.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Service.Implementation
{
    public class TransferDonationService : ITransferDonationService
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly MessageFlowDbContext _context;
        private readonly IDonationRequestRepository _donationRequestRepository;
        private readonly ITransferRepository _transfersRepository;
        private readonly IDonationsRepository _donationsRepository;
        private readonly IMapper _mapper;
        public TransferDonationService(IRabbitMqService rabbitMqService,
            IDonationRequestRepository  donationRequestRepository,
            ITransferRepository transfersRepository,
            IDonationsRepository donationsRepository,
            IMapper mapper,
            MessageFlowDbContext context ) 
        { 
            _rabbitMqService = rabbitMqService;
            _transfersRepository = transfersRepository;
            _donationsRepository = donationsRepository;
            _donationRequestRepository = donationRequestRepository;
            _mapper = mapper;
            _context = context;
        }
        public async Task ConfirmTransferAsync(TransferDonationModel transferMessage)
        {
            
            try
            {
                // 1️⃣ Obtener la solicitud correspondiente
                var solicitud = await _donationRequestRepository.GetByIdAsync(transferMessage.RequestId);
                   
                if (solicitud == null)
                    throw new Exception("Solicitud no encontrada.");

                if (solicitud.IsCancelled == true)
                    throw new Exception("Solicitud ya fue cancelada.");

                // 2️⃣ Actualizar inventario de tu organización
                foreach (var item in transferMessage.Donations)
                {
                    var inventoryItem = await _donationsRepository.GetDonation(item.Description, item.Category);

                    if (inventoryItem == null)
                        throw new Exception($"No hay inventario suficiente para {item.Description}");

                    // Si somos la organización donante
                    if (transferMessage.DonationOrgId == Organization.Id)
                    {
                        if (inventoryItem.Quantity < item.Quantity)
                            throw new Exception($"Cantidad insuficiente en inventario para {item.Description}");

                        await _donationsRepository.UppdateDonation(inventoryItem.Id, item.Quantity*-1);
                    }
                    else
                    {
                        // Si somos la receptora
                        await _donationsRepository.UppdateDonation(inventoryItem.Id, item.Quantity);
                    }
                }

                // 3️⃣ Guardar la transferencia
                var entityTransferencia = _mapper.Map<DonationTransfer>(transferMessage);
                await _transfersRepository.ConfirmTransferAsync(entityTransferencia);

                // 4️⃣ Confirmación a la organización solicitante
                var routingKey = string.Format(Exchanges.RoutingKeyTransferDonation,transferMessage.DonationOrgId);
                _rabbitMqService.Publish(routingKey, JsonConvert.SerializeObject(transferMessage));

            }
            catch
            {
                throw;
            }
        }
    }
}
