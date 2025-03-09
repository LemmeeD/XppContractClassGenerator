using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XppContractClassGenerator
{
    class JsonParserContractGenerator
    {

        public JsonParserContractGenerator()
        {
            
        }

        public ContractClasses Parse(string json)
        {
            JToken jToken = JToken.Parse(json);
            TraversingParseParameter param = new TraversingParseParameter();
            ContractClasses result = new ContractClasses();
            this.RecursiveTraverse(result, null, null, jToken, 0, null, false);
            return result;
        }

        protected ContractClassEntry RecursiveTraverse(ContractClasses result, ContractClass parentContract, ContractClass currContract, JToken jToken, int level, string currentPropertyName, bool inArray)
        {
            bool baseCase = (result.Count == 0) && (level == 0);

            ContractClassEntry entry = null;
            if (jToken is JProperty)
            {
                JProperty jProp = jToken as JProperty;
                entry = this.RecursiveTraverse(result, parentContract, currContract, jProp.Value, level, jProp.Name, inArray);
                if (entry != null)
                {
                    currContract.AddEntry(entry);
                }
            }
            else if (jToken is JValue)
            {
                JValue jVal = jToken as JValue;
                entry = ContractClassEntry.CreateFromJValue(currentPropertyName, jVal);
            }
            else if (jToken is JObject)
            {
                JObject jObj = jToken as JObject;
                ContractClass newContract = new ContractClass(result.GenerateClassNameForLevel(level), level);
                result.Add(newContract);
                if (!baseCase)
                {
                    if (inArray)
                    {
                        entry = ContractClassEntry.CreateFromJArray(currentPropertyName, newContract);
                    }
                    else
                    {
                        entry = ContractClassEntry.CreateFromJObject(currentPropertyName, jObj, newContract);
                    }
                }
                JEnumerable<JToken> children = jObj.Children();
                foreach (JToken childJToken in children)
                {
                    this.RecursiveTraverse(result, currContract, newContract, childJToken, level + 1, currentPropertyName, inArray);
                }
            }
            else if (jToken is JArray)
            {
                JArray jArr = jToken as JArray;
                entry = this.RecursiveTraverse(result, parentContract, currContract, jArr.First, level, currentPropertyName, true);
            }
            return entry;
        }
    }
}
