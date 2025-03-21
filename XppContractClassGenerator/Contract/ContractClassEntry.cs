using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XppContractClassGenerator
{
    class ContractClassEntry : IEquatable<ContractClassEntry>
    {
        public DataType Type { get; set; }
        public string JsonName { get; set; }
        public bool IsDate { get; set; }
        public DataType ElementType { get; set; }
        public ContractClass ElementContract { get; set; }
        public bool IsRoot { get; set; }
        public string FieldVariableType
        {
            get
            {
                string ret = "";
                if (this.Type == DataType.OBJECT)
                {
                    ret = this.ElementContract.Name;
                }
                else
                {
                    ret = DataTypeHelper.ToXppPrimitiveType(this.Type);
                }
                return ret;
            }
        }

        public string FieldVariableName
        {
            get
            {
                string ret = "";
                if (this.IsRootJArray)
                {
                    // caso speciale: JSON solo con un array
                    ret = "elements";   //valore di default per il campo
                }
                else
                {
                    ret = StringHelper.ToCamelCase(this.JsonName);
                }
                return ret;
            }
        }
        public string FieldVariableUpperName { get { return StringHelper.FirstLetterUpperCase(this.FieldVariableName); } }
        public string ParameterVariableName { get { return string.Format(@"_{0}", this.FieldVariableName); } }
        public string ParmMethodName { get { return string.Format(@"parm{0}", this.FieldVariableUpperName); } }
        public string HasMethodName { get { return string.Format(@"has{0}", this.FieldVariableUpperName); } }
        public string GetMethodName { get { return string.Format(@"get{0}", this.FieldVariableUpperName); } }
        public bool IsRootJArray { get { return string.IsNullOrEmpty(this.JsonName) && this.IsRoot && DataTypeHelper.IsCollection(this.Type); } }

        public ContractClassEntry()
        {

        }

        public ContractClassEntry(DataType _type, bool _isDate, string _jsonName, DataType _elementType, ContractClass _elementContract)
        {
            this.Type = _type;
            this.IsDate = _isDate;
            this.JsonName = _jsonName;
            this.ElementType = _elementType;
            this.ElementContract = _elementContract;
        }

        public static ContractClassEntry CreateJValueInJObject(string propName, JValue jVal)
        {
            bool isDate = false;
            if (jVal.Type == JTokenType.String)
            {
                isDate = StringHelper.StringIsFormattedAsDate((string)jVal.Value, Static.GetApplicationOptions().DateFormat);
            }
            return new ContractClassEntry(DataTypeHelper.FromJTokenType(jVal.Type), isDate, propName, DataType.NONE, null);
        }

        public static ContractClassEntry CreateJObjectInJObject(string propName, ContractClass elementContractClass)
        {
            return new ContractClassEntry(DataType.OBJECT, false, propName, DataType.OBJECT, elementContractClass);
        }

        public static ContractClassEntry CreateJValueInJArray(JValue element)
        {
            return new ContractClassEntry(Static.GetApplicationOptions().CollectionDataType, false, null, DataTypeHelper.FromJTokenType(element.Type), null);
        }

        public static ContractClassEntry CreateJObjectInJArray(ContractClass elementContractClass)
        {
            return new ContractClassEntry(Static.GetApplicationOptions().CollectionDataType, false, null, DataType.OBJECT, elementContractClass);
        }

        public static ContractClassEntry CreateJArrayInJObject(string propName, DataType elementType, ContractClass elementContractClass)
        {
            return new ContractClassEntry(Static.GetApplicationOptions().CollectionDataType, false, propName, elementType, elementContractClass);
        }

        #region Fields
        public string GenerateFields()
        {
            List<string> lines = new List<string>();
            lines.Add(this.GenerateField());
            if (Static.GetApplicationOptions().HandleValuesPresence)
            {
                lines.Add(this.GenerateHasField());
            }
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        public string GenerateField()
        {
            return string.Format("{0}protected {1} {2};", Static.GetApplicationOptions().Tab, this.FieldVariableType, this.FieldVariableName);
        }

        public string GenerateHasField()
        {
            return string.Format("{0}protected {1} {2};", Static.GetApplicationOptions().Tab, DataTypeHelper.ToXppPrimitiveType(DataType.BOOLEAN), this.HasMethodName);
        }
        #endregion

        public string GenerateMethods()
        {
            List<string> lines = new List<string>();
            lines.Add(this.GenerateParmMethod());
            if (Static.GetApplicationOptions().HandleDates && this.IsDate)
            {
                lines.Add(this.GenerateGetDateMethod());
            }
            if (Static.GetApplicationOptions().HandleValuesPresence)
            {
                lines.Add(this.GenerateHasMethod());
            }
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        #region ParmMethod
        protected string GenerateParmMethod()
        {
            List<string> lines = new List<string>();
            lines.Add(this.GenerateParmMethodDecorator());
            lines.Add(this.GenerateParmMethodSignature());
            lines.Add(this.GenerateParmMethodBody());
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        protected string GenerateParmMethodDecorator()
        {
            string decoratorStr = null;
            if (this.IsRoot && DataTypeHelper.IsCollection(this.Type))
            {
                if (this.ElementType == DataType.OBJECT)
                {
                    decoratorStr = string.Format(@"{0}[DataCollectionAttribute({1}, classStr({2}))]", Static.GetApplicationOptions().Tab, DataTypeHelper.ToXppTypesEnum(this.ElementType), this.ElementContract.Name);
                }
                else
                {
                    decoratorStr = string.Format(@"{0}[DataCollectionAttribute({1})]", Static.GetApplicationOptions().Tab, DataTypeHelper.ToXppTypesEnum(this.ElementType));
                }
            }
            else if (this.Type == DataType.OBJECT)
            {
                decoratorStr = string.Format(@"{0}[DataMemberAttribute('{1}'), DataObjectAttribute(classStr({2}))]", Static.GetApplicationOptions().Tab, this.JsonName, this.ElementContract.Name);
            }
            else if (DataTypeHelper.IsCollection(this.Type))
            {
                if (DataTypeHelper.IsTypedCollection(this.Type))
                {
                    if (this.ElementType == DataType.OBJECT)
                    {
                        decoratorStr = string.Format(@"{0}[DataMemberAttribute('{1}'), DataCollectionAttribute({2}, classStr({3}))]", Static.GetApplicationOptions().Tab, this.JsonName, DataTypeHelper.ToXppTypesEnum(this.ElementType), this.ElementContract.Name);
                    }
                    else
                    {
                        decoratorStr = string.Format(@"{0}[DataMemberAttribute('{1}'), DataCollectionAttribute({2})]", Static.GetApplicationOptions().Tab, this.JsonName, DataTypeHelper.ToXppTypesEnum(this.ElementType));
                    }
                }
                else
                {
                    decoratorStr = string.Format(@"{0}[DataMemberAttribute('{1}'), DataCollectionAttribute({2})]", Static.GetApplicationOptions().Tab, this.JsonName, DataTypeHelper.ToXppTypesEnum(this.Type));
                }
            }
            else
            {
                decoratorStr = string.Format(@"{0}[DataMemberAttribute('{1}')]", Static.GetApplicationOptions().Tab, this.JsonName);
            }
            return decoratorStr;
        }

        protected string GenerateParmMethodSignature()
        {
            return string.Format(@"{0}public {1} {2}({1} {3} = {4})", Static.GetApplicationOptions().Tab, this.FieldVariableType, this.ParmMethodName, this.ParameterVariableName, this.FieldVariableName); ;
        }

        protected string GenerateParmMethodBody()
        {
            List<string> lines = new List<string>();
            lines.Add(Static.GetApplicationOptions().Tab + "{");
            if (Static.GetApplicationOptions().HandleValuesPresence)
            {
                lines.Add(string.Format(@"{0}{0}if (!prmIsDefault({1}))", Static.GetApplicationOptions().Tab, this.HasMethodName));
                lines.Add(Static.GetApplicationOptions().Tab + Static.GetApplicationOptions().Tab + "{");
                lines.Add(string.Format(@"{0}{0}{0}{1} = true;", Static.GetApplicationOptions().Tab, this.HasMethodName));
                lines.Add(Static.GetApplicationOptions().Tab + Static.GetApplicationOptions().Tab + "}");
            }
            lines.Add(string.Format(@"{0}{0}{1} = {2};", Static.GetApplicationOptions().Tab, this.FieldVariableName, this.ParameterVariableName));
            lines.Add(string.Format(@"{0}{0}return {1};", Static.GetApplicationOptions().Tab, this.FieldVariableName));
            lines.Add(Static.GetApplicationOptions().Tab + "}");
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }
        #endregion

        #region HasMethod
        protected string GenerateHasMethod()
        {
            List<string> lines = new List<string>();
            lines.Add(this.GenerateHasMethodSignature());
            lines.Add(this.GenerateHasMethodBody());
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        protected string GenerateHasMethodSignature()
        {
            return string.Format(@"{0}public {1} {2}()", Static.GetApplicationOptions().Tab, DataTypeHelper.ToXppPrimitiveType(DataType.BOOLEAN), this.HasMethodName);
        }

        protected string GenerateHasMethodBody()
        {
            List<string> lines = new List<string>();
            lines.Add(Static.GetApplicationOptions().Tab + "{");
            lines.Add(string.Format(@"{0}{0}return {1};", Static.GetApplicationOptions().Tab, this.HasMethodName));
            lines.Add(Static.GetApplicationOptions().Tab + "}");
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }
        #endregion

        #region GetDateMethod
        protected string GenerateGetDateMethod()
        {
            List<string> lines = new List<string>();
            lines.Add(this.generateGetDateMethodSignature());
            lines.Add(this.GenerateGetDateMethodBody());
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        protected string generateGetDateMethodSignature()
        {
            return string.Format(@"{0}public {1} {2}()", Static.GetApplicationOptions().Tab, DataTypeHelper.ToXppPrimitiveType(DataType.UTCDATETIME), this.GetMethodName);
        }

        protected string GenerateGetDateMethodBody()
        {
            List<string> lines = new List<string>();
            lines.Add(Static.GetApplicationOptions().Tab + "{");
            lines.Add(string.Format(@"{0}{0}{1} ret;", Static.GetApplicationOptions().Tab, DataTypeHelper.ToXppPrimitiveType(DataType.UTCDATETIME)));
            lines.Add(string.Format(@"{0}{0}if (this.{1}())", Static.GetApplicationOptions().Tab, this.ParmMethodName));
            lines.Add(Static.GetApplicationOptions().Tab + Static.GetApplicationOptions().Tab + "{");
            lines.Add(string.Format(@"{0}{0}{0}System.DateTime csDateTime = System.DateTime::ParseExact(this.{1}(), ""{2}"", null);", Static.GetApplicationOptions().Tab, this.ParmMethodName, Static.GetApplicationOptions().DateFormat));
            lines.Add(string.Format(@"{0}{0}{0}ret = Global::clrSystemDateTime2UtcDateTime(csDateTime);", Static.GetApplicationOptions().Tab));
            lines.Add(Static.GetApplicationOptions().Tab + Static.GetApplicationOptions().Tab + "}");
            lines.Add(string.Format(@"{0}{0}return ret;", Static.GetApplicationOptions().Tab, this.HasMethodName));
            lines.Add(Static.GetApplicationOptions().Tab + "}");
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }
        #endregion

        #region Object
        public override bool Equals(object obj)
        {
            return Equals(obj as ContractClassEntry);
        }

        public bool Equals(ContractClassEntry other)
        {
            return other != null &&
                   Type == other.Type &&
                   JsonName == other.JsonName &&
                   ElementContract == other.ElementContract;
        }

        public override int GetHashCode()
        {
            int hashCode = -902181005;
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(JsonName);
            hashCode = hashCode * -1521134295 + EqualityComparer<ContractClass>.Default.GetHashCode(ElementContract);
            return hashCode;
        }
        #endregion
    }
}