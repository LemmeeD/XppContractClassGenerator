using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XppContractClassGenerator
{
    class TraversingParseParameter
    {
        public ContractClass Current;
        public string CurrentPropertyName;
        public bool InArray;
        public ContractClass Parent;
        public ContractClass GrandParent;

        public TraversingParseParameter()
        {

        }

        public TraversingParseParameter(ContractClass rootContractClass)
        {
            this.Current = rootContractClass;
        }

        public void NewCurrent(ContractClass contractClass)
        {
            if (this.Parent != null)
            {
                this.GrandParent = this.Parent;
            }
            if (this.Current != null)
            {
                this.Parent = this.Current;
            }
            this.Current = contractClass;
        }

        public void ResetPrevious()
        {
            this.Current = null;
            if (this.Parent != null)
            {
                this.Current = this.Parent;
            }
            if (this.GrandParent != null)
            {
                this.Parent = this.GrandParent;
            }
        }
    }
}
