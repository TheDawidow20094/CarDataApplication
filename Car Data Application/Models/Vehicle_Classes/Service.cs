using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Data_Application.Models.Vehicle_Classes
{
    public class Service
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int CarMillage { get; set; }
        public Reminder Reminder { get; set; }
        public string Comment { get; set; }

        public Service(string name, string category, double price, string date, string time, int carmillage, Reminder reminder, string comment)
        {
            this.Name = name;
            this.Category = category;
            this.Price = price;
            this.Date = date;
            this.Time = time;
            this.CarMillage = carmillage;
            this.Reminder = reminder;
            this.Comment = comment;
        }
    }
}
