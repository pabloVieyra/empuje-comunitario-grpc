namespace EmpujeComunitario.Client.Common.Model
{
    public class UpdateUserRequestDto : CreateUserRequestDto
    {
        public string Id { get; set; }
        public bool Active { get; set; }

    }
}
