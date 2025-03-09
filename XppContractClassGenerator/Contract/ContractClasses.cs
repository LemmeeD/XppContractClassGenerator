using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XppContractClassGenerator
{
    class ContractClasses : IEnumerable<ContractClass>
    {
        List<ContractClass> contracts;
        public int Count { get { return this.contracts.Count; } }

        public ContractClasses()
        {
            this.contracts = new List<ContractClass>();
        }

        public void Add(ContractClass contract)
        {
            this.contracts.Add(contract);
        }

        public string GenerateClassNameForLevel(int level)
        {
            bool levelAlreadyHasClasses = false;
            if (contracts.Exists(cc => cc.Level == level))
            {
                levelAlreadyHasClasses = true;
            }

            string newClassName = Static.GetApplicationOptions().BaseClassName + "Level" + level;

            if (levelAlreadyHasClasses)
            {
                List<ContractClass> ccList = contracts.Where(cc => cc.Name == newClassName).OrderBy(cc => cc.Name).ToList();
                int i = 0;
                foreach (ContractClass cc in ccList)
                {
                    cc.Name = cc.Name + "_" + i;
                    i++;
                }

                newClassName = newClassName + "_" + i;
            }

            return newClassName;
        }

        public IEnumerator<ContractClass> GetEnumerator()
        {
            foreach (ContractClass contract in this.contracts)
            {
                yield return contract;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
