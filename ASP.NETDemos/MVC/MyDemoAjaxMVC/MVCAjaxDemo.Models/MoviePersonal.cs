using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAjaxDemo.Models
{
    public abstract class MoviePersonal
    {



        
        public int Id { get; set; }



        [Display(Name = "Age")]
       
        [Required,Range(10, 80)]
        public byte Age { get; set; }

       
        [Required, MinLength(2), MaxLength(55)]
        public string Name { get; set; }


    }
}
