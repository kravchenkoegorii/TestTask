using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.API.DTOs;

namespace TestTask.Auth.Interfaces
{
    public interface IAuthorizationService
    {
        Task<UserDto> Register(RegisterDto registerDto);

        Task<UserDto> Login(LoginDto loginDto);

        Task<bool> UserExists(string username);
    }
}
