using Car_Data_Application.Models;
using Car_Data_Application.Models.Vehicle_Classes;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Car_Data_Application.Controllers
{
    class Stats_DataContent : CarDataAppController
    {
        private StatsDataPage PTranslation;

        public void PageGenerator(MainWindow mw, User user, StatsDataPage translation)
        {
            InitialAssignValue(mw, user, translation);

            Grid MainGrid = new();

            List<int> RowsHeights = new List<int>() { 70, 140, 70, 70, 70, 210 };
            foreach (int RowHeight in RowsHeights)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(RowHeight)});
            }

            MainGrid.Children.Add(RefuelingsStatsTitle());
            MainGrid.Children.Add(RefuelingsStatsContent());
            MainGrid.Children.Add(ServicesStatsTitle());
            MainGrid.Children.Add(ServicesStatsContent());
            MainGrid.Children.Add(UserStatsTitle());
            MainGrid.Children.Add(UserStatsContent());

            mainWindow.ScrollViewerContent.Content = MainGrid;

        }

        private void InitialAssignValue(MainWindow mw, User user, StatsDataPage translation)
        {
            PTranslation = translation;
            mainWindow = mw;
            PUser = user;
            mainWindow.AddButon.Visibility = Visibility.Hidden;
            mainWindow.WhereAreYou = "DataStatsPage";
            SetButtonColor(mainWindow.WhereAreYou, (Grid)mainWindow.FindName("SidePanel"));
        }

        private TextBlock RefuelingsStatsTitle()
        {
            TextBlock RefuelingsStats = GenerateTextBlock(PTranslation.RefuelingStatsTitle, PUser.UserLanguage, 0, 0, horizontalAlignment: HorizontalAlignment.Center, isTitle: true, isTitleFontSize: 24);

            return RefuelingsStats;
        }

        private Grid RefuelingsStatsContent()
        {
            Grid RefuelingsStatsGrid = new();
            SetGridProps(ref RefuelingsStatsGrid, 1);

            for (int i = 0; i < 2; i++)
            {
                RefuelingsStatsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 4; i++)
            {
                RefuelingsStatsGrid.RowDefinitions.Add(new RowDefinition());
            }

            RefuelingsStatsGrid.Children.Add(GenerateTextBlock(PTranslation.RefuelingsCount, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Right));
            RefuelingsStatsGrid.Children.Add(GenerateTextBlock(PTranslation.FuelCostAllVehicles, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
            RefuelingsStatsGrid.Children.Add(GenerateTextBlock(PTranslation.LitersCount, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right));
            RefuelingsStatsGrid.Children.Add(GenerateTextBlock(PTranslation.AverageConsumption, PUser.UserLanguage, 3, 0, LightTextColor, HorizontalAlignment.Right));

            List<Refueling> ActiveVehicleRefuelingsList = PUser.Vehicles[PUser.ActiveCarIndex].Refulings;
            int RefuelingsCount = ActiveVehicleRefuelingsList.Count;
            double AverageConsumption = PUser.Vehicles[PUser.ActiveCarIndex].AverageFuelConsumption;
            double RefuelingCost = 0;
            double LitersCount = 0;
            foreach (Refueling refueling in ActiveVehicleRefuelingsList)
            {
                RefuelingCost += refueling.TotalPrice;
                LitersCount += refueling.Liters;
            }

            RefuelingsStatsGrid.Children.Add(GenerateTextBlock(null, RefuelingsCount.ToString(), 0, 1));
            RefuelingsStatsGrid.Children.Add(GenerateTextBlock(null, Math.Round(RefuelingCost, 2).ToString(), 1, 1));
            RefuelingsStatsGrid.Children.Add(GenerateTextBlock(null, Math.Round(LitersCount, 2).ToString(), 2, 1));
            RefuelingsStatsGrid.Children.Add(GenerateTextBlock(null, Math.Round(AverageConsumption, 2).ToString(), 3, 1));

            return RefuelingsStatsGrid;
        }

        private TextBlock ServicesStatsTitle()
        {
            TextBlock ServicesStats = GenerateTextBlock(PTranslation.ServicesStatsTitle, PUser.UserLanguage, 2, 0, horizontalAlignment: HorizontalAlignment.Center, isTitle: true, isTitleFontSize: 24);

            return ServicesStats;
        }

        private Grid ServicesStatsContent()
        {
            Grid ServicesStatsGrid = new();
            SetGridProps(ref ServicesStatsGrid, 3);

            for (int i = 0; i < 2; i++)
            {
                ServicesStatsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 2; i++)
            {
                ServicesStatsGrid.RowDefinitions.Add(new RowDefinition());
            }

            ServicesStatsGrid.Children.Add(GenerateTextBlock(PTranslation.ServicesCount, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Right));
            ServicesStatsGrid.Children.Add(GenerateTextBlock(PTranslation.ServicesCost, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));

            List<Service> ActiveVehicleServicesList = PUser.Vehicles[PUser.ActiveCarIndex].Services;
            int ServicesCount = ActiveVehicleServicesList.Count;
            double ServicesCost = 0;
            foreach (Service service in ActiveVehicleServicesList)
            {
                ServicesCost += service.Price;
            }

            ServicesStatsGrid.Children.Add(GenerateTextBlock(null, ServicesCount.ToString(), 0, 1));
            ServicesStatsGrid.Children.Add(GenerateTextBlock(null, Math.Round(ServicesCost, 2).ToString(), 1, 1));

            return ServicesStatsGrid;
        }

        private TextBlock UserStatsTitle()
        {
            TextBlock ServicesStatsTitle = GenerateTextBlock(PTranslation.UserStatsTitle, PUser.UserLanguage, 4, 0, horizontalAlignment: HorizontalAlignment.Center, isTitle: true, isTitleFontSize: 24);

            return ServicesStatsTitle;
        }

        private Grid UserStatsContent()
        {
            Grid UserStatsGrid = new();
            SetGridProps(ref UserStatsGrid, 5);

            for (int i = 0; i < 2; i++)
            {
                UserStatsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 6; i++)
            {
                UserStatsGrid.RowDefinitions.Add(new RowDefinition());
            }

            UserStatsGrid.Children.Add(GenerateTextBlock(PTranslation.VehiclesCount, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Right));
            UserStatsGrid.Children.Add(GenerateTextBlock(PTranslation.MonthlyMillage, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
            UserStatsGrid.Children.Add(GenerateTextBlock(PTranslation.YearlyMillage, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right));
            UserStatsGrid.Children.Add(GenerateTextBlock(PTranslation.FuelCostAllVehicles, PUser.UserLanguage, 3, 0, LightTextColor, HorizontalAlignment.Right));
            UserStatsGrid.Children.Add(GenerateTextBlock(PTranslation.ServicesCostAllVehicles, PUser.UserLanguage, 4, 0, LightTextColor, HorizontalAlignment.Right));
            UserStatsGrid.Children.Add(GenerateTextBlock(PTranslation.AllCostCount, PUser.UserLanguage, 5, 0, LightTextColor, HorizontalAlignment.Right));

            int VehicleCount = PUser.Vehicles.Count;
            double MonthlyKilometers = 0;
            double YearlyKilometers = 0;
            double VehiclesFuelCost = 0;
            double VehiclesServicesCost = 0;

            foreach (Vehicle vehicle in PUser.Vehicles)
            {
                foreach (Refueling refueling in vehicle.Refulings)
                {
                    VehiclesFuelCost += refueling.TotalPrice;
                }

                foreach (Service service in vehicle.Services)
                {
                    VehiclesServicesCost += service.Price;
                }
            }

            double AllCosts = VehiclesFuelCost + VehiclesServicesCost;


            UserStatsGrid.Children.Add(GenerateTextBlock(null, VehicleCount.ToString(), 0, 1));
            UserStatsGrid.Children.Add(GenerateTextBlock(null, "BRAK FUNKCJI!", 1, 1));
            UserStatsGrid.Children.Add(GenerateTextBlock(null, "BRAK FUNKCJI !", 2, 1));
            UserStatsGrid.Children.Add(GenerateTextBlock(null, Math.Round(VehiclesFuelCost, 2).ToString(), 3, 1));
            UserStatsGrid.Children.Add(GenerateTextBlock(null, Math.Round(VehiclesServicesCost, 2).ToString(), 4, 1));
            UserStatsGrid.Children.Add(GenerateTextBlock(null, Math.Round(AllCosts, 2).ToString(), 5, 1));

            return UserStatsGrid;
        }
    }
}
