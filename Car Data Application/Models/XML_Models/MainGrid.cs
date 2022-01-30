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

		[XmlAttribute(AttributeName = "ENG")]
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
	}

	[XmlRoot(ElementName = "AddRefuelingPage")]
	public class AddRefuelingPage
	{

		[XmlElement(ElementName = "PageTitle")]
		public Translation PageTitle { get; set; }

		[XmlElement(ElementName = "Liters")]
		public Translation Liters { get; set; }

		[XmlElement(ElementName = "PriceForLiter")]
		public Translation PriceForLiter { get; set; }

		[XmlElement(ElementName = "TotalPrice")]
		public Translation TotalPrice { get; set; }

		[XmlElement(ElementName = "IsFull")]
		public Translation IsFull { get; set; }

		[XmlElement(ElementName = "CarMillage")]
		public Translation CarMillage { get; set; }

		[XmlElement(ElementName = "Date")]
		public Translation Date { get; set; }

		[XmlElement(ElementName = "Time")]
		public Translation Time { get; set; }

		[XmlElement(ElementName = "Comment")]
		public Translation Comment { get; set; }

		[XmlElement(ElementName = "FuelType")]
		public Translation FuelType { get; set; }

		[XmlElement(ElementName = "ButtonText")]
		public Translation ButtonText { get; set; }
	}

	[XmlRoot(ElementName = "MainGrid")]
	public class MainGrid
	{

		[XmlElement(ElementName = "SidePanel")]
		public SidePanel SidePanel { get; set; }

		[XmlElement(ElementName = "MainPanel")]
		public MainPanel MainPanel { get; set; }
	}

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

	[XmlRoot(ElementName = "FuelData")]
	public class FuelData
	{

		[XmlElement(ElementName = "AverageConsumption")]
		public Translation AverageConsumption { get; set; }

		[XmlElement(ElementName = "LastConsumption")]
		public Translation LastConsumption { get; set; }

		[XmlElement(ElementName = "LastFuelPrice")]
		public Translation LastFuelPrice { get; set; }
	}

	[XmlRoot(ElementName = "CostData")]
	public class CostData
	{

		[XmlElement(ElementName = "ThisMounth")]
		public Translation ThisMounth { get; set; }

		[XmlElement(ElementName = "ThisMounthFuelCost")]
		public Translation ThisMounthFuelCost { get; set; }

		[XmlElement(ElementName = "ThisMounthOtherCost")]
		public Translation ThisMounthOtherCost { get; set; }

		[XmlElement(ElementName = "PreviousMounth")]
		public Translation PreviousMounth { get; set; }

		[XmlElement(ElementName = "PreviousMounthFuelCost")]
		public Translation PreviousMounthFuelCost { get; set; }

		[XmlElement(ElementName = "PreviousMounthOtherCost")]
		public Translation PreviousMounthOtherCost { get; set; }
	}

	[XmlRoot(ElementName = "XMLEntriesList")]
	public class XMLEntriesList
	{

		[XmlElement(ElementName = "EntriesListText")]
		public Translation EntriesListText { get; set; }

		[XmlElement(ElementName = "Date")]
		public Translation Date { get; set; }

		[XmlElement(ElementName = "Price")]
		public Translation Price { get; set; }

		[XmlElement(ElementName = "Descryption")]
		public Translation Descryption { get; set; }
	}

	[XmlRoot(ElementName = "LoginPanel")]
	public class LoginPanel
	{
		[XmlElement(ElementName = "UserNameText")]
		public Translation UserNameText { get; set; }

		[XmlElement(ElementName = "PasswordText")]
		public Translation PasswordText { get; set; }

		[XmlElement(ElementName = "LogInButton")]
		public Translation LogInButton { get; set; }

		[XmlElement(ElementName = "RegisterButton")]
		public Translation RegisterButton { get; set; }
	}

	[XmlRoot(ElementName = "RegisterPanel")]
	public class RegisterPanel
	{
		[XmlElement(ElementName = "UserNameText")]
		public Translation UserNameText { get; set; }

		[XmlElement(ElementName = "PasswordText")]
		public Translation PasswordText { get; set; }

		[XmlElement(ElementName = "RePasswordText")]
		public Translation RePasswordText { get; set; }

		[XmlElement(ElementName = "EmailText")]
		public Translation EmailText { get; set; }

		[XmlElement(ElementName = "RegisterButton")]
		public Translation RegisterButton { get; set; }
	}

	[XmlRoot(ElementName = "AddButonList")]
	public class AddButonList
	{
		[XmlElement(ElementName = "AddRefueling")]
		public Translation AddRefueling { get; set; }

		[XmlElement(ElementName = "AddCost")]
		public Translation AddCost { get; set; }

		[XmlElement(ElementName = "AddVehicle")]
		public Translation AddVehicle { get; set; }
		}
}


