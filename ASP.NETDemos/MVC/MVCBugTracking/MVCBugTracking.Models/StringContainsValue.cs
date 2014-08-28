using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCBugTracking.Models
{
    //taka atributa moge da se izpolzva samo vyrhu propyrtita
    [AttributeUsage(AttributeTargets.Property)]
    public class StringContainsAttribute : ValidationAttribute
    {
        private string text;

        public StringContainsAttribute(string text)
        {
            this.text = text;
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            if (valueAsString == null)
            {
                return true;
            }

            return valueAsString.Contains(text);
        }
    }
}
