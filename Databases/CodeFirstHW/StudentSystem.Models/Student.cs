using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSystem.Models
{
    [Table("Students")]
    public class Student
    {
        private ICollection<Course> courses;

        public ICollection<Course> Courses
        {
            get { return this.courses; }
            set { this.courses = value; }
        }

        public Student()
        {
            this.courses = new HashSet<Course>();
        }

        [Key, Column("StudentId")]
        public int Id { get; set; }

        [Column("StudentName")]
        public string Name { get; set; }

        [Column("FacultyNumber")]
        public string FacultyNumber { get; set; }
    }
}
