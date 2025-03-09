using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                ContractClass ccFirst = contracts.Where(cc => cc.Level == level).OrderBy(cc => cc.Name).First();
                Match m1 = Regex.Match(ccFirst.Name, @"^.+_(?<num>\d+)$");
                if (m1.Success)
                {
                    int maxNum = this.FindMaximumNumberOfSameLevelClasses(level);
                    newClassName = newClassName + "_" + (maxNum + 1);
                }
                else
                {
                    int nextNumber = this.UpdateSameLevelClassesName(level);
                    newClassName = newClassName + "_" + nextNumber;
                }
            }
            return newClassName;
        }

        protected int UpdateSameLevelClassesName(int level)
        {
            List<ContractClass> ccList = contracts.Where(cc => cc.Level == level).OrderBy(cc => cc.Name).ToList();
            int i = 0;
            foreach (ContractClass cc in ccList)
            {
                cc.Name = cc.Name + "_" + i;
                i++;
            }
            return i;
        }

        protected int FindMaximumNumberOfSameLevelClasses(int level)
        {
            int maxNum = 0;
            List<ContractClass> ccList = contracts.Where(cc => cc.Level == level).OrderBy(cc => cc.Name).ToList();
            foreach (ContractClass cc in ccList)
            {
                Match m = Regex.Match(cc.Name, @"^.+_(?<num>\d+)$");
                if (m.Success)
                {
                    Group g = m.Groups["num"];
                    int lastNum = Convert.ToInt32(g.Value);
                    if (lastNum > maxNum)
                    {
                        maxNum = lastNum;
                    }
                }
            }
            return maxNum;
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
