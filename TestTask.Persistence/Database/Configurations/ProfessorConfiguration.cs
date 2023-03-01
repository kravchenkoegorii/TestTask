using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TestTask.Domain.Models;

namespace TestTask.Persistence.Database.Configurations
{
    public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable("Professors");

            builder.HasMany(x => x.Students)
                 .WithOne(x => x.Professor)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Professor
                {
                    Id = 1,
                    Name = "Vladimir",
                    Surname = "Surname",
                    SubjectName = "Math"
                },
                new Professor
                {
                    Id = 2,
                    Name = "Egor",
                    Surname = "Surname",
                    SubjectName = "English"
                },
                new Professor
                {
                    Id = 3,
                    Name = "Igor",
                    Surname = "Surname",
                    SubjectName = "Geometry"
                });
        }
    }
}
