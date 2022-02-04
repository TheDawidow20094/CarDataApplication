using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Car_Data_Application.Models.XML_Models
{
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
		[XmlElement(ElementName = "FuelTankInfoTitle")]
		public Translation FuelTankInfoTitle { get; set; }

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
		[XmlElement(ElementName = "InsuranceTitle")]
		public Translation InsuranceTitle { get; set; }

		[XmlElement(ElementName = "InsuranceStartDate")]
		public Translation InsuranceStartDate { get; set; }

		[XmlElement(ElementName = "InsuranceEndDate")]
		public Translation InsuranceEndDate { get; set; }

		[XmlElement(ElementName = "InsurancePrice")]
		public Translation InsurancePrice { get; set; }

		[XmlElement(ElementName = "InspectionTitle")]
		public Translation InspectionTitle { get; set; }

		[XmlElement(ElementName = "InspectionStartDate")]
		public Translation InspectionStartDate { get; set; }

		[XmlElement(ElementName = "InspectionEndDate")]
		public Translation InspectionEndDate { get; set; }

		[XmlElement(ElementName = "InspectionPrice")]
		public Translation InspectionPrice { get; set; }
	}

	[XmlRoot(ElementName = "AddVehiclePage")]
	public class AddVehiclePage
	{
		[XmlElement(ElementName = "Title")]
		public Translation Title { get; set; }

		[XmlElement(ElementName = "Brand")]
		public Translation Brand { get; set; }

		[XmlElement(ElementName = "Model")]
		public Translation Model { get; set; }

		[XmlElement(ElementName = "YearOfManufacture")]
		public Translation YearOfManufacture { get; set; }

		[XmlElement(ElementName = "VIN")]
		public Translation VIN { get; set; }

		[XmlElement(ElementName = "Plates")]
		public Translation Plates { get; set; }

		[XmlElement(ElementName = "VehicleMillage")]
		public Translation VehicleMillage { get; set; }

		[XmlElement(ElementName = "GasolineTank")]
		public Translation GasolineTank { get; set; }

		[XmlElement(ElementName = "DieselTank")]
		public Translation DieselTank { get; set; }

		[XmlElement(ElementName = "LPGTank")]
		public Translation LPGTank { get; set; }

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

		[XmlElement(ElementName = "AddButton")]
		public Translation AddButton { get; set; }
	}

	[XmlRoot(ElementName = "TravelCostCalculatorBorder")]
	public class TravelCostCalculatorBorder
	{
		[XmlElement(ElementName = "Distance")]
		public Translation Distance { get; set; }

		[XmlElement(ElementName = "PriceForLiter")]
		public Translation PriceForLiter { get; set; }

		[XmlElement(ElementName = "Consumption")]
		public Translation Consumption { get; set; }

		[XmlElement(ElementName = "Resoult")]
		public Translation Resoult { get; set; }

		[XmlElement(ElementName = "CalculateButton")]
		public Translation CalculateButton { get; set; }

		[XmlElement(ElementName = "ErrorException")]
		public Translation ErrorException { get; set; }
	}

	[XmlRoot(ElementName = "AverageFuelConsumptionCalculatorBorder")]
	public class AverageFuelConsumptionCalculatorBorder
	{
		[XmlElement(ElementName = "ConsumedFuel")]
		public Translation ConsumedFuel { get; set; }

		[XmlElement(ElementName = "NumberOfKilometersTraveled")]
		public Translation NumberOfKilometersTraveled { get; set; }

		[XmlElement(ElementName = "PriceForLiterOptional")]
		public Translation PriceForLiterOptional { get; set; }

		[XmlElement(ElementName = "Resoult")]
		public Translation Resoult { get; set; }

		[XmlElement(ElementName = "CalculateButton")]
		public Translation CalculateButton { get; set; }

		[XmlElement(ElementName = "ErrorException")]
		public Translation ErrorException { get; set; }
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

	[XmlRoot(ElementName = "RefuelingHistoryPage")]
	public class RefuelingHistoryPage
	{
		[XmlElement(ElementName = "RefuelingCost")]
		public Translation RefuelingCost { get; set; }

		[XmlElement(ElementName = "AmountOfFuel")]
		public Translation AmountOfFuel { get; set; }

		[XmlElement(ElementName = "PricePerLiter")]
		public Translation PricePerLiter { get; set; }

		[XmlElement(ElementName = "Consumption")]
		public Translation Consumption { get; set; }
	}

	[XmlRoot(ElementName = "CostPage")]
	public class CostPage
	{
		[XmlElement(ElementName = "Cost")]
		public Translation Cost { get; set; }

		[XmlElement(ElementName = "Type")]
		public Translation Type { get; set; }

		[XmlElement(ElementName = "Date")]
		public Translation Date { get; set; }

		[XmlElement(ElementName = "Comment")]
		public Translation Comment { get; set; }
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

	[XmlRoot(ElementName = "SettingsPage")]
	public class SettingsPage
	{
		[XmlElement(ElementName = "Language")]
		public Translation Language { get; set; }

		[XmlElement(ElementName = "MetricUnit")]
		public Translation MetricUnit { get; set; }

		[XmlElement(ElementName = "Currency")]
		public Translation Currency { get; set; }

		[XmlElement(ElementName = "ApplyButton")]
		public Translation ApplyButton { get; set; }
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
