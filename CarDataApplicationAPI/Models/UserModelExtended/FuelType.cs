using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDataApplicationAPI.Models.UserModelExtended
{
    public class FuelType
    {
        public bool Gasoline { get; set; }
        public bool Diesel { get; set; }
        public bool LPG { get; set; }

        public FuelType(bool gasoline, bool diesel, bool lpg)
        {
            this.Diesel = diesel;
            this.Gasoline = gasoline;
            this.LPG = lpg;
        }
    }
}
