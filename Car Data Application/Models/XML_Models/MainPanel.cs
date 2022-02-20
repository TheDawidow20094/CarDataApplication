using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Car_Data_Application.Models.XML_Models
{

	[XmlRoot(ElementName = "MainPanel")]
	public class MainPanel
	{
		[XmlElement(ElementName = "AddButonList")]
		public AddButonList AddButonList { get; set; }

		[XmlElement(ElementName = "VehiclesPage")]
		public VehiclesPage VehiclesPage { get; set; }

		[XmlElement(ElementName = "HomePage")]
		public HomePage HomePage { get; set; }

		[XmlElement(ElementName = "LoginPanel")]
		public LoginPanel LoginPanel { get; set; }

		[XmlElement(ElementName = "RegisterPanel")]
		public RegisterPanel RegisterPanel { get; set; }

		[XmlElement(ElementName = "AddRefuelingPage")]
		public AddRefuelingPage AddRefuelingPage { get; set; }

		[XmlElement(ElementName = "RefuelingHistoryPage")]
		public RefuelingHistoryPage RefuelingHistoryPage { get; set; }

		[XmlElement(ElementName = "CostPage")]
		public CostPage CostPage { get; set; }

		[XmlElement(ElementName = "AddCostPage")]
		public AddCostPage AddCostPage { get; set; }

		[XmlElement(ElementName = "BackupPanel")]
		public BackupPanel BackupPanel { get; set; }

		[XmlElement(ElementName = "SettingsPage")]
		public SettingsPage SettingsPage { get; set; }

		[XmlElement(ElementName = "CalculatorPage")]
		public CalculatorPage CalculatorPage { get; set; }

		[XmlElement(ElementName = "AddVehiclePage")]
		public AddVehiclePage AddVehiclePage { get; set; }

		[XmlElement(ElementName = "StatsPage")]
		public StatsPage StatsPage { get; set; }
	}
}
