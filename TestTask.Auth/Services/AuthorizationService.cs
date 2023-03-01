using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestTask.API.DTOs;
using TestTask.Auth.Exceptions;
using TestTask.Auth.Interfaces;
using TestTask.Domain.Models;
using TestTask.Persistence.Database;

namespace TestTask.Auth.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly TestTaskDbContext _dbContext;

        private readonly ITokenService _tokenService;

        public AuthorizationService(TestTaskDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await _dbContext.Users
                .SingleOrDefaultAsync(a => a.Username == loginDto.Username);

            if (user == null)
            {
                throw new UnauthorizedException("Invalid Username!");
            }  

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    throw new UnauthorizedException("Invalid Password!");
                }
            }

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }


        public async Task<bool> UserExists(string username)
        {
            return await _dbContext.Users.AnyAsync(x => x.Username == username);
        }
    }
}
