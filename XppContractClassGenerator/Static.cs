using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XppContractClassGenerator
{
    class Static
    {
        protected static ApplicationOptions OPTIONS;

        public static ApplicationOptions GetApplicationOptions()
        {
            if (OPTIONS == null)
            {
                OPTIONS = ApplicationOptions.CreateDefault();
            }
            return OPTIONS;
        }

        public static void SetApplicationOptions(ApplicationOptions options)
        {
            OPTIONS = options;
        }
    }
}
