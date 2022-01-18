using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Data_Application.Models.Vehicle_Classes;

namespace Car_Data_Application.Models
{
    class Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int YearOfManufacture { get; set; }
        public int CarMillage { get; set; }
        public double AverageFuelConsumption { get; set; }
        public double LatestConsumption { get; set; }
        public double LatestFuelPrice { get; set; }
        public double ThisMounthFuelCost { get; set; }
        public double ThisMounthOtherCost { get; set; }
        public double PreviousMounthFuelCost { get; set; }
        public double PreviousMounthOtherCost { get; set; }
        public string Plates { get; set; }
        public string Vin { get; set; }
        public Tank Tanks { get; set; }
        public double Consumptions { get; set; }
        public List<Refueling> Refulings { get; set; }
        public List<string> ImagesSrc { get; set; }
        public List<Service> Services { get; set; }
        public FuelType FuelType { get; set; }
        public CyclicalCosts Insurance { get; set; }
        public CyclicalCosts Inspection { get; set; }


        public Vehicle(int id, string brand, string model, int yearofmanufacture, int carmillage, double averagefuelconsumption, double latestfuelprice, double latestconsumption, double thismounthfuelcost, double thismounthothercost, double previousmounthfuelcost, double previousmounthothercost, string plates, string vin, Tank tanks, double consumptions, List<Refueling> refuelings, List<string> imagessrc, List<Service> services, FuelType fueltype, CyclicalCosts insurance, CyclicalCosts inspection)
        {
            this.Id = id;
            this.Brand = brand;
            this.Model = model;
            this.YearOfManufacture = yearofmanufacture;
            this.CarMillage = carmillage;
            this.AverageFuelConsumption = averagefuelconsumption;
            this.LatestConsumption = latestconsumption;
            this.LatestFuelPrice = latestfuelprice;
            this.ThisMounthFuelCost = thismounthfuelcost;
            this.ThisMounthOtherCost = thismounthothercost;
            this.PreviousMounthFuelCost = previousmounthfuelcost;
            this.PreviousMounthOtherCost = previousmounthothercost;
            this.Plates = plates;
            this.Vin = vin;
            this.Tanks = tanks;
            this.Consumptions = consumptions;
            this.Refulings = refuelings;
            this.ImagesSrc = imagessrc;
            this.Services = services;
            this.FuelType = fueltype;
            this.Insurance = insurance;
            this.Inspection = inspection;
        }
    }
}
