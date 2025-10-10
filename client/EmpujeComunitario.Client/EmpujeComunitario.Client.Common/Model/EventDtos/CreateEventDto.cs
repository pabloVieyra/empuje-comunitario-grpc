using System.ComponentModel.DataAnnotations;

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
        public DateTime EventDateTime { get; set; }
        [Required(ErrorMessage = "El usuario de creación del evento es obligatorio")]
        public string CreationUserId { get; set; }
    }
}
