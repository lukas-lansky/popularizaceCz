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
            var bci = new BioCtvrtky.BioCtvrtkyImport();

            var queryTask = bci.Import();
            queryTask.Wait();

            File.WriteAllText("bc.sql", queryTask.Result);
            Console.ReadLine();
        }
    }
}
