using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMVCDbDemo.Models
{
    public abstract class MoviePersonal
    {
       

      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        

        [Display(Name = "Age")]
        [Range(10, 80)]
        [Required]
        public byte Age { get; set; }

        [MinLength(2), MaxLength(55)]
        [Required]
        public string Name { get; set; }


    }
}
