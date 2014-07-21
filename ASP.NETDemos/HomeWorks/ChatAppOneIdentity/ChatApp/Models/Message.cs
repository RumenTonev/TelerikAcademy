using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{

        public class Message
        {
            public int Id { get; set; }

            public string Contents { get; set; }

            public DateTime Timestamp { get; set; }

            public virtual ApplicationUser Author { get; set; }
        }
    }
