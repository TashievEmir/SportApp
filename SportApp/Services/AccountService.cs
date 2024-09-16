using AutoMapper;
using SportApp.Interfaces;
using SportApp.Models;
using SportApp.Entities;
using BCrypt.Net;
using SportApp.Data;

namespace SportApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;
        public AccountService(IMapper mapper, IServiceScopeFactory serviceScopeFactory) 
        {
            _mapper = mapper;
            _scopeFactory = serviceScopeFactory;
        }
        public Task<LoginResponse> Login()
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            user.CreatedAt = DateTime.Now;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            using (var scope = _scopeFactory.CreateScope())
            {
                var _dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                _dataContext.Users.Add(user);
                await _dataContext.SaveChangesAsync();
            }
            
        }
    }
}
