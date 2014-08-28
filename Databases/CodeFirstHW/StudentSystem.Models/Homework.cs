using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSystem.Models
{
    [Table("Homeworks")]
    public class Homework
    {
        [Key, Column("Id")]
        public int Id { get; set; }

        [Column("Content")]
        public string Content { get; set; }

        [Column("TimeSent")]
        public DateTime TimeSent { get; set; }

        [ForeignKey("Student"), Column("StudentId")]//may not be used
        public int StudentId { get; set; }//this will create FK
        public virtual Student Student { get; set; }

        [ForeignKey("Course"), Column("CourseId")]
        public int CourseId { get; set; }//this will create FK
        public virtual Course Course { get; set; }
    }
}
