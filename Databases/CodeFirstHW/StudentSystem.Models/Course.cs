using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSystem.Models
{
    [Table("Courses")]
    public class Course
    {
        private ICollection<Student> students;

        public ICollection<Student> Students
        {
            get { return this.students; } 
            set { this.students = value; } 
        }

        public Course()
        {
            this.students = new HashSet<Student>();
        }

        [Key, Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Materials")]
        public string Materials { get; set; }
    }
}
