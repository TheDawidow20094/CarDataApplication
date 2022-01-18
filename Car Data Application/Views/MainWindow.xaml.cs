using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Car_Data_Application.Models;
using Car_Data_Application.Controllers;
using System.IO;
using Newtonsoft.Json;

namespace Car_Data_Application.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User User = new User();
        
        public MainWindow()
        {
            InitializeData();
            InitializeComponent();
            SetFooterData();
            HomeContentGenerator OpenHomePage = new HomeContentGenerator();
            OpenHomePage.GeneratorHomeContent(this, User);
        }

        public void InitializeData()
        {
            string JsonResultUser = File.ReadAllText(@"../../../JSON_Files/VehiclesTestJson.json");
            User = JsonConvert.DeserializeObject<User>(JsonResultUser);
        }

        private void VehiclesOnClick(object sender, RoutedEventArgs e)
        {
            VehiclesContentGenerator Generator = new VehiclesContentGenerator();
            Generator.GeneratorVechicleList(this, User);
        }

        private void RefuelingHistoryClick(object sender, RoutedEventArgs e)
        {
            RefuelingHistoryContentGenerator Generator = new RefuelingHistoryContentGenerator();
            Generator.GeneratorRefulingHistory(this, User);
        }

        private void ServicesClick(object sender, RoutedEventArgs e)
        {
            CostContentGenerator Generator = new CostContentGenerator();
            Generator.CostGenerator(this, User);
        }

        public void SetFooterData()
        {
            CarNameButton.Content = User.Vehicles[User.ActiveCarIndex].Brand + " " + User.Vehicles[User.ActiveCarIndex].Model;
            UserName.Text = User.UserName + " " + User.UserSurname;
        }

        private void ChangeActiveCarClick(object sender, RoutedEventArgs e)
        {
            GenerateSelectedCar Generator = new GenerateSelectedCar();
            Generator.GeneratorCarSelectList(this, User);
        }

        private void HomeOnClick(object sender, RoutedEventArgs e)
        {
            HomeContentGenerator Generator = new HomeContentGenerator();
            Generator.GeneratorHomeContent(this, User);
        }
    }
}
