using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Weblog.Infrastructure.Extension
{
    public static class StringExtension
    {
        /// Removes all accents from the input string.  
        public static string RemoveAccents(this string text)  
        {  
            if (string.IsNullOrWhiteSpace(text))  
                return text;  
  
            text = text.Normalize(NormalizationForm.FormD);  
            char[] chars = text  
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)   
                != UnicodeCategory.NonSpacingMark).ToArray();  
  
            return new string(chars).Normalize(NormalizationForm.FormC);  
        }    


        public static string Slugify(this string phrase)
        {
            string output = phrase.RemoveAccents().ToLower();

            // Keep Persian letters (آ-ی), English letters, numbers, spaces, and dashes
            output = Regex.Replace(output, @"[^a-z0-9آ-ی\s-]", "");

            // Normalize multiple spaces
            output = Regex.Replace(output, @"\s+", " ").Trim();

            // Replace spaces with hyphens
            output = Regex.Replace(output, @"\s", "-");

            // Collapse multiple hyphens into one
            output = Regex.Replace(output, @"-+", "-");

            // Trim hyphens from start and end
            output = output.Trim('-');

            return output;
        }  
    }
}