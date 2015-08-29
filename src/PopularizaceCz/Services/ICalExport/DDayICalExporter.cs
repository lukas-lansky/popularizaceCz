using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;
using System.Collections.Generic;

namespace PopularizaceCz.Services.ICalExport
{
	public class DDayICalExporter : IICalExporter
	{
		public string Export(IEnumerable<ICalEvent> events)
		{
			var ical = new iCalendar();

			foreach (var appEvent in events)
			{
				var libEvent = ical.Create<Event>();

				libEvent.Name = appEvent.Name;
				libEvent.Start = new iCalDateTime(appEvent.Start);

				if (appEvent.End.HasValue)
				{
					libEvent.End = new iCalDateTime(appEvent.End.Value);
				}

				libEvent.Location = appEvent.Location;
			}

			return new iCalendarSerializer().SerializeToString(ical);
		}
	}
}