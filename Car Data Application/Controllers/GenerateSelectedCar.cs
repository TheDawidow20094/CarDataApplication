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
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class GenerateSelectedCar
    {
        public ComboBox Carlist = new ComboBox();
        private User User;
        private MainWindow Mainwindow;

        public void GeneratorCarSelectList(MainWindow mw, User user)
        {
            Mainwindow = mw;
            User = user;
            Carlist.Background = Brushes.LightCoral;
            Carlist.HorizontalContentAlignment = HorizontalAlignment.Center;
            Carlist.VerticalContentAlignment = VerticalAlignment.Center;
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
            User.SerializeData();
            RefreshPage();
        }

        public void RefreshPage()
        { 
            switch (Mainwindow.WhereAreYou)
            {
                case "HomePage":
                    HomeContentGenerator RefreshHomePage = new HomeContentGenerator();
                    RefreshHomePage.GeneratorHomeContent(Mainwindow, User);
                break;

                case "CostPage":
                    CostContentGenerator RefreshCostPage = new CostContentGenerator();
                    RefreshCostPage.CostGenerator(Mainwindow, User);
                break;

                case "RefuelingPage":
                    RefuelingHistoryContentGenerator RefreshRefuelinfHistoryPage = new RefuelingHistoryContentGenerator();
                    RefreshRefuelinfHistoryPage.GeneratorRefulingHistory(Mainwindow, User);
                break;

                case "CalculatorPage":
                    CalculatorContentGenerator RefreshCalculatorPage = new CalculatorContentGenerator();
                    RefreshCalculatorPage.CalculatorGenerator(Mainwindow, User);
                break;
            }
        }
    }
}
