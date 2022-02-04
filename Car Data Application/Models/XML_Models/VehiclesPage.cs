using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Car_Data_Application.Models.XML_Models
{
	[XmlRoot(ElementName = "VehiclesPage")]
	public class VehiclesPage
	{

		[XmlElement(ElementName = "VehicleNameGrid")]
		public VehicleNameGrid VehicleNameGrid { get; set; }

		[XmlElement(ElementName = "PrimaryInfoGrid")]
		public PrimaryInfoGrid PrimaryInfoGrid { get; set; }

		[XmlElement(ElementName = "FuelTankInfoGrid")]
		public FuelTankInfoGrid FuelTankInfoGrid { get; set; }

		[XmlElement(ElementName = "CyclicalCostGrid")]
		public CyclicalCostGrid CyclicalCostGrid { get; set; }
	}
}
