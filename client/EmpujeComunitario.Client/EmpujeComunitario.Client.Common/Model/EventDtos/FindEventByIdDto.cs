using System.ComponentModel.DataAnnotations;


namespace EmpujeComunitario.Client.Common.Model.EventDtos
{
    public class FindEventByIdDto
    {
        [Required]
        public int EventId { get; set; }
    }
}
