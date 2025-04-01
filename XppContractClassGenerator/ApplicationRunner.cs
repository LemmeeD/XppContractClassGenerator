using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XppContractClassGenerator
{
    class ApplicationRunner
    {
        public static void RunApplication(ApplicationOptions options)
        {
            Static.SetApplicationOptions(options);
            PreProcessDirectory(options.OutputDirectoryPath);

            JsonParserContractGenerator jpcg = new JsonParserContractGenerator();
            ContractClasses contracts = jpcg.ParseAndGenerate(options.Json);

            foreach (ContractClass contract in contracts)
            {
                string filepath = "";
                string filecontent = "";
                if (Static.GetApplicationOptions().Language == ProgrammingLanguage.SourceCode)
                {
                    filepath = Path.Combine(options.OutputDirectoryPath, contract.Name + ".cs");
                    filecontent = contract.GenerateClass();
                }
                else if (Static.GetApplicationOptions().Language == ProgrammingLanguage.XmlSourceCode)
                {
                    filepath = Path.Combine(options.OutputDirectoryPath, contract.Name + ".xml");
                    filecontent = contract.GenerateXppClass();
                }
                StreamWriter sw = File.CreateText(filepath);
                sw.Write(filecontent);
                sw.Dispose();
            }

            OpenOutputDirectory(options.OutputDirectoryPath);
        }

        protected static void PreProcessDirectory(string outputDir)
        {
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            IEnumerable<string> filepaths = Directory.EnumerateFiles(outputDir);
            foreach (string filepath in filepaths)
            {
                File.Delete(filepath);
            }
        }

        protected static void OpenOutputDirectory(string outputDir)
        {
            Process.Start("explorer.exe", outputDir);
        }
    }
}
