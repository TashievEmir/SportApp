﻿using SportApp.Entities;
using SportApp.Models;

namespace SportApp.Interfaces
{
    public interface IAccountService
    {
        public Task<LoginResponse> Login(LoginDto loginDto);
        public Task RegisterAsync(RegisterDto registerDto);
        User GetById(int userId);

    }
}
