using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Data_Application.Models.Vehicle_Classes
{
    public class Reminder
    {
        public string Date { get; set; }
        public int Millage { get; set; }

        public Reminder(string date, int millage)
        {
            this.Date = date;
            this.Millage = millage;
        }
    }
}
