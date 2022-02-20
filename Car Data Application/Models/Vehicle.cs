using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Data_Application.Models.Vehicle_Classes;

namespace Car_Data_Application.Models
{
     public class Vehicle
    {
        public int Id { get; set; }
        public string PictureFileName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int YearOfManufacture { get; set; }
        public int CarMillage { get; set; }
        public double AverageFuelConsumption { get; set; }
        public string Plates { get; set; }
        public string Vin { get; set; }
        public Tanks Tanks { get; set; }
        public List<Refueling> Refulings { get; set; }
        public List<Service> Services { get; set; }
        public CyclicalCosts Insurance { get; set; }
        public CyclicalCosts Inspection { get; set; }
        public List<EntriesList> EntriesList { get; set; }

        public Vehicle()
        {
            this.Tanks = new Tanks();
            this.Refulings = new List<Refueling>();
            this.Services = new List<Service>();
            this.EntriesList = new List<EntriesList>();
        }

        public Vehicle(int id, string picturefilename, string brand, string model, int yearofmanufacture, int carmillage, double averagefuelconsumption, string plates, string vin, Tanks tanks, List<Refueling> refuelings, List<Service> services, CyclicalCosts insurance, CyclicalCosts inspection, List<EntriesList> entries)
        {
            this.Id = id;
            this.PictureFileName = picturefilename;
            this.Brand = brand;
            this.Model = model;
            this.YearOfManufacture = yearofmanufacture;
            this.CarMillage = carmillage;
            this.AverageFuelConsumption = averagefuelconsumption;
            this.Plates = plates;
            this.Vin = vin;
            this.Tanks = tanks;
            this.Refulings = refuelings;
            this.Services = services;
            this.Insurance = insurance;
            this.Inspection = inspection;
            this.EntriesList = entries;
        }
    }
}
