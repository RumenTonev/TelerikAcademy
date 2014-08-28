using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMVCDbDemo.Models
{
    [Table("Actors")]
    public class LeadingActor:MoviePersonal
    {
        [Display(Name = "Studio")]
        [MinLength(2), MaxLength(55)]
        [Required]
        public string StudioName{ get; set; }

        [Display(Name = "Studio Adress")]
        [MinLength(30), MaxLength(75)]
        [Required]
        public string StudioAdress { get; set; }

    }
}
