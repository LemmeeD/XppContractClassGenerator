using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Windows.Forms.LinkLabel;

namespace XppContractClassGenerator
{
    class ContractClass : IEquatable<ContractClass>
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public List<ContractClassEntry> Entries { get; set; }
        public bool HasInstantiableFields
        {
            get
            {
                return (Entries.Where(e => { return DataTypeHelper.IsInstantiable(e.Type); }).Count() > 0);
            }
        }

        public ContractClass(string name, int level)
        {
            this.Name = name;
            this.Level = level;
            this.Entries = new List<ContractClassEntry>();
        }

        public void AddEntry(ContractClassEntry entry)
        {
            this.Entries.Add(entry);
        }

        public string GenerateXppClass()
        {
            MemoryStream ms = new MemoryStream();
            XmlDocument root = new XmlDocument();
            XmlDeclaration dec = root.CreateXmlDeclaration("1.0", "utf-8", null);
            root.AppendChild(dec);

            XmlNode classNode = root.CreateElement("AxClass");
            root.AppendChild(classNode);

            XmlNode nameNode = root.CreateElement("Name");
            nameNode.InnerText = this.Name;
            classNode.AppendChild(nameNode);

            XmlNode sourceCodeNode = root.CreateElement("SourceCode");
            //sourceCodeNode.InnerText = this.Name;
            classNode.AppendChild(sourceCodeNode);

            XmlNode declarationNode = this.GenerateXppDeclaration(root);
            sourceCodeNode.AppendChild(declarationNode);

            XmlNode methodsNode = this.GenerateXppMethods(root);
            sourceCodeNode.AppendChild(methodsNode);

            root.Save(ms);
            byte[] fileContent = ms.ToArray();
            string ret = Encoding.UTF8.GetString(fileContent);
            ret = ret.Replace("<AxClass>", "<AxClass xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">");
            ret = ret.Replace("&lt;", "<");
            ret = ret.Replace("&gt;", ">");
            return ret;
        }

        protected XmlNode GenerateXppDeclaration(XmlDocument _Root)
        {
            XmlNode sourceCodeDeclarationNode = _Root.CreateElement("Declaration");
            sourceCodeDeclarationNode.InnerText = StringHelper.EmbedSourceCodeInCDATANode(this.GenerateXppClassDeclaration());
            return sourceCodeDeclarationNode;
        }

        protected XmlNode GenerateXppMethods(XmlDocument _Root)
        {
            XmlNode methodsNode = _Root.CreateElement("Methods");
            foreach (ContractClassEntry entry in this.Entries)
            {
                entry.GenerateAndAddXppMethods(_Root, methodsNode);
            }
            return methodsNode;
        }

        protected void GenerateAndAddXppMethod(XmlDocument _Root, XmlNode _MethodsNode, string _MethodName, string _MethodSource)
        {
            XmlNode methodNameNode = _Root.CreateElement("Name");
            methodNameNode.InnerText = _MethodName;
            _MethodsNode.AppendChild(methodNameNode);

            XmlNode methodourceNode = _Root.CreateElement("Source");
            methodourceNode.InnerText = StringHelper.EmbedSourceCodeInCDATANode(_MethodSource);
            _MethodsNode.AppendChild(methodourceNode);
        }

        public string GenerateClass()
        {
            List<string> lines = new List<string>();
            lines.Add(this.GenerateClassSignature());
            lines.Add("{");
            foreach (ContractClassEntry entry in this.Entries)
            {
                lines.Add(entry.GenerateFields());
            }
            lines.Add(Static.GetApplicationOptions().NewLine);
            foreach (ContractClassEntry entry in this.Entries)
            {
                lines.Add(entry.GenerateCsMethods());
                lines.Add(Static.GetApplicationOptions().NewLine);
            }
            //lines.Add(this.GenerateConstructor());
            lines.Add(this.GenerateConstructMethod());
            lines.Add(@"}");
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        protected string GenerateClassSignature()
        {
            List<string> lines = new List<string>();
            lines.Add(@"[DataContractAttribute]");
            lines.Add(string.Format(@"public class {0}", this.Name));
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        protected string GenerateXppClassDeclaration()
        {
            List<string> lines = new List<string>();
            lines.Add(this.GenerateClassSignature());
            lines.Add("{");
            foreach (ContractClassEntry entry in this.Entries)
            {
                lines.Add(entry.GenerateFields());
            }
            lines.Add("}");
            lines.Add(Static.GetApplicationOptions().NewLine);
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        public string GenerateConstructor()
        {
            List<string> lines = new List<string>();
            lines.Add(string.Format(@"{0}public void new()", Static.GetApplicationOptions().Tab));
            lines.Add(Static.GetApplicationOptions().Tab + @"{");
            foreach (ContractClassEntry entry in this.Entries)
            {
                if (DataTypeHelper.IsInstantiable(entry.Type))
                {
                    if (DataTypeHelper.IsCollection(entry.Type))
                    {
                        lines.Add(string.Format("{0}{0}{1} = new {2}({3});", Static.GetApplicationOptions().Tab, entry.FieldVariableName, entry.FieldVariableType, DataTypeHelper.ToXppTypesEnum(entry.ElementType)));
                    }
                    else
                    {
                        lines.Add(string.Format("{0}{0}{1} = new {2}();", Static.GetApplicationOptions().Tab, entry.FieldVariableName, entry.FieldVariableType));
                    }
                }
            }
            lines.Add(Static.GetApplicationOptions().Tab + @"}");
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        public string GenerateConstructMethod()
        {
            List<string> lines = new List<string>();
            if (HasInstantiableFields)
            {
                lines.Add(string.Format(@"{0}public void initFields()", Static.GetApplicationOptions().Tab));
                lines.Add(Static.GetApplicationOptions().Tab + @"{");
                foreach (ContractClassEntry entry in this.Entries)
                {
                    if (DataTypeHelper.IsInstantiable(entry.Type))
                    {
                        if (DataTypeHelper.IsCollection(entry.Type))
                        {
                            lines.Add(string.Format("{0}{0}{1} = new {2}({3});", Static.GetApplicationOptions().Tab, entry.FieldVariableName, entry.FieldVariableType, DataTypeHelper.ToXppTypesEnum(entry.ElementType)));
                        }
                        else
                        {
                            lines.Add(string.Format("{0}{0}{1} = {2}::construct();", Static.GetApplicationOptions().Tab, entry.FieldVariableName, entry.FieldVariableType));
                        }
                    }
                }
                lines.Add(Static.GetApplicationOptions().Tab + @"}");
            }
            lines.Add(string.Format(@"{0}public static {1} construct()", Static.GetApplicationOptions().Tab, this.Name));
            lines.Add(Static.GetApplicationOptions().Tab + @"{");
            lines.Add(string.Format("{0}{0}{1} newObj = new {1}();", Static.GetApplicationOptions().Tab, this.Name));
            if (HasInstantiableFields)
            {
                lines.Add(string.Format("{0}{0}newObj.initFields();", Static.GetApplicationOptions().Tab, this.Name));
            }
            lines.Add(string.Format("{0}{0}return newObj;", Static.GetApplicationOptions().Tab, this.Name));
            lines.Add(Static.GetApplicationOptions().Tab + @"}");
            return CollectionHelper.Stringify<string>(lines, Static.GetApplicationOptions().NewLine);
        }

        #region Object
        public override bool Equals(object obj)
        {
            return Equals(obj as ContractClass);
        }

        public bool Equals(ContractClass other)
        {
            return other != null &&
                   Name == other.Name &&
                   EqualityComparer<List<ContractClassEntry>>.Default.Equals(Entries, other.Entries);
        }

        public override int GetHashCode()
        {
            int hashCode = -53605239;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<ContractClassEntry>>.Default.GetHashCode(Entries);
            return hashCode;
        }
        #endregion
    }
}
