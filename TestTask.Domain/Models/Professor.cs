using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Domain.Models
{
    public class Professor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string SubjectName { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
