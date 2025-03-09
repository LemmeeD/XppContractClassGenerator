using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XppContractClassGenerator
{
    class CollectionHelper
    {
        public static string Stringify<T>(ICollection<T> coll, string sep)
        {
            //return string.Join(sep, coll.ToArray());
            int i = 0;
            string ret = "";
            foreach (T elem in coll)
            {
                ret += elem.ToString();
                if (i+1 != coll.Count)
                {
                    ret += sep;
                }

                i++;
            }
            return ret;
        }
    }
}
