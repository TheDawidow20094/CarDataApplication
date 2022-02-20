using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Car_Data_Application.Models.XML_Models
{
	[XmlRoot(ElementName = "XMLButton")]
	public class XMLButton
	{

		[XmlAttribute(AttributeName = "PL")]
		public string PL { get; set; }

		[XmlAttribute(AttributeName = "ENG")]
		public string ENG { get; set; }

		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "IsEnabled")]
		public bool IsEnabled { get; set; }

		[XmlAttribute(AttributeName = "Icon")]
		public string Icon { get; set; }

		[XmlAttribute(AttributeName = "IsSmallButton")]
		public bool IsSmallButton { get; set; }
	}
}
