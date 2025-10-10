using EmpujeComunitario.MessageFlow.Common.Model.MessagesRabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Service.Interface
{
    public interface IEventSolidaryService
    {
        Task CreateRequestCancellEvent(CancelEventModel request);
        Task CreateEvent(SolidaryEventModel request);
    }
}
