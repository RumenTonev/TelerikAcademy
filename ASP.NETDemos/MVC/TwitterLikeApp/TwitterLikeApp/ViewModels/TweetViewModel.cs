using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TwitterLikeApp.ViewModels
{
    public class TweetViewModel
    {
        [Required]
        [Display(Name = "Content")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        //[Required]
       // [Display(Name = "CreatedOn")]
        //public string Datetime { get; set; }
    }
}