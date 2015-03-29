using PopularizaceCz.Import.WikiData;
using PopularizaceCz.Import.BioCtvrtky;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopularizaceCz.Import.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var bcTask = new BioCtvrtkyImport().Import();
            var wdTask = new WikiDataImporter().Import();

            Task.WaitAll(bcTask, wdTask);

            File.WriteAllText("bc.sql", bcTask.Result);
            File.WriteAllText("wd.sql", wdTask.Result);
        }
    }
}
