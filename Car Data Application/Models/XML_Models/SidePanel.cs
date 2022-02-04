using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Car_Data_Application.Models.XML_Models
{
	[XmlRoot(ElementName = "SidePanel")]
	public class SidePanel
	{

		[XmlElement(ElementName = "XMLButton")]
		public List<XMLButton> XMLButton { get; set; }
	}
}
