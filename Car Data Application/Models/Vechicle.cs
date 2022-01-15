using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Data_Application.Models.Vehicle_Classes;

namespace Car_Data_Application.Models
{
    class Vechicle
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int YearOfManufacture { get; set; }
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


        public Vechicle(string brand, string model, int yearofmanufacture, string plates, string vin, Tank tanks, double consumptions, List<Refueling> refuelings, List<string> imagessrc, List<Service> services, FuelType fueltype, CyclicalCosts insurance, CyclicalCosts inspection)
        {
            this.Brand = brand;
            this.Model = model;
            this.YearOfManufacture = yearofmanufacture;
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
