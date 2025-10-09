using AutoMapper;
using EmpujeComunitario.MessageFlow.Common.Constants;
using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using EmpujeComunitario.MessageFlow.Common.Settings;
using EmpujeComunitario.MessageFlow.DataAccess.Entities;
using EmpujeComunitario.MessageFlow.DataAccess.Interface;
using EmpujeComunitario.MessageFlow.Service.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Service.Implementation
{
    public class MessagesConsumerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IMapper _mapper;
        private readonly RabbitMqSettings _rabbitMq;
        //private readonly Guid _organizationId = Guid.Parse("ae37c78b-6362-410e-894d-73fff7843bea");


        public MessagesConsumerService(IServiceScopeFactory scopeFactory, IOptions<RabbitMqSettings> options, IMapper mapper)
        {
            _rabbitMq = options.Value;
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            // Configuración de RabbitMQ
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMq.HostName,
                UserName = _rabbitMq.UserName,
                Password = _rabbitMq.Password,
                Port = Convert.ToInt32(_rabbitMq.Port)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();


        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CrearConsumer(Exchanges.ONG_EXCHANGE_NAME, $"queue.request-donation.{Organization.Id}", Exchanges.RoutingKeyRequestDonation, typeof(RequestDonationModel));
            CrearConsumer(Exchanges.ONG_EXCHANGE_NAME, $"queue.transferencia-donaciones.{Organization.Id}", string.Format(Exchanges.RoutingKeyTransferDonation, Organization.Id), typeof(CancelRequestModel));
            CrearConsumer(Exchanges.ONG_EXCHANGE_NAME, $"queue.offer-donation.{Organization.Id}", Exchanges.RoutingKeyOfferDonation, typeof(OfferDonationModel));
            CrearConsumer(Exchanges.ONG_EXCHANGE_NAME, $"queue.donation.deleted.{Organization.Id}", Exchanges.RoutingKeyRequestCancel, typeof(CancelRequestModel));
            CrearConsumer(Exchanges.ONG_EXCHANGE_NAME, $"queue.events-solidary.{Organization.Id}", Exchanges.RoutingKeyEventSolidary, typeof(SolidaryEventModel));
            CrearConsumer(Exchanges.ONG_EXCHANGE_NAME, $"queue.events-cancel.{Organization.Id}", Exchanges.RoutingKeyEventCancel, typeof(CancelEventModel));
            CrearConsumer(Exchanges.ONG_EXCHANGE_NAME, $"queue.events-volunteer.{Organization.Id}", string.Format(Exchanges.RoutingKeyEventVolunteer,Organization.Id), typeof(VolunteerAdhesionModel));

            return Task.CompletedTask;
        }
        private void CrearConsumer(string exchange, string queueName, string bindingKey, Type messageType)
        {
            // Declaramos el exchange tipo Topic
            _channel.ExchangeDeclare(exchange, ExchangeType.Topic, durable: true);

            // Declaramos la cola
            _channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false);

            // Bind de la cola al exchange con la routingKey
            _channel.QueueBind(queueName, exchange, bindingKey);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                Task.Run(async () =>
                {


                    var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                    using var scope = _scopeFactory.CreateScope();

                    try
                    {
                        // Switch usando tus constantes
                        switch (ea.RoutingKey)
                        {
                            //1
                            case var rk when rk == Exchanges.RoutingKeyRequestDonation:
                                var solicitud = JsonConvert.DeserializeObject<RequestDonationModel>(json);
                                var donationService = scope.ServiceProvider.GetRequiredService<IRequestDonationService>();
                                await donationService.CreateRequest(solicitud);
                                break;
                             //2
                            case var rk when rk == string.Format(Exchanges.RoutingKeyTransferDonation, Organization.Id):

                                var transferencia = JsonConvert.DeserializeObject<TransferDonationModel>(json);
                                var transferRepository = scope.ServiceProvider.GetRequiredService<ITransferRepository>();
                                var entityTransferencia = _mapper.Map<DonationTransfer>(transferencia);
                                await transferRepository.ConfirmTransferAsync(entityTransferencia);
                                break;
                            //3
                            case var rk when rk == Exchanges.RoutingKeyOfferDonation:
                                var oferta = JsonConvert.DeserializeObject<OfferDonationModel>(json);
                                var offerRepository = scope.ServiceProvider.GetRequiredService<IOfferRepository>();
                                var entityOferta = _mapper.Map<DonationOffer>(oferta);
                                await offerRepository.AddOfferDonationAsync(entityOferta);
                                break;

                            //
                            case var rk when rk == Exchanges.RoutingKeyRequestCancel:
                                var cancelRequest = JsonConvert.DeserializeObject<CancelRequestModel>(json);
                                var donationRepo = scope.ServiceProvider.GetRequiredService<IDonationRequestRepository>();
                                await donationRepo.CancelDonationRequestAsync(cancelRequest.RequestId);
                                break;

                            case var rk when rk == Exchanges.RoutingKeyEventSolidary:
                                var evento = JsonConvert.DeserializeObject<SolidaryEventModel>(json);
                                var eventRepository = scope.ServiceProvider.GetRequiredService<IEventRepository>();
                                var entityEvento = _mapper.Map<SolidaryEvent>(evento);
                                await eventRepository.AddSolidaryEventAsync(entityEvento);
                                break;

                            case var rk when rk == Exchanges.RoutingKeyEventCancel:
                                var cancelEvent = JsonConvert.DeserializeObject<CancelEventModel>(json);
                                var eventRepo = scope.ServiceProvider.GetRequiredService<IEventRepository>();
                                await eventRepo.CancelEventAsync(cancelEvent.EventId);
                                break;

                            case var rk when rk == string.Format(Exchanges.RoutingKeyEventVolunteer,Organization.Id):
                                var adhesion = JsonConvert.DeserializeObject<VolunteerAdhesionModel>(json);
                                var volunteerRepository = scope.ServiceProvider.GetRequiredService<IVolunteerRepository>();
                                var entityAdhesion = _mapper.Map<VolunteerAdhesion>(adhesion);
                                await volunteerRepository.AddVolunteerAdhesionAsync(entityAdhesion);
                                break;

                            default:
                                Console.WriteLine($"⚠️ RoutingKey desconocida: {ea.RoutingKey}");
                                break;
                        }

                        Console.WriteLine($"📥 Mensaje procesado desde {ea.Exchange} con routingKey {ea.RoutingKey}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ Error procesando mensaje: {ex.Message}");
                    }
                });
            }; 
            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }

    }
}
