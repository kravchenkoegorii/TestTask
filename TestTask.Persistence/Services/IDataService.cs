using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Domain.Models;

namespace TestTask.Persistence.Services
{
    public interface IDataService
    {
        Task<ICollection<Professor>> GetAllUsingEF();

        Task<ICollection<Professor>> GetAllUsingDapper();
    }
}
