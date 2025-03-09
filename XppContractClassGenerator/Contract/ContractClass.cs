using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XppContractClassGenerator
{
    class ContractClass : IEquatable<ContractClass>
    {
        public string Name { get; set; }
        public int Level { get; set; }
        protected List<ContractClassEntry> Entries;

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

        public string GenerateClass()
        {
            List<string> lines = new List<string>();
            lines.Add(@"[DataContractAttribute]");
            lines.Add(string.Format(@"public class {0}", this.Name));
            lines.Add("{");
            foreach (ContractClassEntry entry in this.Entries)
            {
                lines.Add(entry.generateFields());
            }
            lines.Add(Static.GetApplicationOptions().NewLine);
            foreach (ContractClassEntry entry in this.Entries)
            {
                lines.Add(entry.generateMethods());
                lines.Add(Static.GetApplicationOptions().NewLine);
            }
            lines.Add(@"}");
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
