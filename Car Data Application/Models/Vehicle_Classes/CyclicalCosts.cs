using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Data_Application.Models.Vehicle_Classes
{
    public class CyclicalCosts
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Price { get; set; }

        public CyclicalCosts(string startdate, string enddate, int price)
        {
            this.StartDate = startdate;
            this.EndDate = enddate;
            this.Price = price;
        }
    }
}
