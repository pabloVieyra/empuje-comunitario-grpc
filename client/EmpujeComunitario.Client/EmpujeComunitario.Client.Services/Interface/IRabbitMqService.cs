using EmpujeComunitario.Client.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Services.Interface
{
    public interface IRabbitMqService
    {
        BaseObjectResponse<string> Publish(string routingKey, string message);
    }
}
