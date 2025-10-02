using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Common.Model.EventDtos
{
    public class RemoveUserFromEventDto
    {


        [Required]
        public int EventId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string ActorId { get; set; } = string.Empty;
    }
}
