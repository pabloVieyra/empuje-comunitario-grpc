using System.ComponentModel.DataAnnotations;

namespace EmpujeComunitario.Client.Common.Model
{
    public class CreateUserRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
