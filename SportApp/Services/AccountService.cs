using AutoMapper;
using SportApp.Interfaces;
using SportApp.Models;
using SportApp.Entities;
using BCrypt.Net;
using SportApp.Data;
using Microsoft.EntityFrameworkCore;

namespace SportApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TokenService _tokenService;
        public AccountService(IMapper mapper, IServiceScopeFactory serviceScopeFactory, TokenService tokenService) 
        {
            _mapper = mapper;
            _scopeFactory = serviceScopeFactory;
            _tokenService = tokenService;
        }
        public async Task<LoginResponse> Login(LoginDto loginDto)
        {
            User user = new User();

            using (var scope = _scopeFactory.CreateScope())
            {
                var _dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
            }

            if (user is null)
            {
                throw new Exception("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new Exception("Incorrect password");
            }

            string token = _tokenService.CreateToken(user);

            var refreshToken = "";//_tokenService.GenerateRefreshToken();

            //_tokenService.SetRefreshToken(refreshToken, user);

            var response = new LoginResponse
            {
                UserId = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Patronymic = user.Patronymic,
                Email = user.Email,
                Token = token,
                RefreshToken = refreshToken,
                RoleId = user.RoleId
            };

            return response;
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
