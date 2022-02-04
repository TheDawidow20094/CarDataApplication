using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Car_Data_Application.Models.XML_Models
{
	[XmlRoot(ElementName = "HomePage")]
	public class HomePage
	{

		[XmlElement(ElementName = "FuelData")]
		public FuelData FuelData { get; set; }

		[XmlElement(ElementName = "CostData")]
		public CostData CostData { get; set; }

		[XmlElement(ElementName = "XMLEntriesList")]
		public XMLEntriesList XMLEntriesList { get; set; }
	}
}
