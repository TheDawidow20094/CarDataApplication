using Car_Data_Application.Models;
using Car_Data_Application.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Car_Data_Application.Models.Vehicle_Classes;
using System;
using Car_Data_Application.Models.XML_Models;
using System.Windows.Media.Effects;

namespace Car_Data_Application.Controllers
{
    class HomeContentGenerator : CarDataAppController
    {
        public void GeneratorHomeContent(MainWindow mw, User user, HomePage homePage)
        {
            InitialAssignValue(mw, user);
           
            Grid Grid = new Grid();

            Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(135) });
            Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(200) });
            Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(80) });
            Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(280) });

            Grid.Children.Add(FuelDataGenerator(user, homePage.FuelData));
            Grid.Children.Add(CostDataGenerator(user, homePage.CostData));
            Grid.Children.Add(EntriesListText(homePage.XMLEntriesList));
            Grid.Children.Add(EnteriesListGenerator(user, homePage.XMLEntriesList));

            mw.ScrollViewerContent.Content = Grid;
        }

        private void InitialAssignValue(MainWindow mw, User user)
        {
            mainWindow = mw;
            PUser = user;
            mainWindow.WhereAreYou = "HomePage";
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.FindName("SidePanel")));
        }

        private Grid FuelDataGenerator(User user, FuelData translation)
        {
            Grid FuelDataGrid = new Grid();
            SetGridProps(ref FuelDataGrid, 0);

            FuelDataGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(114) });
            FuelDataGrid.ColumnDefinitions.Add(new ColumnDefinition());

            FuelDataGrid.Children.Add(GenerateIcon("../../../Images/Icons/gas-station.png", 0, 0));

            Grid FuelDataGridContent = new Grid();
            Grid.SetColumn(FuelDataGridContent, 1);
            FuelDataGridContent.VerticalAlignment = VerticalAlignment.Center;
            FuelDataGridContent.HorizontalAlignment = HorizontalAlignment.Center;


            for (int i = 0; i < 2; i++) // 2 columns in this grid
            {
                FuelDataGridContent.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 3; i++) // 3 rows in this grid
            {
                FuelDataGridContent.RowDefinitions.Add(new RowDefinition());
            }

            int LastRefuelingElement = user.Vehicles[user.ActiveCarIndex].Refulings.Count();

            FuelDataGridContent.Children.Add(GenerateTextBlock(translation.AverageConsumption, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Right));
            FuelDataGridContent.Children.Add(GenerateTextBlock(translation.LastConsumption, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
            FuelDataGridContent.Children.Add(GenerateTextBlock(translation.LastFuelPrice, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right));

            FuelDataGridContent.Children.Add(GenerateTextBlock(null, user.Vehicles[user.ActiveCarIndex].AverageFuelConsumption.ToString() + " L/100km", 0, 1));
            if (LastRefuelingElement > 0)
            {
                FuelDataGridContent.Children.Add(GenerateTextBlock(null, user.Vehicles[user.ActiveCarIndex].Refulings[LastRefuelingElement - 1].Consumption.ToString() + " L/100km", 1, 1));
                FuelDataGridContent.Children.Add(GenerateTextBlock(null, user.Vehicles[user.ActiveCarIndex].Refulings[LastRefuelingElement - 1].TotalPrice.ToString() + " zł", 2, 1));
            }
            else
            {

            }

            FuelDataGrid.Children.Add(FuelDataGridContent);


            return FuelDataGrid;
        }

        private Grid CostDataGenerator(User user, CostData translation)
        {
            Grid CostDataGrid = new Grid();
            SetGridProps(ref CostDataGrid, 1);

            CostDataGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(114) });
            CostDataGrid.ColumnDefinitions.Add(new ColumnDefinition());

            CostDataGrid.Children.Add(GenerateIcon("../../../Images/Icons/dollar.png", 0, 0));

            Grid CostDataGridContent = new Grid();
            Grid.SetColumn(CostDataGridContent, 1);
            CostDataGridContent.VerticalAlignment = VerticalAlignment.Center;
            CostDataGridContent.HorizontalAlignment = HorizontalAlignment.Center;

            for (int i = 0; i < 2; i++) // 2 columns in this grid
            {
                ColumnDefinition FuelDataGridColumn = new ColumnDefinition();
                CostDataGridContent.ColumnDefinitions.Add(FuelDataGridColumn);
            }
            for (int y = 0; y < 6; y++) // 6 rows in this grid
            {
                RowDefinition FuelDataGridRow = new RowDefinition();
                CostDataGridContent.RowDefinitions.Add(FuelDataGridRow);
            }

            CostDataGridContent.Children.Add(GenerateTextBlock(translation.ThisMounth, PUser.UserLanguage, 0, 0, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
            CostDataGridContent.Children.Add(GenerateTextBlock(translation.ThisMounthFuelCost, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
            CostDataGridContent.Children.Add(GenerateTextBlock(translation.ThisMounthOtherCost, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right));
            CostDataGridContent.Children.Add(GenerateTextBlock(translation.PreviousMounth, PUser.UserLanguage, 3, 0, DarkTextColor, HorizontalAlignment.Right, VerticalAlignment.Center, true));
            CostDataGridContent.Children.Add(GenerateTextBlock(translation.PreviousMounthFuelCost, PUser.UserLanguage, 4, 0, LightTextColor, HorizontalAlignment.Right));
            CostDataGridContent.Children.Add(GenerateTextBlock(translation.PreviousMounthOtherCost, PUser.UserLanguage, 5, 0, LightTextColor, HorizontalAlignment.Right));

            CostDataGridContent.Children.Add(GenerateTextBlock(null, user.Vehicles[user.ActiveCarIndex].ThisMounthFuelCost.ToString() + " zł", 1, 1));
            CostDataGridContent.Children.Add(GenerateTextBlock(null, user.Vehicles[user.ActiveCarIndex].ThisMounthOtherCost.ToString() + " zł", 2, 1));
            CostDataGridContent.Children.Add(GenerateTextBlock(null, user.Vehicles[user.ActiveCarIndex].PreviousMounthFuelCost.ToString() + " zł", 4, 1));
            CostDataGridContent.Children.Add(GenerateTextBlock(null, user.Vehicles[user.ActiveCarIndex].PreviousMounthOtherCost.ToString() + " zł", 5, 1));

            CostDataGrid.Children.Add(CostDataGridContent);

            return CostDataGrid;
        }

        private TextBlock EntriesListText(XMLEntriesList translation)
        {
            TextBlock EntriesListText = GenerateTextBlock(translation.EntriesListText, PUser.UserLanguage, 2, 0, "#FF2A2729", HorizontalAlignment.Center);
            EntriesListText.FontSize = 34;
            EntriesListText.Margin = new Thickness(0,15,0,10);

            return EntriesListText;
        }

        private Grid EnteriesListGenerator (User user, XMLEntriesList translation)
        {
            Grid EnteriesListMainGrid = new Grid();
            SetGridProps(ref EnteriesListMainGrid, 3);

            ScrollViewer DataViewer = new ScrollViewer();

            Grid AuxiliaryGrid = new Grid();

            int index = 0;
            foreach (EntriesList entries in user.Vehicles[user.ActiveCarIndex].EntriesList)
            {
                AuxiliaryGrid.RowDefinitions.Add(new RowDefinition());

                Grid EnteriesListGrid = new Grid();
                SetGridProps(ref EnteriesListGrid, index);
                EnteriesListGrid.Margin = new Thickness(70,10,70,10);

                for (int i = 0; i < 2; i++) // 2 is number of columns
                {
                    ColumnDefinition EnteriesListGridColumn = new ColumnDefinition();
                    EnteriesListGrid.ColumnDefinitions.Add(EnteriesListGridColumn);
                }
                for (int x = 0; x < 4; x++) // 4 is number of rows
                {
                    RowDefinition EnteriesListGridRow = new RowDefinition();
                    EnteriesListGrid.RowDefinitions.Add(EnteriesListGridRow);
                }

                string LightTextColor = "#FF9C9397";
                string DarkTextColor = "#FF2A2729"; // change to set in config

                EnteriesListGrid.Children.Add(GenerateTextBlock(translation.Date, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
                EnteriesListGrid.Children.Add(GenerateTextBlock(translation.Price, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right));
                EnteriesListGrid.Children.Add(GenerateTextBlock(translation.Descryption, PUser.UserLanguage, 3, 0, LightTextColor, HorizontalAlignment.Right));

                EnteriesListGrid.Children.Add(GenerateTextBlock(null, entries.Type.ToString(), 0, 0, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
                EnteriesListGrid.Children.Add(GenerateTextBlock(null, entries.Date.ToString(), 1, 2));
                EnteriesListGrid.Children.Add(GenerateTextBlock(null, entries.Price.ToString() + " Zł", 2, 2));
                EnteriesListGrid.Children.Add(GenerateTextBlock(null, entries.Descryption.ToString(), 3, 2));

                AuxiliaryGrid.Children.Add(EnteriesListGrid);
                index++;
            }

            DataViewer.Content = AuxiliaryGrid;
            EnteriesListMainGrid.Children.Add(DataViewer);

            return EnteriesListMainGrid;
        }

    }
}
