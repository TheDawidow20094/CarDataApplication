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
        private TextBox PriceForOneFuelUnitValueOptional = new TextBox();
        private TextBox FuelConsumptionValue = new TextBox();
        private TextBlock ResoultUsedFuel = new TextBlock();
        private TextBox ConsumedFuelValue = new TextBox();
        private TextBox NumberOfKilometersTraveledValue = new TextBox();

        private Grid AverageFuelConsumptionCalculatorGrid = new Grid();
        private Grid TravelCostCalculatorGrid = new Grid();

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

            mainWindow.ScrollViewerContent.Content = Grid;
        }

        private void SetButtonColor()
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
                    //RefreshPage();
                    Grid.Children.Remove(AverageFuelConsumptionCalculatorGrid);
                    Grid.Children.Add(TravelCostCalculatorGenerator());
                break;

                case "Średnie spalanie":
                    Grid.Children.Remove(TravelCostCalculatorGrid);
                    //RefreshPage();
                    Grid.Children.Add(AverageFuelConsumptionCalculatorGenerator());
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
            CalculatorTypeSelector.Items.Add("Średnie spalanie");
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

            Grid.SetRow(ResoultUsedFuel, 4);
            Grid.SetColumn(ResoultUsedFuel, 2);
            TravelCostCalculatorGrid.Children.Add(ResoultUsedFuel);

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
                double UsedFuel = (Distance / FuelConsumption);
                ResoultPriceValue.Text = Resoult + " zł".ToString();
                ResoultUsedFuel.Text = UsedFuel + " litrów";
            }
            catch (Exception ex) { MessageBox.Show("Krytyczny błąd w trakcie boliczeń " + ex); }
        }

        private Grid AverageFuelConsumptionCalculatorGenerator()
        {
            AverageFuelConsumptionCalculatorGrid.Margin = new Thickness(10);
            AverageFuelConsumptionCalculatorGrid.Background = Brushes.LightGray;
            Grid.SetRow(AverageFuelConsumptionCalculatorGrid, 1);
            for (int i = 0; i < 3; i++) // 3 columns in this grid
            {
                ColumnDefinition AverageFuelConsumptionCalculatorGridColumn = new ColumnDefinition();
                AverageFuelConsumptionCalculatorGrid.ColumnDefinitions.Add(AverageFuelConsumptionCalculatorGridColumn);
            }
            for (int y = 0; y < 4; y++) // 4 rows in this grid
            {
                RowDefinition AverageFuelConsumptionCalculatorGridRow = new RowDefinition();
                AverageFuelConsumptionCalculatorGrid.RowDefinitions.Add(AverageFuelConsumptionCalculatorGridRow);
            }

            TextBlock ConsumedFuelText = new TextBlock();
            ConsumedFuelText.Text = "Spalone paliwo:";
            Grid.SetRow(ConsumedFuelText, 0);
            Grid.SetColumn(ConsumedFuelText, 0);
            AverageFuelConsumptionCalculatorGrid.Children.Add(ConsumedFuelText);

            ConsumedFuelValue.Height = 35;
            ConsumedFuelValue.Width = 250;
            Grid.SetRow(ConsumedFuelValue, 0);
            Grid.SetColumn(ConsumedFuelValue, 2);
            AverageFuelConsumptionCalculatorGrid.Children.Add(ConsumedFuelValue);

            TextBlock NumberOfKilometersTraveledText = new TextBlock();
            NumberOfKilometersTraveledText.Text = "Ilość przejechanych kilometrów:";
            Grid.SetRow(NumberOfKilometersTraveledText, 1);
            Grid.SetColumn(NumberOfKilometersTraveledText, 0);
            AverageFuelConsumptionCalculatorGrid.Children.Add(NumberOfKilometersTraveledText);

            NumberOfKilometersTraveledValue.Height = 35;
            NumberOfKilometersTraveledValue.Width = 250;
            Grid.SetRow(NumberOfKilometersTraveledValue, 1);
            Grid.SetColumn(NumberOfKilometersTraveledValue, 2);
            AverageFuelConsumptionCalculatorGrid.Children.Add(NumberOfKilometersTraveledValue);

            TextBlock PriceForOneFuelUnitText = new TextBlock();
            PriceForOneFuelUnitText.Text = "Cena za litr (opcjonalnie):";
            Grid.SetRow(PriceForOneFuelUnitText, 2);
            Grid.SetColumn(PriceForOneFuelUnitText, 0);
            AverageFuelConsumptionCalculatorGrid.Children.Add(PriceForOneFuelUnitText);

            PriceForOneFuelUnitValueOptional.Height = 35;
            PriceForOneFuelUnitValueOptional.Width = 250;
            Grid.SetRow(PriceForOneFuelUnitValueOptional, 2);
            Grid.SetColumn(PriceForOneFuelUnitValueOptional, 2);
            AverageFuelConsumptionCalculatorGrid.Children.Add(PriceForOneFuelUnitValueOptional);

            TextBlock ResoultText = new TextBlock();
            ResoultText.Text = "Wynik:";
            Grid.SetRow(ResoultText, 3);
            Grid.SetColumn(ResoultText, 0);
            AverageFuelConsumptionCalculatorGrid.Children.Add(ResoultText);

            ResoultUsedFuel.Height = 35;
            ResoultUsedFuel.Width = 250;
            Grid.SetRow(ResoultUsedFuel, 3);
            Grid.SetColumn(ResoultUsedFuel, 2);
            AverageFuelConsumptionCalculatorGrid.Children.Add(ResoultUsedFuel);

            Button CalculateButton = new Button();
            CalculateButton.Content = "Oblicz";
            CalculateButton.Height = 60;
            CalculateButton.Width = 100;
            CalculateButton.Click += AverageFuelConsumptionCalculatorButton_Click;
            Grid.SetRow(CalculateButton, 4);
            Grid.SetColumn(CalculateButton, 1);
            AverageFuelConsumptionCalculatorGrid.Children.Add(CalculateButton);

            return AverageFuelConsumptionCalculatorGrid;
        }

        private void AverageFuelConsumptionCalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PriceForOneFuelUnitValueOptional != null)
                {
                    double ConsumedFuel = Convert.ToDouble(ConsumedFuelValue.Text);
                    double NumberOfKilometersTraveled = Convert.ToDouble(NumberOfKilometersTraveledValue);
                    double PriceForOneFuelUnitOptional = Convert.ToDouble(PriceForOneFuelUnitValueOptional);
                    double FuelResult = ((ConsumedFuel / NumberOfKilometersTraveled) * 100);
                    double CostResult = FuelResult * PriceForOneFuelUnitOptional;

                    ResoultUsedFuel.Text = "Spalanie wynosi " + FuelResult.ToString() + " litrów/100km" + " Koszt: " + CostResult.ToString();
                }
                else
                {
                    double ConsumedFuel = Convert.ToDouble(ConsumedFuelValue.Text);
                    double NumberOfKilometersTraveled = Convert.ToDouble(NumberOfKilometersTraveledValue);
                    double Result = ((ConsumedFuel / NumberOfKilometersTraveled) * 100);

                    ResoultUsedFuel.Text = "Spalanie wyniosło " + Result.ToString() + " litrów na 100/km";
                }
            }
            catch (Exception ex) { MessageBox.Show("Krytyczny błąd w trakcie boliczeń " + ex); }
        }

        private void RefreshPage()
        {
            CalculatorContentGenerator RefreshPage = new CalculatorContentGenerator();
            RefreshPage.CalculatorGenerator(mainWindow, PUser);
        }
    }
}
