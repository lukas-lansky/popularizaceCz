using PopularizaceCz.Import.BioCtvrtky;
using System.IO;
using System.Threading.Tasks;

namespace PopularizaceCz.Import.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            // var bcTask = new BioCtvrtkyImport().Import();
            var tvTask = new TydenVedy.TydenVedyImport().Import();

			Task.WaitAll(tvTask); //, wdTask);

            // File.WriteAllText("bc.sql", bcTask.Result);
            File.WriteAllText("tv.sql", tvTask.Result);
        }
    }
}
