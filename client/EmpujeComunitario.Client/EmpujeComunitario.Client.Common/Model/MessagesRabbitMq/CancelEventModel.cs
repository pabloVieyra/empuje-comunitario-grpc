﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Common.Model.MessagesRabbitMq
{
    public class CancelEventModel
    {
        public string OrgId { get; set; }
        public string EventId { get; set; }
    }
}
