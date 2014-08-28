using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterLikeApp.Models
{
    public class Tag
    {
        public Tag()
  {
    Messages = new HashSet<Message>();
  }

        public int Id { get; set; }
        public string Name { get; set; }
        //virtual for lazy loading
        public virtual ICollection<Message> Messages { get; set; }       
    }
}
