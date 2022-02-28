using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Car_Data_Application.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserLanguage { get; set; }
        public string UnitsType { get; set; }
        public string Currency { get; set; }
        public int ActiveCarIndex { get; set; }
        public List<Vehicle> Vehicles { get; set; }

        public User(int id, string login, string password, string email, string userlanguage, string metricunit, string currency, int activecarindex, List<Vehicle> vehicles)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.Email = email;
            this.UserLanguage = userlanguage;
            this.UnitsType = metricunit;
            this.Currency = currency;
            this.ActiveCarIndex = activecarindex;
            this.Vehicles = vehicles;
        }

        public User()
        {

        }

        public void SerializeData()
        {
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(@"../../../JSON_Files/VehiclesTestJson.json", jsonString);
        }

        public string SerializeData(object data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            return jsonString;
        }
    }
}
