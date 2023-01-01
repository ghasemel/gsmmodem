using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMModem
{
    public static class StringExt
    {
        public static string Reverse(this string value)
        {
            char[] charArray = value.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string ToStringExt(this object value)
        {
            if (value == null)
                return "";
            return value.ToString();
        }

        public static string SwapChar2By2(this string str, char default_char = 'F')
        {
            if (str.Length % 2 != 0)
                str = str + default_char;

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < str.Length; i += 2)
            {
                result.Append(str[i + 1] + "" + str[i]);
            }
            return result.ToString();
        }

        public static string Remove(this string value, string subStringForRemove)
        {
            return value.Replace(subStringForRemove, "");
        }

        public static string ConvertToArabic(this string value)
        {
            return value.Replace("ی", "ي").Replace("ک", "ك");
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
