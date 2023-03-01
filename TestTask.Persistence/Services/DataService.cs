using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Domain.Models;
using TestTask.Persistence.Database;

namespace TestTask.Persistence.Services
{
    public class DataService : IDataService
    {
        private readonly TestTaskDbContext _dbContext;

        private readonly IConfiguration _configuration;

        public DataService(TestTaskDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<ICollection<Professor>> GetAllUsingEF()
        {
            return await _dbContext.Professors
                .Include(prof => prof.Students)
                .ToListAsync();
        }

        public async Task<ICollection<Professor>> GetAllUsingDapper()
        {
            var connString = _configuration.GetValue<string>("ConnectionString");
            using IDbConnection db = new SqlConnection(connString);

            var sqlQuery = "SELECT [p].[Id], [p].[Name], [p].[SubjectName], [p].[Surname], " +
                "[s].[Id] AS StudId, [s].[Group] AS StudGroup, [s].[Name] AS StudName, [s].[ProfessorId] AS ProfId, [s].[Surname] AS StudSurname " +
                "FROM [Professors] AS [p] " +
                "LEFT JOIN [Students] AS [s] ON [p].[Id] = [s].[ProfessorId] " +
                "ORDER BY [p].[Id]";

            var results = await db.QueryAsync<Professor>(sqlQuery);

            return results.ToList();
        }
    }
}