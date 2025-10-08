using EmpujeComunitario.MessageFlow.Common.Model;
using EmpujeComunitario.MessageFlow.Common.Settings;
using EmpujeComunitario.MessageFlow.Service.Interface;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Service.Implementation
{
    public class RabbitMqService : IRabbitMqService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _model;
        private readonly RabbitMqSettings _rabbitMq;
        private const string ONG_EXCHANGE_NAME = "ong_network.exchange";
        private const string Successful = "Mensaje encolado correctamente.";
        private const string Unsuccessful = "Ocurrio un error al enviar el mensaje.";
        public RabbitMqService(IOptions<RabbitMqSettings> options)
        {
            _rabbitMq = options.Value;
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMq.HostName,
                UserName = _rabbitMq.UserName,
                Password = _rabbitMq.Password,
                Port = Convert.ToInt32(_rabbitMq.Port)
            };
            _connection = factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(
                exchange: ONG_EXCHANGE_NAME,
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false,
                arguments: null
            );
        }
        public BaseObjectResponse<string> Publish(string routingKey, string message)
        {
            var response = new BaseObjectResponse<string>();
            try
            {
                var body = Encoding.UTF8.GetBytes(message);
                ReadOnlyMemory<byte> bodyMemory = new ReadOnlyMemory<byte>(body);

                _model.BasicPublish(
                     exchange: ONG_EXCHANGE_NAME,
                     routingKey: routingKey,
                     mandatory: true,
                     basicProperties: null,
                     body: bodyMemory
                );

                return response.OkWithData(Successful);
            }
            catch (Exception e)
            {
                return response.BadRequestWithoutData(Unsuccessful);
            }

        }

        void IDisposable.Dispose()
        {
            _model?.Close();
            _model?.Dispose();
            _connection?.Close();
            _connection.Dispose();
        }
    }
}
