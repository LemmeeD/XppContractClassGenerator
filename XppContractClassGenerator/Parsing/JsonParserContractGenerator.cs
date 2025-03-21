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
            //this.RecursiveTraverse(result, null, null, jToken, 0, null, false, false);
            this.RecursiveMainTraverse(result, jToken);
            return result;
        }

        public void RecursiveMainTraverse(ContractClasses result, JToken jToken)
        {
            int rootLevel = 0;
            if (jToken is JObject)
            {
                this.RecursiveTraverseJObject(result, jToken as JObject, rootLevel);
            }
            else if (jToken is JArray)
            {
                ContractClassEntry entry = this.RecursiveTraverseJArray(result, jToken as JArray, rootLevel);
                ContractClass jObjContractClass = new ContractClass(result.GenerateClassNameForLevel(rootLevel), rootLevel);
                jObjContractClass.AddEntry(entry);
                result.Add(jObjContractClass);
            }
        }

        protected ContractClass RecursiveTraverseJObject(ContractClasses result, JObject jObj, int level)
        {
            ContractClass jObjContractClass = new ContractClass(result.GenerateClassNameForLevel(level), level);
            JEnumerable<JProperty> children = jObj.Children<JProperty>();
            foreach (JProperty childJProp in children)
            {
                ContractClassEntry entry = this.RecursiveTraverseJProperties(result, childJProp, level, jObjContractClass);
                jObjContractClass.AddEntry(entry);
            }
            result.Add(jObjContractClass);
            return jObjContractClass;
        }

        protected ContractClassEntry RecursiveTraverseJArray(ContractClasses result, JArray jArray, int level)
        {
            ContractClassEntry entry = null;
            JToken firstJVal = jArray.First;
            if (firstJVal != null)
            {
                entry = this.RecursiveTraverseElementsInJArray(result, firstJVal, level);
                if (level == 0)
                {
                    entry.IsRoot = true;
                }
            }
            return entry;
        }

        protected ContractClassEntry RecursiveTraverseJProperties(ContractClasses result, JProperty jProp, int level, ContractClass jObjContractClass)
        {
            ContractClassEntry entry = null;
            JToken jToken = jProp.Value;
            if (jToken is JValue)
            {
                entry = ContractClassEntry.CreateJValueInJObject(jProp.Name, jToken as JValue);
            }
            else if (jToken is JObject)
            {
                ContractClass childJObjContractClass = this.RecursiveTraverseJObject(result, jToken as JObject, level + 1);
                entry = ContractClassEntry.CreateJObjectInJObject(jProp.Name, childJObjContractClass);
            }
            else if (jToken is JArray)
            {
                ContractClassEntry childEntry = this.RecursiveTraverseJArray(result, jToken as JArray, level);
                entry = ContractClassEntry.CreateJArrayInJObject(jProp.Name, childEntry.ElementType, childEntry.ElementContract);
                //entry = childEntry;
            }
            return entry;
        }

        protected ContractClassEntry RecursiveTraverseElementsInJArray(ContractClasses result, JToken jToken, int level)
        {
            ContractClassEntry entry = null;
            if (jToken is JValue)
            {
                entry = ContractClassEntry.CreateJValueInJArray(jToken as JValue);
            }
            else if (jToken is JObject)
            {
                ContractClass elementJObjContractClass = this.RecursiveTraverseJObject(result, jToken as JObject, level+1);
                entry = ContractClassEntry.CreateJObjectInJArray(elementJObjContractClass);
            }
            return entry;
        }
    }
}
