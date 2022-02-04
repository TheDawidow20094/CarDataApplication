using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Car_Data_Application.Models.XML_Models
{
	[XmlRoot(ElementName = "CalculatorPage")]
	public class CalculatorPage
	{
		[XmlElement(ElementName = "TravelCostCalculatorBorder")]
		public TravelCostCalculatorBorder TravelCostCalculatorBorder { get; set; }

		[XmlElement(ElementName = "AverageFuelConsumptionCalculatorBorder")]
		public AverageFuelConsumptionCalculatorBorder AverageFuelConsumptionCalculatorBorder { get; set; }

		[XmlElement(ElementName = "TravelCostCalculator")]
		public Translation TravelCostCalculator { get; set; }

		[XmlElement(ElementName = "AverageFuelConsumptionCalculator")]
		public Translation AverageFuelConsumptionCalculator { get; set; }

		[XmlElement(ElementName = "Gasoline")]
		public Translation Gasoline { get; set; }

		[XmlElement(ElementName = "Diesel")]
		public Translation Diesel { get; set; }

		[XmlElement(ElementName = "LPG")]
		public Translation LPG { get; set; }

		[XmlElement(ElementName = "NoVehicleException")]
		public Translation NoVehicleException { get; set; }

		[XmlElement(ElementName = "CalculateButton")]
		public Translation CalculateButton { get; set; }
	}
}
