namespace EmpujeComunitario.Client.Common.Model
{
    /// <summary>
    /// Modelo para crear un nuevo usuario.
    /// </summary>
    /// <example>
    /// {
    ///   "UsernameOrEmail": "jdoe",
    ///   "Password": "1234",
    /// }
    /// </example>
    public class LoginRequestDto
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }


    }
}
