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
    public partial class MainWindow : Window
    {
        private BrushConverter Converter = new BrushConverter();
        User User = new User();
        public string WhereAreYou = string.Empty;
        
        public MainWindow()
        {
            InitializeData();
            InitializeComponent();
            SetFooterData();
            new CarDataAppController().GoToHomePage(this, User);
        }

        public void InitializeData()
        {
            string JsonResultUser = File.ReadAllText(@"../../../JSON_Files/VehiclesTestJson.json", Encoding.UTF8);
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
            UserName.Text = User.Login;
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

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            LoginWindow Loginwindow = new LoginWindow(this, User);
            Loginwindow.ShowDialog();
        }

        private void SetingClick(object sender, RoutedEventArgs e)
        {
            SetingsContentGenerator Generator = new SetingsContentGenerator();
            Generator.GenerateSetingContent(this, User);
        }

        private void CalculatorClick(object sender, RoutedEventArgs e)
        {
            CalculatorContentGenerator Generator = new CalculatorContentGenerator();
            Generator.CalculatorGenerator(this, User);
        }

        private void HandleAddButonMouseEnter(object sender, MouseEventArgs e)
        {
            AddButon.Background = (Brush)Converter.ConvertFromString("#FF4FE84F");
        }

        private void HandleAddButonMouseLeave(object sender, MouseEventArgs e)
        {
            AddButon.Background = (Brush)Converter.ConvertFromString("#FF38AE38");
        }

        private void HandleAddButtonClick(object sender, MouseButtonEventArgs e)
        {
            switch (WhereAreYou)
            {
                case "VehicleContentPage":
                    new AddVehiclePageGenerator().PageGenerator(this, User);
                break;

                case "HomePage":
                    MessageBox.Show("HomePage");
                break;

                case "RefuelingPage":
                    MessageBox.Show("RefuelingPage");
                break;
            }
        }
    }
}
