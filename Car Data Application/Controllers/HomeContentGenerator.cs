using Car_Data_Application.Models;
using Car_Data_Application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Car_Data_Application.Models.Vehicle_Classes;

namespace Car_Data_Application.Controllers
{
    class HomeContentGenerator
    {
        public void GeneratorHomeContent(MainWindow mv, User user)
        {
            Grid Grid = new Grid();
            for (int i = 0; i < 3; i++) // 3 is number of displays blocks with data
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(120);
                Grid.RowDefinitions.Add(row);
            }

            Grid.Children.Add(FuelDataGenerator(user));
            Grid.Children.Add(CostDataGenerator(user));
            Grid.Children.Add(EnteriesListGenerator(user));

            mv.ScrollViewerContent.Content = Grid;
        }

        public Grid FuelDataGenerator(User user)
        {
            Grid FuelDataGrid = new Grid();
            FuelDataGrid.Margin = new Thickness(10);
            FuelDataGrid.Background = Brushes.LightGray;
            Grid.SetRow(FuelDataGrid, 0);

            for (int i = 0; i < 3; i++) // 3 columns in this grid
            {
                ColumnDefinition FuelDataGridColumn = new ColumnDefinition();
                FuelDataGrid.ColumnDefinitions.Add(FuelDataGridColumn);
            }

            for (int y = 0; y < 4; y++) // 4 rows in this grid
            {
                RowDefinition FuelDataGridRow = new RowDefinition();
                FuelDataGrid.RowDefinitions.Add(FuelDataGridRow);
            }

            int LastRefuelingElement = user.Vehicles[user.ActiveCarIndex].Refulings.Count();

            Image FuelIcon = new Image();
            ImageSourceConverter source = new ImageSourceConverter();
            FuelIcon.SetValue(Image.SourceProperty, source.ConvertFromString(@"../../../Images/Icons/fuelicon.png"));
            Grid.SetRow(FuelIcon, 0);
            Grid.SetColumn(FuelIcon, 1);
            FuelDataGrid.Children.Add(FuelIcon);

            TextBlock AvrageFuelConsumption = new TextBlock();
            AvrageFuelConsumption.Text = "Średnie spalanie:";
            Grid.SetRow(AvrageFuelConsumption, 1);
            Grid.SetColumn(AvrageFuelConsumption, 0);
            FuelDataGrid.Children.Add(AvrageFuelConsumption);

            TextBlock AvrageFuelConsumptionValue = new TextBlock();
            AvrageFuelConsumptionValue.Text = user.Vehicles[user.ActiveCarIndex].AverageFuelConsumption.ToString() + " L/100km";
            Grid.SetRow(AvrageFuelConsumptionValue, 1);
            Grid.SetColumn(AvrageFuelConsumptionValue, 2);
            FuelDataGrid.Children.Add(AvrageFuelConsumptionValue);

            TextBlock LatestConsumption = new TextBlock();
            LatestConsumption.Text = "Ostatnie spalanie:";
            Grid.SetRow(LatestConsumption, 2);
            Grid.SetColumn(LatestConsumption, 0);
            FuelDataGrid.Children.Add(LatestConsumption);

            TextBlock LatestConsumptionValue = new TextBlock();
            LatestConsumptionValue.Text = user.Vehicles[user.ActiveCarIndex].Refulings[LastRefuelingElement - 1].LatestConsumption.ToString() + " L/100km";
            Grid.SetRow(LatestConsumptionValue, 2);
            Grid.SetColumn(LatestConsumptionValue, 2);
            FuelDataGrid.Children.Add(LatestConsumptionValue);

            TextBlock LatestFuelPrice = new TextBlock();
            LatestFuelPrice.Text = "Ostatnia cena paliwa:";
            Grid.SetRow(LatestFuelPrice, 3);
            Grid.SetColumn(LatestFuelPrice, 0);
            FuelDataGrid.Children.Add(LatestFuelPrice);

            TextBlock LatestFuelPriceValue = new TextBlock();
            LatestFuelPriceValue.Text = user.Vehicles[user.ActiveCarIndex].Refulings[LastRefuelingElement - 1].LatestFuelPrice.ToString() + " zł";
            Grid.SetRow(LatestFuelPriceValue, 3);
            Grid.SetColumn(LatestFuelPriceValue, 2);
            FuelDataGrid.Children.Add(LatestFuelPriceValue);

            return FuelDataGrid;
        }

        public Grid CostDataGenerator(User user)
        {
            Grid CostDataGrid = new Grid();
            CostDataGrid.Margin = new Thickness(10);
            CostDataGrid.Background = Brushes.LightGray;
            Grid.SetRow(CostDataGrid, 1);

            for (int i = 0; i < 3; i++) // 3 columns in this grid
            {
                ColumnDefinition FuelDataGridColumn = new ColumnDefinition();
                CostDataGrid.ColumnDefinitions.Add(FuelDataGridColumn);
            }

            for (int y = 0; y < 7; y++) // 7 rows in this grid
            {
                RowDefinition FuelDataGridRow = new RowDefinition();
                CostDataGrid.RowDefinitions.Add(FuelDataGridRow);
            }

            Image CostIcon = new Image();
            ImageSourceConverter source = new ImageSourceConverter();
            CostIcon.SetValue(Image.SourceProperty, source.ConvertFromString(@"../../../Images/Icons/cost.png"));
            Grid.SetRow(CostIcon, 0);
            Grid.SetColumn(CostIcon, 1);
            CostDataGrid.Children.Add(CostIcon);

            TextBlock ThisMounthText = new TextBlock();
            ThisMounthText.Text = "Ten miesiąc:";
            Grid.SetRow(ThisMounthText, 1);
            Grid.SetColumn(ThisMounthText, 0);
            CostDataGrid.Children.Add(ThisMounthText);

            TextBlock FuelCostValue = new TextBlock();
            FuelCostValue.Text = user.Vehicles[user.ActiveCarIndex].ThisMounthFuelCost.ToString() + " zł"; 
            Grid.SetRow(FuelCostValue, 2);
            Grid.SetColumn(FuelCostValue, 1);
            CostDataGrid.Children.Add(FuelCostValue);

            TextBlock FuelText = new TextBlock();
            FuelText.Text = "Paliwo";
            Grid.SetRow(FuelText, 2);
            Grid.SetColumn(FuelText, 2);
            CostDataGrid.Children.Add(FuelText);

            TextBlock ThisMounthOtherCostValue = new TextBlock();
            ThisMounthOtherCostValue.Text = user.Vehicles[user.ActiveCarIndex].ThisMounthOtherCost.ToString() + " zł";
            Grid.SetRow(ThisMounthOtherCostValue, 3);
            Grid.SetColumn(ThisMounthOtherCostValue, 1);
            CostDataGrid.Children.Add(ThisMounthOtherCostValue);

            TextBlock OtherCostText = new TextBlock();
            OtherCostText.Text = "Pozostałe koszta";
            Grid.SetRow(OtherCostText, 3);
            Grid.SetColumn(OtherCostText, 2);
            CostDataGrid.Children.Add(OtherCostText);

            TextBlock PreviousMonthText = new TextBlock();
            PreviousMonthText.Text = "Poprzedni miesiąc";
            Grid.SetRow(PreviousMonthText, 4);
            Grid.SetColumn(PreviousMonthText, 0);
            CostDataGrid.Children.Add(PreviousMonthText);

            TextBlock PreviousMonthFuelCostValue = new TextBlock();
            PreviousMonthFuelCostValue.Text = user.Vehicles[user.ActiveCarIndex].PreviousMounthFuelCost.ToString() + " zł";
            Grid.SetRow(PreviousMonthFuelCostValue, 5);
            Grid.SetColumn(PreviousMonthFuelCostValue, 1);
            CostDataGrid.Children.Add(PreviousMonthFuelCostValue);

            TextBlock PreviousMonthFuelCostText = new TextBlock();
            PreviousMonthFuelCostText.Text = "Paliwo";
            Grid.SetRow(PreviousMonthFuelCostText, 5);
            Grid.SetColumn(PreviousMonthFuelCostText, 2);
            CostDataGrid.Children.Add(PreviousMonthFuelCostText);

            TextBlock PreviousMonthOtherCostValue = new TextBlock();
            PreviousMonthOtherCostValue.Text = user.Vehicles[user.ActiveCarIndex].PreviousMounthOtherCost.ToString() + " zł";
            Grid.SetRow(PreviousMonthOtherCostValue, 6);
            Grid.SetColumn(PreviousMonthOtherCostValue, 1);
            CostDataGrid.Children.Add(PreviousMonthOtherCostValue);

            TextBlock PreviousMonthOtherCostText = new TextBlock();
            PreviousMonthOtherCostText.Text = "Pozostałe koszta";
            Grid.SetRow(PreviousMonthOtherCostText, 6);
            Grid.SetColumn(PreviousMonthOtherCostText, 2);
            CostDataGrid.Children.Add(PreviousMonthOtherCostText);

            return CostDataGrid;
        }

        public ScrollViewer EnteriesListGenerator (User user)
        {
            ScrollViewer DataViewer = new ScrollViewer();
            Grid.SetRow(DataViewer, 2);

            foreach (EntriesList entries in user.Vehicles[user.ActiveCarIndex].EntriesList)
            {
                Grid DataGrid = new Grid();
                DataGrid.Margin = new Thickness(10);
                DataGrid.Background = Brushes.LightGray;
                for (int i = 0; i < 3; i++) // 3 is number of columns
                {
                    ColumnDefinition DataGridColumn = new ColumnDefinition();
                    DataGrid.ColumnDefinitions.Add(DataGridColumn);
                }
                for (int x = 0; x < 4; x++) // 4 is number of rows
                {
                    RowDefinition DataGridRow = new RowDefinition();
                    DataGrid.RowDefinitions.Add(DataGridRow);
                }
                DataViewer.Content = DataGrid;

                TextBlock TypeValue = new TextBlock();
                TypeValue.Text = entries.Type.ToString();
                Grid.SetRow(TypeValue, 0);
                Grid.SetColumn(TypeValue, 1);
                DataGrid.Children.Add(TypeValue);

                TextBlock DataText = new TextBlock();
                DataText.Text = "Data:";
                Grid.SetRow(DataText, 1);
                Grid.SetColumn(DataText, 0);
                DataGrid.Children.Add(DataText);

                TextBlock DataValue = new TextBlock();
                DataValue.Text = entries.Date.ToString();
                Grid.SetRow(DataValue, 1);
                Grid.SetColumn(DataValue, 2);
                DataGrid.Children.Add(DataValue);

                TextBlock CostText = new TextBlock();
                CostText.Text = "Koszt:";
                Grid.SetRow(CostText, 2);
                Grid.SetColumn(CostText, 0);
                DataGrid.Children.Add(CostText);

                TextBlock CostTextValue = new TextBlock();
                CostTextValue.Text = entries.Price.ToString() + " zł";
                Grid.SetRow(CostTextValue, 2);
                Grid.SetColumn(CostTextValue, 2);
                DataGrid.Children.Add(CostTextValue);

                TextBlock DescryptionText = new TextBlock();
                DescryptionText.Text = "Opis:";
                Grid.SetRow(DescryptionText, 3);
                Grid.SetColumn(DescryptionText, 0);
                DataGrid.Children.Add(DescryptionText);

                TextBlock DescryptionValue = new TextBlock();
                DescryptionValue.Text = entries.Descryption.ToString();
                Grid.SetRow(DescryptionValue, 3);
                Grid.SetColumn(DescryptionValue, 2);
                DataGrid.Children.Add(DescryptionValue);

            }

            
            

            return DataViewer;
        }
    }
}
