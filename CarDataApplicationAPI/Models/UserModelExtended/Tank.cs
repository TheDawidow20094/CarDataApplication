using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDataApplicationAPI.Models.UserModelExtended
{
    public class Tanks
    {
        public int LPG { get; set; }
        public int Gasoline { get; set; }
        public int Diesel { get; set; }

        public Tanks(int lpg, int gasoline, int diesel)
        {
            this.Diesel = diesel;
            this.Gasoline = gasoline;
            this.LPG = lpg;
        }
    }
}
