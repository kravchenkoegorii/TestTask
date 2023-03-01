using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Domain.Models;

namespace TestTask.Persistence.Database.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasData(
                new Student
                {
                    Id = 1,
                    Name = "A",
                    Surname = "A",
                    Group = "PZ-21-3",
                    ProfessorId = 1
                },
                new Student
                {
                    Id = 2,
                    Name = "B",
                    Surname = "B",
                    Group = "PZ-21-3",
                    ProfessorId = 1
                },
                new Student
                {
                    Id = 3,
                    Name = "C",
                    Surname = "C",
                    Group = "PZ-21-2",
                    ProfessorId = 2
                },
                new Student
                {
                    Id = 4,
                    Name = "D",
                    Surname = "D",
                    Group = "PZ-21-2",
                    ProfessorId = 2
                },
                new Student
                {
                    Id = 5,
                    Name = "E",
                    Surname = "E",
                    Group = "PZ-21-2",
                    ProfessorId = 3
                },
                new Student
                {
                    Id = 6,
                    Name = "F",
                    Surname = "F",
                    Group = "PZ-21-1",
                    ProfessorId = 3
                },
                new Student
                {
                    Id = 7,
                    Name = "G",
                    Surname = "G",
                    Group = "PZ-21-1",
                    ProfessorId = 3
                });
        }
    }
}
