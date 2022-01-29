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

		[XmlAttribute(AttributeName = "EN")]
		public string ENG { get; set; }

		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "IsEnabled")]
		public bool IsEnabled { get; set; }
	}

	[XmlRoot(ElementName = "SidePanel")]
	public class SidePanel
	{

		[XmlElement(ElementName = "XMLButton")]
		public List<XMLButton> XMLButton { get; set; }
	}

	[XmlRoot(ElementName = "Translation")]
	public class Translation
	{

		[XmlAttribute(AttributeName = "PL")]
		public string PL { get; set; }

		[XmlAttribute(AttributeName = "EN")]
		public string ENG { get; set; }
	}

	[XmlRoot(ElementName = "VehicleNameGrid")]
	public class VehicleNameGrid
	{

		[XmlElement(ElementName = "Brand")]
		public Translation Brand { get; set; }

		[XmlElement(ElementName = "Model")]
		public Translation Model { get; set; }
	}

	[XmlRoot(ElementName = "PrimaryInfoGrid")]
	public class PrimaryInfoGrid
	{

		[XmlElement(ElementName = "YearOfManufacture")]
		public Translation YearOfManufacture { get; set; }

		[XmlElement(ElementName = "Vin")]
		public Translation Vin { get; set; }

		[XmlElement(ElementName = "Plates")]
		public Translation Plates { get; set; }

		[XmlElement(ElementName = "Millage")]
		public Translation Millage { get; set; }
	}

	[XmlRoot(ElementName = "FuelTankInfoGrid")]
	public class FuelTankInfoGrid
	{

		[XmlElement(ElementName = "Gasoline")]
		public Translation Gasoline { get; set; }

		[XmlElement(ElementName = "Diesel")]
		public Translation Diesel { get; set; }

		[XmlElement(ElementName = "LPG")]
		public Translation LPG { get; set; }
	}

	[XmlRoot(ElementName = "CyclicalCostGrid")]
	public class CyclicalCostGrid
	{

		[XmlElement(ElementName = "InsuranceStartDate")]
		public Translation InsuranceStartDate { get; set; }

		[XmlElement(ElementName = "InsuranceEndDate")]
		public Translation InsuranceEndDate { get; set; }

		[XmlElement(ElementName = "InsurancePrice")]
		public Translation InsurancePrice { get; set; }

		[XmlElement(ElementName = "InspectionStartDate")]
		public Translation InspectionStartDate { get; set; }

		[XmlElement(ElementName = "InspectionEndDate")]
		public Translation InspectionEndDate { get; set; }

		[XmlElement(ElementName = "InspectionPrice")]
		public Translation InspectionPrice { get; set; }
	}

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

	[XmlRoot(ElementName = "MainPanel")]
	public class MainPanel
	{

		[XmlElement(ElementName = "VehiclesPage")]
		public VehiclesPage VehiclesPage { get; set; }
	}

	[XmlRoot(ElementName = "MainGrid")]
	public class MainGrid
	{

		[XmlElement(ElementName = "SidePanel")]
		public SidePanel SidePanel { get; set; }

		[XmlElement(ElementName = "MainPanel")]
		public MainPanel MainPanel { get; set; }
	}
}
