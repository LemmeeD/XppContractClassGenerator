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

        public bool EntryInObject { get { return (this.Type == DataType.OBJECT); } }
        public bool EntryObjectInArray { get { return (this.Type == DataType.LIST) && (this.ElementType == DataType.OBJECT); } }
        public bool EntryValueInArray { get { return (this.Type == DataType.LIST) && (this.ElementType != DataType.NONE) && (this.ElementType != DataType.OBJECT) && (this.ElementType != DataType.LIST); } }

        public string FieldName { get { return StringHelper.ToCamelCase(this.JsonName); } }
        public string FieldUpperName { get { return StringHelper.FirstLetterUpperCase(this.FieldName); } }
        public string ParameterName { get { return string.Format(@"_{0}", this.FieldName); } }
        public string ParmName { get { return string.Format(@"parm{0}", this.FieldUpperName); } }
        public string HasFieldName { get { return string.Format(@"has{0}", this.FieldUpperName); } }
        public string GetFieldName { get { return string.Format(@"get{0}", this.FieldUpperName); } }

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

        public static ContractClassEntry CreateJValue(string propName, JValue jVal)
        {
            bool isDate = false;
            if (jVal.Type == JTokenType.String)
            {
                isDate = StringHelper.StringIsFormattedAsDate((string) jVal.Value, Static.GetApplicationOptions().DateFormat);
            }
            return new ContractClassEntry(DataTypeHelper.FromJTokenType(jVal.Type), isDate, propName, DataType.NONE, null);
        }

        public static ContractClassEntry CreateJObject(string propName, ContractClass elementContractClass)
        {
            return new ContractClassEntry(DataType.OBJECT, false, propName, DataType.NONE, elementContractClass);
        }

        public static ContractClassEntry CreateJObjectEntryInArray(string propName, JObject element, ContractClass elementContractClass)
        {
            return new ContractClassEntry(DataTypeHelper.FromJTokenType(JTokenType.Array), false, propName, DataTypeHelper.FromJTokenType(element.Type), elementContractClass);
        }

        public static ContractClassEntry CreateJValueEntryInArray(string propName, JValue element)
        {
            return new ContractClassEntry(DataTypeHelper.FromJTokenType(JTokenType.Array), false, propName, DataTypeHelper.FromJTokenType(element.Type), null);
        }

        #region Fields
        public string generateFields()
        {
            List<string> lines = new List<string>();
            lines.Add(this.generateField());
            if (Static.GetApplicationOptions().HandleValuesPresence)
            {
                lines.Add(this.generateHasField());
            }
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        public string generateField()
        {
            string decoratorStr = null;
            if (this.Type == DataType.OBJECT)
            {
                decoratorStr = string.Format("{0}protected {1} {2};", Static.GetApplicationOptions().Tab, this.ElementContract.Name, this.FieldName);
            }
            else if (this.Type == DataType.LIST)
            {
                decoratorStr = string.Format("{0}protected {1} {2};", Static.GetApplicationOptions().Tab, DataTypeHelper.ToString(this.Type), this.FieldName);
            }
            else
            {
                decoratorStr = string.Format("{0}protected {1} {2};", Static.GetApplicationOptions().Tab, DataTypeHelper.ToString(this.Type), this.FieldName);
            }
            return decoratorStr;
            
        }

        public string generateHasField()
        {
            return string.Format("{0}protected {1} {2};", Static.GetApplicationOptions().Tab, DataTypeHelper.ToString(DataType.BOOLEAN), this.HasFieldName);
        }
        #endregion

        public string generateMethods()
        {
            List<string> lines = new List<string>();
            lines.Add(this.generateParmMethod());
            if (Static.GetApplicationOptions().HandleDates && this.IsDate)
            {
                lines.Add(this.generateGetDateMethod());
            }
            if (Static.GetApplicationOptions().HandleValuesPresence)
            {
                lines.Add(this.generateHasMethod());
            }
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        #region ParmMethod
        protected string generateParmMethod()
        {
            List<string> lines = new List<string>();
            lines.Add(this.generateParmMethodDecorator());
            lines.Add(this.generateParmMethodSignature());
            lines.Add(this.generateParmMethodBody());
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        protected string generateParmMethodDecorator()
        {
            string decoratorStr = null;
            if (this.Type == DataType.OBJECT)
            {
                decoratorStr = string.Format(@"{0}[DataMemberAttribute('{1}'), DataObjectAttribute(classStr({2}))]", Static.GetApplicationOptions().Tab, this.JsonName, this.ElementContract.Name);
            }
            else if (this.Type == DataType.LIST)
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
                decoratorStr = string.Format(@"{0}[DataMemberAttribute('{1}')]", Static.GetApplicationOptions().Tab, this.JsonName);
            }
            return decoratorStr;
        }

        protected string generateParmMethodSignature()
        {
            string signatureStr = null;
            if (this.Type == DataType.OBJECT)
            {
                signatureStr = string.Format(@"{0}public {1} {2}({1} {3} = {4})", Static.GetApplicationOptions().Tab, this.ElementContract.Name, this.ParmName, this.ParameterName, this.FieldName);
            }
            else if (this.Type == DataType.LIST)
            {
                signatureStr = string.Format(@"{0}public {1} {2}({1} {3} = {4})", Static.GetApplicationOptions().Tab, DataTypeHelper.ToString(this.Type), this.ParmName, this.ParameterName, this.FieldName);
            }
            else
            {
                signatureStr = string.Format(@"{0}public {1} {2}({1} {3} = {4})", Static.GetApplicationOptions().Tab, DataTypeHelper.ToString(this.Type), this.ParmName, this.ParameterName, this.FieldName);
            }
            return signatureStr;
        }

        protected string generateParmMethodBody()
        {
            List<string> lines = new List<string>();
            lines.Add(Static.GetApplicationOptions().Tab + "{");
            if (Static.GetApplicationOptions().HandleValuesPresence)
            {
                lines.Add(string.Format(@"{0}{0}if (!prmIsDefault({1}))", Static.GetApplicationOptions().Tab, this.HasFieldName));
                lines.Add(Static.GetApplicationOptions().Tab + Static.GetApplicationOptions().Tab + "{");
                lines.Add(string.Format(@"{0}{0}{0}{1} = true;", Static.GetApplicationOptions().Tab, this.HasFieldName));
                lines.Add(Static.GetApplicationOptions().Tab + Static.GetApplicationOptions().Tab + "}");
            }
            lines.Add(string.Format(@"{0}{0}{1} = {2};", Static.GetApplicationOptions().Tab, this.FieldName, this.ParameterName));
            lines.Add(string.Format(@"{0}{0}return {1};", Static.GetApplicationOptions().Tab, this.FieldName));
            lines.Add(Static.GetApplicationOptions().Tab + "}");
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }
        #endregion

        #region HasMethod
        protected string generateHasMethod()
        {
            List<string> lines = new List<string>();
            lines.Add(this.generateHasMethodSignature());
            lines.Add(this.generateHasMethodBody());
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        protected string generateHasMethodSignature()
        {
            return string.Format(@"{0}public {1} {2}()", Static.GetApplicationOptions().Tab, DataTypeHelper.ToString(DataType.BOOLEAN), this.HasFieldName);
        }

        protected string generateHasMethodBody()
        {
            List<string> lines = new List<string>();
            lines.Add(Static.GetApplicationOptions().Tab + "{");
            lines.Add(string.Format(@"{0}{0}return {1};", Static.GetApplicationOptions().Tab, this.HasFieldName));
            lines.Add(Static.GetApplicationOptions().Tab + "}");
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }
        #endregion

        #region GetDateMethod
        protected string generateGetDateMethod()
        {
            List<string> lines = new List<string>();
            lines.Add(this.generateGetDateMethodSignature());
            lines.Add(this.generateGetDateMethodBody());
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        protected string generateGetDateMethodSignature()
        {
            return string.Format(@"{0}public {1} {2}()", Static.GetApplicationOptions().Tab, DataTypeHelper.ToString(DataType.UTCDATETIME), this.GetFieldName);
        }

        protected string generateGetDateMethodBody()
        {
            List<string> lines = new List<string>();
            lines.Add(Static.GetApplicationOptions().Tab + "{");
            lines.Add(string.Format(@"{0}{0}{1} ret;", Static.GetApplicationOptions().Tab, DataTypeHelper.ToString(DataType.UTCDATETIME)));
            lines.Add(string.Format(@"{0}{0}if (this.{1}())", Static.GetApplicationOptions().Tab, this.ParmName));
            lines.Add(Static.GetApplicationOptions().Tab + Static.GetApplicationOptions().Tab + "{");
            lines.Add(string.Format(@"{0}{0}{0}System.DateTime csDateTime = System.DateTime::ParseExact(this.{1}(), ""{2}"", null);", Static.GetApplicationOptions().Tab, this.ParmName, Static.GetApplicationOptions().DateFormat));
            lines.Add(string.Format(@"{0}{0}{0}ret = Global::clrSystemDateTime2UtcDateTime(csDateTime);", Static.GetApplicationOptions().Tab));
            lines.Add(Static.GetApplicationOptions().Tab + Static.GetApplicationOptions().Tab + "}");
            lines.Add(string.Format(@"{0}{0}return ret;", Static.GetApplicationOptions().Tab, this.HasFieldName));
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