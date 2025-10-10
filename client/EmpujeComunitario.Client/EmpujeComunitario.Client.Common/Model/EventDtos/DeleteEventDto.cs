using System.ComponentModel.DataAnnotations;

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
