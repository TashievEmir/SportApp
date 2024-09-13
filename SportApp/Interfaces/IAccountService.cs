using SportApp.Models;

namespace SportApp.Interfaces
{
    public interface IAccountService
    {
        public Task<LoginResponse> Login();
        public Task<> Register();

    }
}
