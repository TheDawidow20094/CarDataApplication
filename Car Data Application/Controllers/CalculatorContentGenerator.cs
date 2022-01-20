using Car_Data_Application.Models;
using Car_Data_Application.Models.Vehicle_Classes;
using Car_Data_Application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class CalculatorContentGenerator
    {
        private MainWindow mainWindow;
        private User PUser;

        private ComboBox CalculatorTypeSelector = new ComboBox();
        private Grid Grid = new Grid();
        private TextBlock ResoultPriceValue = new TextBlock();
        private TextBox DistanceValue = new TextBox();
        private TextBox PriceForOneFuelUnitValue = new TextBox();
        private TextBox FuelConsumptionValue = new TextBox();

        public void CalculatorGenerator(MainWindow mw, User user)
        {
            mainWindow = mw;
            PUser = user;
            mainWindow.WhereAreYou = "CalculatorPage";
            SetButtonColor();

            for (int i = 0; i < 2; i++) // 2 is number of displays blocks with data
            {
                RowDefinition GridRow = new RowDefinition();
                Grid.RowDefinitions.Add(GridRow);
            }
            Grid.Children.Add(MainContentGenerator());
            Grid.Children.Add(TravelCostCalculatorGenerator());

            mainWindow.ScrollViewerContent.Content = Grid;
        }

        public void SetButtonColor()
        {
            BrushConverter bc = new BrushConverter();
            mainWindow.HomePageButton.Background = Brushes.White;
            mainWindow.LoginPageButton.Background = Brushes.White;
            mainWindow.CarPageButton.Background = Brushes.White;
            mainWindow.RefuelingHistoryPageButton.Background = Brushes.White;
            mainWindow.StatsPageButton.Background = Brushes.White;
            mainWindow.CostPageButton.Background = Brushes.White;
            mainWindow.BackupPageButton.Background = Brushes.White;
            mainWindow.SetingPaneButton.Background = Brushes.White;
            mainWindow.CalculatorPageButton.Background = (Brush)bc.ConvertFrom("#07EDE9");
        }

        private void HandleChangeCalculatorType(object sender, SelectionChangedEventArgs e)
        {
            switch (CalculatorTypeSelector.SelectedItem)
            {
                case "Koszt podróży":
                    Grid.Children.Add(TravelCostCalculatorGenerator());
                break;
            }
        }

        private Grid MainContentGenerator()
        {
            Grid MainContentGrid = new Grid();
            MainContentGrid.Margin = new Thickness(10);
            MainContentGrid.Background = Brushes.LightGray;
            Grid.SetRow(MainContentGrid, 0);

            for (int i = 0; i < 2; i++) // 2 is number of rows
            {
                RowDefinition MainContentGridRow = new RowDefinition();
                MainContentGridRow.Height = new GridLength(45);
                MainContentGrid.RowDefinitions.Add(MainContentGridRow);
            }
            for (int y = 0; y < 2; y++) // 2 is number of column
            {
                ColumnDefinition MainContentColumnDefinition = new ColumnDefinition();
                MainContentGrid.ColumnDefinitions.Add(MainContentColumnDefinition);
            }


            CalculatorTypeSelector.Height = 35;
            CalculatorTypeSelector.Width = 200;
            CalculatorTypeSelector.HorizontalContentAlignment = HorizontalAlignment.Center;
            CalculatorTypeSelector.VerticalContentAlignment = VerticalAlignment.Center;
            CalculatorTypeSelector.Items.Add("Koszt podróży");
            CalculatorTypeSelector.SelectedItem = "Koszt podróży";
            CalculatorTypeSelector.SelectionChanged += HandleChangeCalculatorType;
            Grid.SetRow(CalculatorTypeSelector, 0);
            Grid.SetColumn(CalculatorTypeSelector, 0);
            MainContentGrid.Children.Add(CalculatorTypeSelector);

            ComboBox FuelTypeSelector = new ComboBox();
            FuelTypeSelector.Height = 35;
            FuelTypeSelector.Width = 200;
            FuelTypeSelector.HorizontalContentAlignment = HorizontalAlignment.Center;
            FuelTypeSelector.VerticalContentAlignment = VerticalAlignment.Center;
            if (PUser.Vehicles.Count > 0)
            {
                if (PUser.Vehicles[PUser.ActiveCarIndex].FuelType.Diesel == true)
                {
                    FuelTypeSelector.Items.Add("Diesel");
                    FuelTypeSelector.SelectedItem = "Diesel";
                }
                if (PUser.Vehicles[PUser.ActiveCarIndex].FuelType.Gasoline == true)
                {
                    FuelTypeSelector.Items.Add("Benzyna");
                    FuelTypeSelector.SelectedItem = "Benzyna";
                }
                if (PUser.Vehicles[PUser.ActiveCarIndex].FuelType.LPG == true)
                {
                    FuelTypeSelector.Items.Add("LPG");
                }
            }
            else
            {
                FuelTypeSelector.Items.Add("Brak aut w bazie");
                FuelTypeSelector.SelectedItem = "Brak aut w bazie";
                FuelTypeSelector.IsEnabled = false;
            }
            Grid.SetRow(FuelTypeSelector, 0);
            Grid.SetColumn(FuelTypeSelector, 1);
            MainContentGrid.Children.Add(FuelTypeSelector);

            return MainContentGrid;
        }

        private Grid TravelCostCalculatorGenerator()
        {
            Grid TravelCostCalculatorGrid = new Grid();
            TravelCostCalculatorGrid.Margin = new Thickness(10);
            TravelCostCalculatorGrid.Background = Brushes.LightGray;
            Grid.SetRow(TravelCostCalculatorGrid, 1);
            for (int i = 0; i < 3; i++) // 3 columns in this grid
            {
                ColumnDefinition TravelCostCalculatorGridColumn = new ColumnDefinition();
                TravelCostCalculatorGrid.ColumnDefinitions.Add(TravelCostCalculatorGridColumn);
            }
            for (int y = 0; y < 6; y++) // 6 rows in this grid
            {
                RowDefinition TravelCostCalculatorGridRow = new RowDefinition();
                TravelCostCalculatorGrid.RowDefinitions.Add(TravelCostCalculatorGridRow);
            }


            TextBlock DistanceText = new TextBlock();
            DistanceText.Text = "Planowana odległość:";
            Grid.SetRow(DistanceText, 0);
            Grid.SetColumn(DistanceText, 0);
            TravelCostCalculatorGrid.Children.Add(DistanceText);

            DistanceValue.Height = 35;
            DistanceValue.Width = 250;
            Grid.SetRow(DistanceValue, 0);
            Grid.SetColumn(DistanceValue, 2);
            TravelCostCalculatorGrid.Children.Add(DistanceValue);

            TextBlock PriceForOneFuelUnitText = new TextBlock();
            PriceForOneFuelUnitText.Text = "Cena za litr";
            Grid.SetRow(PriceForOneFuelUnitText, 1);
            Grid.SetColumn(PriceForOneFuelUnitText, 0);
            TravelCostCalculatorGrid.Children.Add(PriceForOneFuelUnitText);

            PriceForOneFuelUnitValue.Height = 35;
            PriceForOneFuelUnitValue.Width = 250;
            try { PriceForOneFuelUnitValue.Text = PUser.Vehicles[PUser.ActiveCarIndex].Refulings[PUser.ActiveCarIndex].LatestFuelPrice.ToString(); } catch { }
            Grid.SetRow(PriceForOneFuelUnitValue, 1);
            Grid.SetColumn(PriceForOneFuelUnitValue, 2);
            TravelCostCalculatorGrid.Children.Add(PriceForOneFuelUnitValue);

            TextBlock FuelConsumptionText = new TextBlock();
            FuelConsumptionText.Text = "Spalanie";
            Grid.SetRow(FuelConsumptionText, 2);
            Grid.SetColumn(FuelConsumptionText, 0);
            TravelCostCalculatorGrid.Children.Add(FuelConsumptionText);

            FuelConsumptionValue.Height = 35;
            FuelConsumptionValue.Width = 250;
            try { FuelConsumptionValue.Text = PUser.Vehicles[PUser.ActiveCarIndex].AverageFuelConsumption.ToString(); } catch { }
            Grid.SetRow(FuelConsumptionValue, 2);
            Grid.SetColumn(FuelConsumptionValue, 2);
            TravelCostCalculatorGrid.Children.Add(FuelConsumptionValue);

            TextBlock ResoultText = new TextBlock();
            ResoultText.Text = "Wynik";
            Grid.SetRow(ResoultText, 3);
            Grid.SetColumn(ResoultText, 0);
            TravelCostCalculatorGrid.Children.Add(ResoultText);

            Grid.SetRow(ResoultPriceValue, 3);
            Grid.SetColumn(ResoultPriceValue, 2);
            TravelCostCalculatorGrid.Children.Add(ResoultPriceValue);

            Button CalculateButton = new Button();
            CalculateButton.Content = "Oblicz";
            CalculateButton.Height = 60;
            CalculateButton.Width = 100;
            CalculateButton.Click += TravelCostCalculateButton_Click;
            Grid.SetRow(CalculateButton, 5);
            Grid.SetColumn(CalculateButton, 1);
            TravelCostCalculatorGrid.Children.Add(CalculateButton);

            return TravelCostCalculatorGrid;
        }

        private void TravelCostCalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double Distance = Convert.ToDouble(DistanceValue.Text);
                double PriceForOneUnit = Convert.ToDouble(PriceForOneFuelUnitValue.Text);
                double FuelConsumption = Convert.ToDouble(FuelConsumptionValue.Text);
                double Resoult = (Distance * FuelConsumption / 100) * PriceForOneUnit;
                ResoultPriceValue.Text = Resoult + " zł".ToString();
            }
            catch (Exception ex) { MessageBox.Show("Krytyczny błąd w trakcie boliczeń " + ex); }
        }
    }
}
