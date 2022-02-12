using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Data_Application.Models.Vehicle_Classes
{
    public class Refueling
    {
        public double Liters { get; set; }
        public double PriceForLiter { get; set; }
        public double TotalPrice { get; set; }
        public bool IsFull { get; set; }
        public int CarMillage { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Comment { get; set; }
        public string FuelType { get; set; }
        public double Distance { get; set; }
        public double Consumption { get; set; }

        public Refueling()
        {
            this.Liters = 0;
            this.PriceForLiter = 0;
            this.TotalPrice = 0;
            this.IsFull = true;
            this.CarMillage = 0;
            this.Date = "";
            this.Time = "";
            this.Comment = "";
            this.FuelType = "";
            this.Distance = 0;
            this.Consumption = 0;
        }

        public Refueling(double liters, double priceforliter, double totalprice, bool isfull, int carmillage, string date, string time, string comment, string fueltype, double distance, double consumption)
        {
            this.Liters = liters;
            this.PriceForLiter = priceforliter;
            this.TotalPrice = totalprice;
            this.IsFull = isfull;
            this.CarMillage = carmillage;
            this.Date = date;
            this.Time = time;
            this.Comment = comment;
            this.FuelType = fueltype;
            this.Distance = distance;
            this.Consumption = consumption;
        }

    }
}
