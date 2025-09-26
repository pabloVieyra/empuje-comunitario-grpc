using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Common.Model.EventDtos
{
    public class DeleteEventDto
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        public string ActorId { get; set; } = string.Empty;
    }
}
