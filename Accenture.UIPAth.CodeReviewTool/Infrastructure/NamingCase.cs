using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;

namespace Accenture.UIPAth.CodeReviewTool.Infrastructure
{
  public static class NamingCase
    {
        // Convert the string to Pascal case.
        public static string ToPascalCase(this string the_string)
        {
            TextInfo info = Thread.CurrentThread.CurrentCulture.TextInfo;

            the_string = info.ToTitleCase(the_string);
            string[] parts = the_string.Split(new char[] { },
                StringSplitOptions.RemoveEmptyEntries);
            string result = String.Join(String.Empty, parts);
            return result;
        }
        // Convert the string to camel case.
        public static string ToCamelCase(this string the_string)
        {
            
            the_string = the_string.ToPascalCase();
            return the_string.Substring(0, 1).ToLower() +
                the_string.Substring(1);
           


        }
        //Convert to standard name ^[A-Za-z\d_-]+$
        public static bool ToStandardCase(this string the_string)
        {
            bool result=false;
            string pattern = @"^[a-zA-Z_ ]*$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(the_string) == true)
            {
                result = true;
            }
                        
            return result;

        }
    }
}
