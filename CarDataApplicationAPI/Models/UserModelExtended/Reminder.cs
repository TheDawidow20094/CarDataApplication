using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDataApplicationAPI.Models.UserModelExtended
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
