using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Data_Application.Models.Vehicle_Classes
{
    class Tank
    {
        public int LPG { get; set; }
        public int Gasoline { get; set; }
        public int Diesel { get; set; }

        public Tank(int lpg, int gasoline, int diesel)
        {
            this.Diesel = diesel;
            this.Gasoline = gasoline;
            this.LPG = lpg;
        }
    }
}
