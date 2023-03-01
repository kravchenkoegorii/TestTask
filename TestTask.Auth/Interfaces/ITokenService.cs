using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Domain.Models;

namespace TestTask.Auth.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
