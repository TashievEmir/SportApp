using SportApp.Entities;

namespace SportApp.Models
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Photo { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
    }
}
