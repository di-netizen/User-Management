using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Dtos
{
    public class RegisterDto
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; } = string.Empty;
        public string? EmployeeNumber { get; set; } = string.Empty;

        [Required] public string Password { get; set; } = string.Empty;
    }
}
