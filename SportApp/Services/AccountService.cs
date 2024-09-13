﻿using AutoMapper;
using SportApp.Interfaces;
using SportApp.Models;
using SportApp.Entities;

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

        public Task Register(RegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            user.CreatedAt = DateTime.Now;

            using (var scope = _scopeFactory.CreateScope())
            {

            }
                throw new NotImplementedException();
        }
    }
}
