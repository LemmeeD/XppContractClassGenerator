using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XppContractClassGenerator
{
    class JTokenHelper
    {
        public static bool ContainedInJObject(JToken jToken)
        {
            JToken parentJtoken = jToken.Parent;
            if (parentJtoken == null)
            {
                return false;
            }
            if (parentJtoken is JObject)
            {
                return true;
            }
            return ContainedInJObject(parentJtoken);
        }
    }
}
