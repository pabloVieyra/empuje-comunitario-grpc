using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Common.Model
{
    public class UpdateUserRequestDto : CreateUserRequestDto
    {
        public string Id { get; set; }
        public bool Active { get; set; }

    }
}
