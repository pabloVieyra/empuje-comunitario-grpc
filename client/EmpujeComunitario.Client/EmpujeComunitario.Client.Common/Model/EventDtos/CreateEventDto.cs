using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Common.Model.EventDtos
{
    public class CreateEventDto
    {

        [Required(ErrorMessage = "El nombre del evento es obligatorio")]
        [StringLength(100)]
        public string EventName { get; set; } 

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500)]
        public string Description { get; set; } 

        [Required(ErrorMessage = "La fecha y hora del evento es obligatoria")]
        public string EventDateTime { get; set; }
        [Required(ErrorMessage = "El usuario de creación del evento es obligatorio")]
        public string CreationUserId { get; set; }
    }
}
