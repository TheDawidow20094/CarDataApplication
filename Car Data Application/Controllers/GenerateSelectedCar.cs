using Car_Data_Application.Models;
using Car_Data_Application.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Car_Data_Application.Controllers
{
    class GenerateSelectedCar
    {
        public ComboBox Carlist = new ComboBox();
        public User User;

        public void GeneratorCarSelectList(MainWindow mw, User user)
        {
            User = user;

            Carlist.SelectionChanged += CarlistSelectionChanged;

            foreach (Vehicle vehicle in user.Vehicles)
            {
                Carlist.Items.Add(vehicle.Brand + " " + vehicle.Model);
            }

            mw.CarName.Children.Add(Carlist);        
        }

        private void CarlistSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User.ActiveCarIndex = Carlist.SelectedIndex;
            string jsonString = JsonSerializer.Serialize(User);
            File.WriteAllText(@"../../../JSON_Files/VehiclesTestJson.json", jsonString);
        }
    }
}
