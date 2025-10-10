using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Common.Model
{
    public class ValidationErrorResponse
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }

}
