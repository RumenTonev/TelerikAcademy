using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMVCDbDemo.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
         [MinLength(2),MaxLength(55)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Year")]
        [Range(1900,2100)]
        public ushort YearOfCreation { get; set; }



       [Required]
        public virtual Director Director { get; set; }
        [Required]
        public virtual LeadingActress LeadingFemaleActress { get; set; }
        [Required]
        public virtual LeadingActor LeadingMaleActor { get; set; }
    }
}
