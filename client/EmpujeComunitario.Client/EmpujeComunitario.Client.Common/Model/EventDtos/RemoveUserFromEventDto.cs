using System.ComponentModel.DataAnnotations;

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
