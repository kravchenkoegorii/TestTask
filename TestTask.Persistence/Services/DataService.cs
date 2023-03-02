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
            var connString = _configuration.GetSection("Config:ConnectionString").Value;
            using IDbConnection db = new SqlConnection(connString);

            var lookup = new Dictionary<int, Professor>();

            var sqlQuery = @"SELECT p.*, s.* " +
                "FROM [Professors] AS [p] " +
                "LEFT JOIN [Students] AS [s] ON [p].[Id] = [s].[ProfessorId]";

            db.Query<Professor, Student, Professor>(sqlQuery, (p, s) =>
            {
                Professor professor;
                if (!lookup.TryGetValue(p.Id, out professor))
                    lookup.Add(p.Id, professor = p);
                if (professor.Students == null)
                    professor.Students = new List<Student>();
                professor.Students.Add(s);
                return professor;
            }).AsQueryable();

            var resultList = lookup.Values;

            return resultList;
        }
    }
}