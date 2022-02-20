using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Car_Data_Application.Models.XML_Models
{
	[XmlRoot(ElementName = "StatsPage")]
	public class StatsPage
	{

		[XmlElement(ElementName = "StatsDataPage")]
		public StatsDataPage StatsDataPage { get; set; }

		[XmlElement(ElementName = "StatsChartsPage")]
		public StatsChartsPage StatsChartsPage { get; set; }
	}
}
