using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterLikeApp.Models
{
    public class Message
    {
        public Message()
  {
    Tags = new HashSet<Tag>();
  }
        public int Id { get; set; }
      [Required]
      [DataType(DataType.MultilineText)]
        public string Content { get; set; }

      [Display(Name = "Created on")]
      public DateTime CreatedOn { get; set; }

        
        
        public virtual ICollection<Tag> Tags { get; set; }
         [Required]
        public virtual ApplicationUser Author { get; set; }
    }
}
