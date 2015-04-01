using System;
using System.Collections.Generic;

namespace PopularizaceCz.Services.ICalExport
{
    public interface IICalExporter
    {
		string Export(IEnumerable<ICalEvent> events);
    }
}