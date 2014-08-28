﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Isbn { get; set; }
        public string WebSite { get; set; }
    }
}