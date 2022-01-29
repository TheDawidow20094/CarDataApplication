using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Car_Data_Application.Models.XML_Models
{
	[XmlRoot(ElementName = "Button")]
	public class Button
	{

		[XmlAttribute(AttributeName = "PL")]
		public string PL { get; set; }

		[XmlAttribute(AttributeName = "EN")]
		public string EN { get; set; }

		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "IsEnabled")]
		public string IsEnabled { get; set; }
	}

	[XmlRoot(ElementName = "SidePanel")]
	public class SidePanel
	{

		[XmlElement(ElementName = "Button")]
		public List<Button> Button { get; set; }
	}

	[XmlRoot(ElementName = "MainView")]
	public class MainView
	{

		[XmlElement(ElementName = "SidePanel")]
		public SidePanel SidePanel { get; set; }

		[XmlElement(ElementName = "MainPanel")]
		public object MainPanel { get; set; }
	}
}
