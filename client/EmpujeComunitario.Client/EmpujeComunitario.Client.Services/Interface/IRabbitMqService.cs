using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Services.Interface
{
    public interface IRabbitMqService
    {
        void Publish(string queueName, string message);
    }
}
