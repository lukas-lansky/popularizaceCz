using System;

namespace PopularizaceCz.Services.ICalExport
{
	public class ICalEvent
	{
		public string Name { get; set; }

		public DateTime Start { get; set; }

		public DateTime? End { get; set; }

		public string Location { get; set; }
    }
}