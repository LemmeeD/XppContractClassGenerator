using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XppContractClassGenerator
{
    class ContractClassEntry : IEquatable<ContractClassEntry>
    {
        #region PROPERTIES
        public DataType Type { get; set; }
        public string JsonName { get; set; }
        public bool IsDate { get; set; }
        public DataType ElementType { get; set; }
        public ContractClass ElementContract { get; set; }
        public string ElementXppPrimitiveData {
            get
            {
                string ret = "";
                if (this.ElementType == DataType.OBJECT)
                {
                    ret = this.ElementContract.Name;
                }
                else
                {
                    ret = DataTypeHelper.ToXppPrimitiveType(this.ElementType);
                }
                return ret;
            }
        }
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
        public string GetDateMethodName { get { return string.Format(@"get{0}", this.FieldVariableUpperName); } }
        public string SetDateMethodName { get { return string.Format(@"set{0}", this.FieldVariableUpperName); } }
        public string GetCollectionMethodName { get { return string.Format(@"get{0}AtIndex", this.FieldVariableUpperName); } }
        public bool IsRootJArray { get { return string.IsNullOrEmpty(this.JsonName) && this.IsRoot && DataTypeHelper.IsCollection(this.Type); } }
        #endregion

        #region CONSTRUCTORS
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
        #endregion

        #region METHODS
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

        #region Source code field methods
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

        protected string GenerateField()
        {
            return string.Format("{0}protected {1} {2};", Static.GetApplicationOptions().Tab, this.FieldVariableType, this.FieldVariableName);
        }

        protected string GenerateHasField()
        {
            return string.Format("{0}protected {1} {2};", Static.GetApplicationOptions().Tab, DataTypeHelper.ToXppPrimitiveType(DataType.BOOLEAN), this.HasMethodName);
        }
        #endregion

        #region Csharp
        public string GenerateCsMethods()
        {
            List<string> lines = new List<string>();
            lines.Add(this.GenerateCsParmMethod());
            if (Static.GetApplicationOptions().HandleDates && this.IsDate)
            {
                lines.Add(this.GenerateGetDateMethod());
                lines.Add(this.GenerateSetDateMethod());
            }
            if (Static.GetApplicationOptions().TranscribeGetCollectionAtIndex && DataTypeHelper.IsCollection(this.Type))
            {
                lines.Add(this.GenerateGetCollectionElementMethod());
            }
            if (Static.GetApplicationOptions().HandleValuesPresence)
            {
                lines.Add(this.GenerateHasMethod());
            }
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        #region Csharp parm methods
        protected string GenerateCsParmMethod()
        {
            List<string> lines = new List<string>();
            lines.Add(this.GenerateCsParmMethodDecorator());
            lines.Add(this.GenerateCsParmMethodSignature());
            lines.Add(this.GenerateCsParmMethodBody());
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        protected string GenerateCsParmMethodDecorator()
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
                decoratorStr = string.Format(@"{0}[DataMemberAttribute('{1}')]", Static.GetApplicationOptions().Tab, this.JsonName);
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

        protected string GenerateCsParmMethodSignature()
        {
            return string.Format(@"{0}public {1} {2}({1} {3} = {4})", Static.GetApplicationOptions().Tab, this.FieldVariableType, this.ParmMethodName, this.ParameterVariableName, this.FieldVariableName);
        }

        protected string GenerateCsParmMethodBody()
        {
            List<string> lines = new List<string>();
            lines.Add(string.Format(@"{0}{1}", Static.GetApplicationOptions().Tab, "{"));
            if (Static.GetApplicationOptions().HandleValuesPresence)
            {
                lines.Add(string.Format(@"{0}{0}if (!prmIsDefault({1}))", Static.GetApplicationOptions().Tab, this.ParameterVariableName));
                lines.Add(string.Format(@"{0}{0}{1}", Static.GetApplicationOptions().Tab, "{"));
                lines.Add(string.Format(@"{0}{0}{0}{1} = true;", Static.GetApplicationOptions().Tab, this.HasMethodName));
                lines.Add(string.Format(@"{0}{0}{1}", Static.GetApplicationOptions().Tab, "}"));
            }
            lines.Add(string.Format(@"{0}{0}{1} = {2};", Static.GetApplicationOptions().Tab, this.FieldVariableName, this.ParameterVariableName));
            lines.Add(string.Format(@"{0}{0}return {1};", Static.GetApplicationOptions().Tab, this.FieldVariableName));
            lines.Add(string.Format(@"{0}{1}", Static.GetApplicationOptions().Tab, "}"));
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }
        #endregion

        #region Csharp has methods
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
            lines.Add(string.Format(@"{0}{1}", Static.GetApplicationOptions().Tab, "{"));
            lines.Add(string.Format(@"{0}{0}return {1};", Static.GetApplicationOptions().Tab, this.HasMethodName));
            lines.Add(string.Format(@"{0}{1}", Static.GetApplicationOptions().Tab, "}"));
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }
        #endregion

        #region Csharp get collection methods
        protected string GenerateGetCollectionElementMethod()
        {
            List<string> lines = new List<string>();
            lines.Add(string.Format(@"{0}public {1} {2}(int _index)", Static.GetApplicationOptions().Tab, this.ElementXppPrimitiveData, this.GetCollectionMethodName));
            lines.Add(string.Format(@"{0}{1}", Static.GetApplicationOptions().Tab, "{"));
            lines.Add(string.Format(@"{0}{0}ListIterator it = new ListIterator({1});", Static.GetApplicationOptions().Tab, this.FieldVariableName));
            lines.Add(string.Format(@"{0}{0}int i = 1;", Static.GetApplicationOptions().Tab));
            lines.Add(string.Format(@"{0}{0}{1} cursor;", Static.GetApplicationOptions().Tab, this.ElementXppPrimitiveData));
            lines.Add(string.Format(@"{0}{0}while (it.more())", Static.GetApplicationOptions().Tab));
            lines.Add(string.Format(@"{0}{0}{1}", Static.GetApplicationOptions().Tab, "{"));
            lines.Add(string.Format(@"{0}{0}{0}cursor = it.value();", Static.GetApplicationOptions().Tab));
            lines.Add(string.Format(@"{0}{0}{0}if (i == _index)", Static.GetApplicationOptions().Tab));
            lines.Add(string.Format(@"{0}{0}{0}{1}", Static.GetApplicationOptions().Tab, "{"));
            lines.Add(string.Format(@"{0}{0}{0}{0}break;", Static.GetApplicationOptions().Tab));
            lines.Add(string.Format(@"{0}{0}{0}{1}", Static.GetApplicationOptions().Tab, "}"));
            lines.Add(string.Format(@"{0}{0}{0}i++;", Static.GetApplicationOptions().Tab));
            lines.Add(string.Format(@"{0}{0}{0}it.next();", Static.GetApplicationOptions().Tab));
            lines.Add(string.Format(@"{0}{0}{1}", Static.GetApplicationOptions().Tab, "}"));
            lines.Add(string.Format(@"{0}{0}return cursor;", Static.GetApplicationOptions().Tab));
            lines.Add(string.Format(@"{0}{1}", Static.GetApplicationOptions().Tab, "}"));
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }
        #endregion

        #region Csharp date methods
        protected string GenerateGetDateMethod()
        {
            List<string> lines = new List<string>();
            lines.Add(this.GenerateGetDateMethodSignature());
            lines.Add(this.GenerateGetDateMethodBody());
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        protected string GenerateSetDateMethod()
        {
            string paramVariableName = this.ParameterVariableName;
            List<string> lines = new List<string>();
            lines.Add(this.GenerateSetDateMethodSignature(paramVariableName));
            lines.Add(this.GenerateSetDateMethodBody(paramVariableName));
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        protected string GenerateGetDateMethodSignature()
        {
            return string.Format(@"{0}public {1} {2}()", Static.GetApplicationOptions().Tab, DataTypeHelper.ToXppPrimitiveType(DataType.UTCDATETIME), this.GetDateMethodName);
        }

        protected string GenerateSetDateMethodSignature(string _value)
        {
            return string.Format(@"{0}public void {1}(utcdatetime {2})", Static.GetApplicationOptions().Tab, this.SetDateMethodName, _value);
        }

        protected string GenerateGetDateMethodBody()
        {
            List<string> lines = new List<string>();
            lines.Add(string.Format(@"{0}{1}", Static.GetApplicationOptions().Tab, "{"));
            lines.Add(string.Format(@"{0}{0}{1} ret;", Static.GetApplicationOptions().Tab, DataTypeHelper.ToXppPrimitiveType(DataType.UTCDATETIME)));
            lines.Add(string.Format(@"{0}{0}if (this.{1}())", Static.GetApplicationOptions().Tab, this.ParmMethodName));
            lines.Add(string.Format(@"{0}{0}{1}", Static.GetApplicationOptions().Tab, "{"));
            lines.Add(string.Format(@"{0}{0}{0}System.DateTime csDateTime = System.DateTime::ParseExact(this.{1}(), ""{2}"", null);", Static.GetApplicationOptions().Tab, this.ParmMethodName, Static.GetApplicationOptions().DateFormat));
            lines.Add(string.Format(@"{0}{0}{0}ret = Global::clrSystemDateTime2UtcDateTime(csDateTime);", Static.GetApplicationOptions().Tab));
            lines.Add(string.Format(@"{0}{0}{1}", Static.GetApplicationOptions().Tab, "}"));
            lines.Add(string.Format(@"{0}{0}return ret;", Static.GetApplicationOptions().Tab, this.HasMethodName));
            lines.Add(string.Format(@"{0}{1}", Static.GetApplicationOptions().Tab, "}"));
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        protected string GenerateSetDateMethodBody(string _value)
        {
            List<string> lines = new List<string>();
            lines.Add(string.Format(@"{0}{1}", Static.GetApplicationOptions().Tab, "{"));
            lines.Add(string.Format(@"{0}{0}System.DateTime dt = Global::utcDateTime2SystemDateTime({1});", Static.GetApplicationOptions().Tab, _value));
            lines.Add(string.Format(@"{0}{0}{1} = dt.ToString(""{3}"");", Static.GetApplicationOptions().Tab, this.FieldVariableName, Static.GetApplicationOptions().DateFormat));
            lines.Add(string.Format(@"{0}{1}", Static.GetApplicationOptions().Tab, "}"));
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }
        #endregion

        #endregion

        #region Xpp Methods
        public void GenerateAndAddXppMethods(XmlDocument _Root, XmlNode _MethodsNode)
        {
            this.GenerateAndAddXppParmMethod(_Root, _MethodsNode);
            this.GenerateAndAddXppHasMethod(_Root, _MethodsNode);
            this.GenerateAndAddXppDateMethod(_Root, _MethodsNode);
            this.GenerateAndAddXppGetCollectionMethod(_Root, _MethodsNode);
        }

        protected void GenerateAndAddXppMethod(XmlDocument _Root, XmlNode _MethodsNode, string _MethodName, string _MethodSource)
        {
            XmlNode methodNode = _Root.CreateElement("Method");
            _MethodsNode.AppendChild(methodNode);

            XmlNode methodNameNode = _Root.CreateElement("Name");
            methodNameNode.InnerText = _MethodName;
            methodNode.AppendChild(methodNameNode);

            XmlNode methodourceNode = _Root.CreateElement("Source");
            methodourceNode.InnerText = StringHelper.EmbedSourceCodeInCDATANode(_MethodSource);
            methodNode.AppendChild(methodourceNode);
        }

        #region Xpp parm methods
        protected void GenerateAndAddXppParmMethod(XmlDocument _Root, XmlNode _MethodsNode)
        {
            this.GenerateAndAddXppMethod(_Root, _MethodsNode, this.ParmMethodName, this.GenerateCsParmMethod());
        }
        #endregion

        #region Xpp has methods
        protected void GenerateAndAddXppHasMethod(XmlDocument _Root, XmlNode _MethodsNode)
        {
            if (Static.GetApplicationOptions().HandleValuesPresence)
            {
                this.GenerateAndAddXppMethod(_Root, _MethodsNode, this.HasMethodName, this.GenerateHasMethod());
            }
        }
        #endregion

        #region Xpp date methods
        protected void GenerateAndAddXppDateMethod(XmlDocument _Root, XmlNode _MethodsNode)
        {
            if (this.IsDate)
            {
                this.GenerateAndAddXppMethod(_Root, _MethodsNode, this.GetDateMethodName, this.GenerateGetDateMethod());
                this.GenerateAndAddXppMethod(_Root, _MethodsNode, this.SetDateMethodName, this.GenerateSetDateMethod());
            }
        }
        #endregion

        #region Xpp get collection methods
        protected void GenerateAndAddXppGetCollectionMethod(XmlDocument _Root, XmlNode _MethodsNode)
        {
            if (Static.GetApplicationOptions().TranscribeGetCollectionAtIndex && DataTypeHelper.IsCollection(this.Type))
            {
                this.GenerateAndAddXppMethod(_Root, _MethodsNode, this.GetCollectionMethodName, this.GenerateGetCollectionElementMethod());
            }
        }
        #endregion

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

        #endregion
    }
}