using System.ComponentModel.DataAnnotations;

namespace EmpujeComunitario.Client.Common.Model.EventDtos
{
    public class EventDto
    {
        public int Id { get; set; }
        [Required]
        public string EventName { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string EventDateTime { get; set; } = string.Empty;

        public string ModificationUser { get; set; } = string.Empty;
    }
}
