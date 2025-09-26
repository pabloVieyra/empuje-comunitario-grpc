using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Common.Model.EventDtos
{
    public class UpdateEventDto
    {
        [Required(ErrorMessage = "El token es obligatorio")]
        public string Token { get; set; } = string.Empty;

        [Required]
        public EventDto Event { get; set; } = new EventDto();
    }
}
