namespace SportApp.Models
{
    public class LoginResponse
    {
        public long UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token {  get; set; }
        public RefreshToken RefreshToken { get; set; }

    }
}
