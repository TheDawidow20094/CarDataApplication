using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Data_Application.Models.Vehicle_Classes
{
    class EntriesList
    {
        public string Type { get; set; }
        public string Date { get; set; }
        public double Price { get; set; }
        public string Descryption { get; set; }

        public EntriesList(string type, string date, double price, string descryption)
        {
            this.Type = type;
            this.Date = date;
            this.Price = price;
            this.Descryption = descryption;
        }
    }
}
