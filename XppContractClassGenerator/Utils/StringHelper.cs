using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XppContractClassGenerator
{
    class StringHelper
    {
        public static string ToCamelCase(string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str.ToLowerInvariant();
        }

        public static string FirstLetterUpperCase(string str)
        {
            string firstLetterUpperCase = str[0].ToString().ToUpper();
            string restOfString = str.Substring(1);
            return firstLetterUpperCase + restOfString;
        }

        public static bool StringIsFormattedAsDate(string str, string datePattern)
        {
            bool ret = false;
            try
            {
                DateTime.ParseExact(str, datePattern, System.Globalization.CultureInfo.InvariantCulture);
                ret = true;
            }
            catch (Exception)
            {

            }
            return ret;
        }

        public static string EmbedSourceCodeInCDATANode(string _SourceCode)
        {
            return "<![CDATA[" + Static.GetApplicationOptions().NewLine + _SourceCode + "]]>";
        }
    }
}
