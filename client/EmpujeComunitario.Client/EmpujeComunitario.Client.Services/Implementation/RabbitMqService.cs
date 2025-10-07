using EmpujeComunitario.Client.Common.Settings;
using EmpujeComunitario.Client.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;


namespace EmpujeComunitario.Client.Services.Implementation
{
    public class RabbitMqService : IRabbitMqService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _model;
        private readonly RabbitMq _rabbitMq;
        private const string ONG_EXCHANGE_NAME = "ong_network.exchange";
        public RabbitMqService(IOptions<RabbitMq> options)
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
        public void Publish(string routingKey, string message)
        {


            var body = Encoding.UTF8.GetBytes(message);
            ReadOnlyMemory<byte> bodyMemory = new ReadOnlyMemory<byte>(body);

            _model.BasicPublish(
                 exchange: ONG_EXCHANGE_NAME,
                 routingKey: routingKey,
                 mandatory: false,
                 basicProperties: null,
                 body: bodyMemory
            );
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
