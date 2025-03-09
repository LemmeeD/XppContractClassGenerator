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

        public ContractClasses ParseAndGenerate(string json)
        {
            JToken jToken = JToken.Parse(json);
            ContractClasses result = new ContractClasses();
            this.RecursiveTraverse(result, null, null, jToken, 0, null, false, false);
            return result;
        }

        protected ContractClassEntry RecursiveTraverse(ContractClasses result, ContractClass parentContract, ContractClass currContract, JToken jToken, int level, string currentPropertyName, bool inObject, bool inArray)
        {
            bool baseCase = (result.Count == 0) && (level == 0);

            ContractClassEntry entry = null;
            if (jToken is JProperty)
            {
                JProperty jProp = jToken as JProperty;
                entry = this.RecursiveTraverse(result, parentContract, currContract, jProp.Value, level, jProp.Name, false, false);
                if (entry != null)
                {
                    currContract.AddEntry(entry);
                }
            }
            else if (jToken is JValue)
            {
                JValue jVal = jToken as JValue;
                if (inObject && inArray)
                {
                    entry = ContractClassEntry.CreateJValue(currentPropertyName, jVal);
                }
                else if (!inObject && inArray)
                {
                    entry = ContractClassEntry.CreateJValueEntryInArray(currentPropertyName, jVal);
                }
                else
                {
                    entry = ContractClassEntry.CreateJValue(currentPropertyName, jVal);
                }
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
                        entry = ContractClassEntry.CreateJObjectEntryInArray(currentPropertyName, jObj, newContract);
                    }
                    else
                    {
                        entry = ContractClassEntry.CreateJObject(currentPropertyName, newContract);
                    }
                }
                JEnumerable<JToken> children = jObj.Children();
                foreach (JToken childJToken in children)
                {
                    this.RecursiveTraverse(result, currContract, newContract, childJToken, level + 1, currentPropertyName, true, inArray);
                }
            }
            else if (jToken is JArray)
            {
                JArray jArr = jToken as JArray;
                entry = this.RecursiveTraverse(result, parentContract, currContract, jArr.First, level, currentPropertyName, inObject, true);
            }
            return entry;
        }
    }
}
