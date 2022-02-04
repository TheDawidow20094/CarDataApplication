using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Car_Data_Application.Models.XML_Models
{
	[XmlRoot(ElementName = "Translation")]
	public class Translation
	{

		[XmlAttribute(AttributeName = "PL")]
		public string PL { get; set; }

		[XmlAttribute(AttributeName = "ENG")]
		public string ENG { get; set; }
	}
}
