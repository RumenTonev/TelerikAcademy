using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepeaterDemo.Extensions
{
    public static class ObjectExtensions
    {
       
            public static IEnumerable<T> WrapInEnumerable<T>(this T t)
            {
                yield return t;
            }
        
    }
}