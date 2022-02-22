using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarDataApplicationAPI.Models
{

    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserLanguage { get; set; }
        public string UnitType { get; set; }
        public string Currency { get; set; }
        public int ActiveCarIndex { get; set; }
        public List<Vehicle> Vehicles { get; set; }

        //public UserModel(int id, string login, string userlanguage, string unittype, string currency, int activecarindex, List<Vehicle> vehicles)
        //{
        //    this.Id = id;
        //    this.Login = login;
        //    this.UserLanguage = userlanguage;
        //    this.UnitType = unittype;
        //    this.Currency = currency;
        //    this.ActiveCarIndex = activecarindex;
        //    this.Vehicles = vehicles;
        //}

        public UserModel()
        {

        }

    }
}
