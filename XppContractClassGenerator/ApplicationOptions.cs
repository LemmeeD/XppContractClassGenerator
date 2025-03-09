using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XppContractClassGenerator
{
    class ApplicationOptions
    {
        public string OutputDirectoryPath { get; set; }
        public string Json { get; set; }
        public string BaseClassName { get; set; }
        public string NewLine { get; set; }
        public string Tab { get; set; }
        public bool HandleValuesPresence { get; set; }
        public bool HandleDates { get; set; }
        public string DateFormat { get; set; }

        public ApplicationOptions()
        {
            this.NewLine = Environment.NewLine;
            this.Tab = "\t"; //"     "; // "\t";
        }

        public static ApplicationOptions CreateDefault()
        {
            ApplicationOptions options = new ApplicationOptions();
            options.OutputDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "output");
            options.Json = "";
            options.BaseClassName = "JsonContract";
            options.HandleValuesPresence = false;
            options.HandleDates = false;
            options.DateFormat = "dd-MM-yyyy";
            return options;
        }
    }
}
