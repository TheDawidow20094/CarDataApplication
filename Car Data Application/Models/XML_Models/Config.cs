using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Car_Data_Application.Models.XML_Models
{
	[XmlRoot(ElementName = "Config")]
	public class Config
	{
		[XmlElement(ElementName = "SidePanel")]
		public SidePanel SidePanel { get; set; }

		[XmlElement(ElementName = "MainPanel")]
		public MainPanel MainPanel { get; set; }
	}

}


