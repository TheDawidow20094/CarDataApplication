using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Data_Application.Models.Vehicle_Classes
{
    class Refueling
    {
        public double Liters { get; set; }
        public double PriceForLiter { get; set; }
        public double TotalPrice { get; set; }
        public bool IsFull { get; set; }
        public int CarMillage { get; set; }
        public double LatestConsumption { get; set; }
        public double LatestFuelPrice { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Comment { get; set; }

        public Refueling(double liters, double priceforliter, double totalprice, bool isfull, int carmillage, double latestconsumption, double latestfuelprice, string date, string time, string comment)
        {
            this.Liters = liters;
            this.PriceForLiter = priceforliter;
            this.TotalPrice = totalprice;
            this.IsFull = isfull;
            this.CarMillage = carmillage;
            this.LatestConsumption = latestconsumption;
            this.LatestFuelPrice = latestfuelprice;
            this.Date = date;
            this.Time = time;
            this.Comment = comment;
        }

    }
}
