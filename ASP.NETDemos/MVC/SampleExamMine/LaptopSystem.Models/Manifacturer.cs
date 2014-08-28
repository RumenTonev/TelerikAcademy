using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopSystem.Models
{
    public class Manufacturer
    {
         private ICollection<Laptop> laptops;

        public Manufacturer()
        {
            this.laptops = new HashSet<Laptop>();
        }

        [Key]
        public int Id { get; set; }
        //uniqueName
        [Required]
        [MaxLength(30)]
        [Index(IsUnique = true)] 
        public string Name { get; set; }

        public virtual ICollection<Laptop> Laptops
        {
            get { return this.laptops; }
            set { this.laptops = value; }
        }
    }
}
