using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Data_Application.Models
{
    class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserLanguage { get; set; }
        public int ActiveCarIndex { get; set; }
        public List<Vehicle> Vehicles { get; set; }

        public User(int id, string username, string usersurname, string userlanguage, int activecarindex, List<Vehicle> vehicles)
        {
            this.Id = id;
            this.UserName = username;
            this.UserSurname = usersurname;
            this.UserLanguage = userlanguage;
            this.ActiveCarIndex = activecarindex;
            this.Vehicles = vehicles;
        }

        public User()
        {

        }
    }
}
