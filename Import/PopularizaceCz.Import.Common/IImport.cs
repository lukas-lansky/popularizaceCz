using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopularizaceCz.Import.Common
{
    public interface IImport
    {
        Task<string> Import();
    }
}
