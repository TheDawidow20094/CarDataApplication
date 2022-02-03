using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class CalculatorContentGenerator : CarDataAppController
    {
        #region SetPrivateVariable

        private ComboBox CalculatorTypeSelector = new ComboBox();
        private Grid Grid = new Grid();
        private Grid CalculatorGrid = new Grid();
        private TextBlock ResoultPriceValue = new TextBlock();
        private TextBox DistanceValue = new TextBox();
        private TextBox PriceForOneFuelUnitValue = new TextBox();
        private TextBox PriceForOneFuelUnitValueOptional = new TextBox();
        private TextBox FuelConsumptionValue = new TextBox();
        private TextBlock ResoultUsedFuel = new TextBlock();
        private TextBlock ResoultUsedFuelAverageFuelCOnsumption = new TextBlock();
        private TextBox ConsumedFuelValue = new TextBox();
        private TextBox NumberOfKilometersTraveledValue = new TextBox();

        #endregion

        public void CalculatorGenerator(MainWindow mw, User user, MainGrid config)
        {
            InitialAssignValue(mw, user, config);

            for (int i = 0; i < 3; i++) // 3 is number of displays blocks with data
            {
                RowDefinition GridRow = new RowDefinition();
                Grid.RowDefinitions.Add(GridRow);
            }
            Grid.Children.Add(MainContentGenerator(Config.MainPanel.CalculatorPage));

            CalculatorGrid.Children.Add(TravelCostCalculatorGenerator(Config.MainPanel.CalculatorPage.TravelCostCalculatorBorder));
            Grid.SetRow(CalculatorGrid, 1);
            Grid.Children.Add(CalculatorGrid);

            mainWindow.ScrollViewerContent.Content = Grid;
        }

        private void InitialAssignValue(MainWindow mw, User user, MainGrid config)
        {
            Config = config;
            mainWindow = mw;
            PUser = user;
            mainWindow.WhereAreYou = "CalculatorPage";

            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.MainGrid.Children[3]));
        }

        private void HandleChangeCalculatorType(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            switch (combobox.SelectedIndex)
            {
                case 0:
                    CalculatorGrid.Children.Clear();
                    CalculatorGrid.Children.Add(TravelCostCalculatorGenerator(Config.MainPanel.CalculatorPage.TravelCostCalculatorBorder));
                break;

                case 1:
                    CalculatorGrid.Children.Clear();
                    CalculatorGrid.Children.Add(AverageFuelConsumptionCalculatorGenerator(Config.MainPanel.CalculatorPage.AverageFuelConsumptionCalculatorBorder));
                break;
            }
        }

        private Border MainContentGenerator(CalculatorPage translation)
        {
            Border MainContentBorder = new Border();
            SetBorderProps(ref MainContentBorder, 0);

            Grid MainContentGrid = new Grid();
            MainContentBorder.Padding = new Thickness(20);
            MainContentBorder.Child = MainContentGrid;
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
            CalculatorTypeSelector.Items.Add(PUser.UserLanguage == "PL" ? translation.TravelCostCalculator.PL : translation.TravelCostCalculator.ENG);
            CalculatorTypeSelector.Items.Add(PUser.UserLanguage == "PL" ? translation.AverageFuelConsumptionCalculator.PL : translation.AverageFuelConsumptionCalculator.ENG);
            CalculatorTypeSelector.SelectedItem = (PUser.UserLanguage == "PL" ? translation.TravelCostCalculator.PL : translation.TravelCostCalculator.ENG);
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
                    FuelTypeSelector.Items.Add(PUser.UserLanguage == "PL" ? translation.Diesel.PL : translation.Diesel.ENG);
                    FuelTypeSelector.SelectedItem = (PUser.UserLanguage == "PL" ? translation.Diesel.PL : translation.Diesel.ENG);
                }
                if (PUser.Vehicles[PUser.ActiveCarIndex].FuelType.Gasoline == true)
                {
                    FuelTypeSelector.Items.Add(PUser.UserLanguage == "PL" ? translation.Gasoline.PL : translation.Gasoline.ENG);
                    FuelTypeSelector.SelectedItem = ((PUser.UserLanguage == "PL" ? translation.Gasoline.PL : translation.Gasoline.ENG));
                }
                if (PUser.Vehicles[PUser.ActiveCarIndex].FuelType.LPG == true)
                {
                    FuelTypeSelector.Items.Add(PUser.UserLanguage == "PL" ? translation.LPG.PL : translation.LPG.ENG);
                }
            }
            else
            {
                FuelTypeSelector.Items.Add(PUser.UserLanguage == "PL" ? translation.NoVehicleException.PL : translation.NoVehicleException.ENG);
                FuelTypeSelector.SelectedItem = (PUser.UserLanguage == "PL" ? translation.NoVehicleException.PL : translation.NoVehicleException.ENG);
                FuelTypeSelector.IsEnabled = false;
            }
            Grid.SetRow(FuelTypeSelector, 0);
            Grid.SetColumn(FuelTypeSelector, 1);
            MainContentGrid.Children.Add(FuelTypeSelector);

            return MainContentBorder;
        }

        private Border TravelCostCalculatorGenerator(TravelCostCalculatorBorder translation)
        {
            Border TravelCostCalculatorBorder = new Border();
            SetBorderProps(ref TravelCostCalculatorBorder, 0);

            Grid TravelCostCalculatorGrid = new Grid();
            TravelCostCalculatorBorder.Padding = new Thickness(20);
            TravelCostCalculatorBorder.Child = TravelCostCalculatorGrid;
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

            Button CalculateButton = new Button();
            CalculateButton.Height = 60;
            CalculateButton.Width = 100;
            CalculateButton.Click += TravelCostCalculateButton_Click;
            CalculateButton.Background = (Brush)Converter.ConvertFromString("#FF1065B9");
            CalculateButton.Foreground = Brushes.White;
            CalculateButton.FontFamily = new FontFamily("Arial Black");
            CalculateButton.FontWeight = FontWeights.Bold;
            CalculateButton.Margin = new Thickness(0, 0, 0, 8);
            Grid.SetRow(CalculateButton, 5);
            Grid.SetColumn(CalculateButton, 1);
            TravelCostCalculatorGrid.Children.Add(CalculateButton);

            switch (PUser.UserLanguage)
            {
                case "PL":
                    TravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.Distance.PL, 0, 0));
                    TravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.PriceForLiter.PL, 1, 0));
                    TravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.Consumption.PL, 2, 0));
                    TravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.Resoult.PL, 3, 0));
                    CalculateButton.Content = translation.CalculateButton.PL;
                    break;
                case "ENG":
                    TravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.Distance.ENG, 0, 0));
                    TravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.PriceForLiter.ENG, 1, 0));
                    TravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.Consumption.ENG, 2, 0));
                    TravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.Resoult.ENG, 3, 0));
                    CalculateButton.Content = translation.CalculateButton.ENG;
                    break;
            }

            DistanceValue = new TextBox();
            DistanceValue.Height = 35;
            DistanceValue.Width = 250;
            Grid.SetRow(DistanceValue, 0);
            Grid.SetColumn(DistanceValue, 2);
            DistanceValue.MouseLeave += DistanceValueTryConvert;
            DistanceValue.Background = (Brush)Converter.ConvertFromString("#FF1065B9");
            DistanceValue.Foreground = Brushes.White;
            DistanceValue.FontFamily = new FontFamily("Arial Black");
            DistanceValue.FontSize = 14;
            DistanceValue.FontWeight = FontWeights.Bold;
            TravelCostCalculatorGrid.Children.Add(DistanceValue);

            PriceForOneFuelUnitValue = new TextBox();
            PriceForOneFuelUnitValue.Height = 35;
            PriceForOneFuelUnitValue.Width = 250;
            try { PriceForOneFuelUnitValue.Text = PUser.Vehicles[PUser.ActiveCarIndex].Refulings[PUser.ActiveCarIndex].LatestFuelPrice.ToString(); } catch { }
            Grid.SetRow(PriceForOneFuelUnitValue, 1);
            Grid.SetColumn(PriceForOneFuelUnitValue, 2);
            PriceForOneFuelUnitValue.MouseLeave += PriceForOneFuelUnitValueTryConvert;
            PriceForOneFuelUnitValue.Background = (Brush)Converter.ConvertFromString("#FF1065B9");
            PriceForOneFuelUnitValue.Foreground = Brushes.White;
            PriceForOneFuelUnitValue.FontFamily = new FontFamily("Arial Black");
            PriceForOneFuelUnitValue.FontSize = 14;
            PriceForOneFuelUnitValue.FontWeight = FontWeights.Bold;
            TravelCostCalculatorGrid.Children.Add(PriceForOneFuelUnitValue);

            FuelConsumptionValue = new TextBox();
            FuelConsumptionValue.Height = 35;
            FuelConsumptionValue.Width = 250;
            try { FuelConsumptionValue.Text = PUser.Vehicles[PUser.ActiveCarIndex].AverageFuelConsumption.ToString(); } catch { }
            Grid.SetRow(FuelConsumptionValue, 2);
            Grid.SetColumn(FuelConsumptionValue, 2);
            FuelConsumptionValue.MouseLeave += FuelConsumptionValueTryConvert;
            FuelConsumptionValue.Background = (Brush)Converter.ConvertFromString("#FF1065B9");
            FuelConsumptionValue.Foreground = Brushes.White;
            FuelConsumptionValue.FontFamily = new FontFamily("Arial Black");
            FuelConsumptionValue.FontSize = 14;
            FuelConsumptionValue.FontWeight = FontWeights.Bold;
            TravelCostCalculatorGrid.Children.Add(FuelConsumptionValue);

            ResoultPriceValue = new TextBlock();
            ResoultPriceValue.Foreground = (Brush)Converter.ConvertFromString("#FFEDF5FD");
            ResoultPriceValue.FontFamily = new FontFamily("Arial Black");
            ResoultPriceValue.FontWeight = FontWeights.Bold;
            Grid.SetRow(ResoultPriceValue, 3);
            Grid.SetColumn(ResoultPriceValue, 2);
            TravelCostCalculatorGrid.Children.Add(ResoultPriceValue);

            ResoultUsedFuel = new TextBlock();
            ResoultUsedFuel.Foreground = (Brush)Converter.ConvertFromString("#FFEDF5FD");
            ResoultUsedFuel.FontFamily = new FontFamily("Arial Black");
            ResoultUsedFuel.FontWeight = FontWeights.Bold;
            Grid.SetRow(ResoultUsedFuel, 4);
            Grid.SetColumn(ResoultUsedFuel, 2);
            TravelCostCalculatorGrid.Children.Add(ResoultUsedFuel);

            return TravelCostCalculatorBorder;
        }

        private void FuelConsumptionValueTryConvert(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            HandleTryConvertValue(ref textbox, PUser.UserLanguage == "PL" ? Config.MainPanel.CalculatorPage.TravelCostCalculatorBorder.Consumption.PL : Config.MainPanel.CalculatorPage.TravelCostCalculatorBorder.Consumption.ENG);
        }
        private void PriceForOneFuelUnitValueTryConvert(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            HandleTryConvertValue(ref textbox, PUser.UserLanguage == "PL" ? Config.MainPanel.CalculatorPage.TravelCostCalculatorBorder.PriceForLiter.PL : Config.MainPanel.CalculatorPage.TravelCostCalculatorBorder.PriceForLiter.ENG);
        }
        private void DistanceValueTryConvert(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            HandleTryConvertValue(ref textbox, PUser.UserLanguage == "PL" ? Config.MainPanel.CalculatorPage.TravelCostCalculatorBorder.Distance.PL : Config.MainPanel.CalculatorPage.TravelCostCalculatorBorder.Distance.ENG);
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
            catch (Exception) { MessageBox.Show((PUser.UserLanguage == "PL" ? Config.MainPanel.CalculatorPage.TravelCostCalculatorBorder.ErrorException.PL : Config.MainPanel.CalculatorPage.TravelCostCalculatorBorder.ErrorException.ENG)); }
        }


        private Border AverageFuelConsumptionCalculatorGenerator(AverageFuelConsumptionCalculatorBorder translation)
        {
            Border AverageFuelConsumptionCalculatorBorder = new Border();
            SetBorderProps(ref AverageFuelConsumptionCalculatorBorder,0);

            Grid AverageFuelConsumptionCalculatorGrid = new Grid();
            AverageFuelConsumptionCalculatorBorder.Padding = new Thickness(20);
            AverageFuelConsumptionCalculatorBorder.Child = AverageFuelConsumptionCalculatorGrid;
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

            Button CalculateButton = new Button();
            CalculateButton.Height = 60;
            CalculateButton.Width = 100;
            CalculateButton.BorderThickness = new Thickness(2);
            CalculateButton.Background = (Brush)Converter.ConvertFromString("#FF1065B9");
            CalculateButton.Foreground = Brushes.White;
            CalculateButton.FontFamily = new FontFamily("Arial Black");
            CalculateButton.FontWeight = FontWeights.Bold;
            CalculateButton.Click += AverageFuelConsumptionCalculatorButton_Click;
            CalculateButton.Margin = new Thickness(0, 0, 0, 8);
            Grid.SetRow(CalculateButton, 4);
            Grid.SetColumn(CalculateButton, 1);
            AverageFuelConsumptionCalculatorGrid.Children.Add(CalculateButton);

            switch (PUser.UserLanguage)
            {
                case "PL":
                    AverageFuelConsumptionCalculatorGrid.Children.Add(GenerateTextBlock(translation.ConsumedFuel.PL, 0, 0));
                    AverageFuelConsumptionCalculatorGrid.Children.Add(GenerateTextBlock(translation.NumberOfKilometersTraveled.PL, 1, 0));
                    AverageFuelConsumptionCalculatorGrid.Children.Add(GenerateTextBlock(translation.PriceForLiterOptional.PL, 2, 0));
                    AverageFuelConsumptionCalculatorGrid.Children.Add(GenerateTextBlock(translation.Resoult.PL, 3, 0));
                    CalculateButton.Content = translation.CalculateButton.PL;
                    break;
                case "ENG":
                    AverageFuelConsumptionCalculatorGrid.Children.Add(GenerateTextBlock(translation.ConsumedFuel.ENG, 0, 0));
                    AverageFuelConsumptionCalculatorGrid.Children.Add(GenerateTextBlock(translation.NumberOfKilometersTraveled.ENG, 1, 0));
                    AverageFuelConsumptionCalculatorGrid.Children.Add(GenerateTextBlock(translation.PriceForLiterOptional.ENG, 2, 0));
                    AverageFuelConsumptionCalculatorGrid.Children.Add(GenerateTextBlock(translation.Resoult.ENG, 3, 0));
                    CalculateButton.Content = translation.CalculateButton.ENG;
                    break;
            }

            ConsumedFuelValue = new TextBox();
            ConsumedFuelValue = new TextBox();
            ConsumedFuelValue.Height = 35;
            ConsumedFuelValue.Width = 250;
            Grid.SetRow(ConsumedFuelValue, 0);
            Grid.SetColumn(ConsumedFuelValue, 2);
            ConsumedFuelValue.MouseLeave += ConsumedFuelValueTryConvert;
            ConsumedFuelValue.Background = (Brush)Converter.ConvertFromString("#FF1065B9");
            ConsumedFuelValue.Foreground = Brushes.White;
            ConsumedFuelValue.FontFamily = new FontFamily("Arial Black");
            ConsumedFuelValue.FontSize = 14;
            ConsumedFuelValue.FontWeight = FontWeights.Bold;
            AverageFuelConsumptionCalculatorGrid.Children.Add(ConsumedFuelValue);

            NumberOfKilometersTraveledValue = new TextBox();
            NumberOfKilometersTraveledValue.Height = 35;
            NumberOfKilometersTraveledValue.Width = 250;
            Grid.SetRow(NumberOfKilometersTraveledValue, 1);
            Grid.SetColumn(NumberOfKilometersTraveledValue, 2);
            NumberOfKilometersTraveledValue.MouseLeave += NumberOfKilometersTraveledValueTryConvert;
            NumberOfKilometersTraveledValue.Background = (Brush)Converter.ConvertFromString("#FF1065B9");
            NumberOfKilometersTraveledValue.Foreground = Brushes.White;
            NumberOfKilometersTraveledValue.FontFamily = new FontFamily("Arial Black");
            NumberOfKilometersTraveledValue.FontSize = 14;
            NumberOfKilometersTraveledValue.FontWeight = FontWeights.Bold;
            AverageFuelConsumptionCalculatorGrid.Children.Add(NumberOfKilometersTraveledValue);

            PriceForOneFuelUnitValueOptional = new TextBox();
            PriceForOneFuelUnitValueOptional.Height = 35;
            PriceForOneFuelUnitValueOptional.Width = 250;
            Grid.SetRow(PriceForOneFuelUnitValueOptional, 2);
            Grid.SetColumn(PriceForOneFuelUnitValueOptional, 2);
            PriceForOneFuelUnitValueOptional.MouseLeave += PriceForOneFuelUnitValueOptionalTryConvert;
            PriceForOneFuelUnitValueOptional.Background = (Brush)Converter.ConvertFromString("#FF1065B9");
            PriceForOneFuelUnitValueOptional.Foreground = Brushes.White;
            PriceForOneFuelUnitValueOptional.FontFamily = new FontFamily("Arial Black");
            PriceForOneFuelUnitValueOptional.FontSize = 14;
            PriceForOneFuelUnitValueOptional.FontWeight = FontWeights.Bold;
            AverageFuelConsumptionCalculatorGrid.Children.Add(PriceForOneFuelUnitValueOptional);

            ResoultUsedFuelAverageFuelCOnsumption = new TextBlock();
            ResoultUsedFuelAverageFuelCOnsumption.Height = 35;
            ResoultUsedFuelAverageFuelCOnsumption.Width = 250;
            ResoultUsedFuelAverageFuelCOnsumption.Foreground = (Brush)Converter.ConvertFromString("#FFEDF5FD");
            ResoultUsedFuelAverageFuelCOnsumption.FontFamily = new FontFamily("Arial Black");
            ResoultUsedFuelAverageFuelCOnsumption.FontWeight = FontWeights.Bold;
            Grid.SetRow(ResoultUsedFuelAverageFuelCOnsumption, 3);
            Grid.SetColumn(ResoultUsedFuelAverageFuelCOnsumption, 2);
            AverageFuelConsumptionCalculatorGrid.Children.Add(ResoultUsedFuelAverageFuelCOnsumption);

            return AverageFuelConsumptionCalculatorBorder;
        }

        private void PriceForOneFuelUnitValueOptionalTryConvert(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            HandleTryConvertValue(ref textbox, PUser.UserLanguage == "PL" ? Config.MainPanel.CalculatorPage.AverageFuelConsumptionCalculatorBorder.PriceForLiterOptional.PL : Config.MainPanel.CalculatorPage.AverageFuelConsumptionCalculatorBorder.PriceForLiterOptional.ENG);
        }
        private void NumberOfKilometersTraveledValueTryConvert(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            HandleTryConvertValue(ref textbox, PUser.UserLanguage == "PL" ? Config.MainPanel.CalculatorPage.AverageFuelConsumptionCalculatorBorder.NumberOfKilometersTraveled.PL : Config.MainPanel.CalculatorPage.AverageFuelConsumptionCalculatorBorder.NumberOfKilometersTraveled.ENG);
        }
        private void ConsumedFuelValueTryConvert(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            HandleTryConvertValue(ref textbox, PUser.UserLanguage == "PL" ? Config.MainPanel.CalculatorPage.AverageFuelConsumptionCalculatorBorder.ConsumedFuel.PL : Config.MainPanel.CalculatorPage.AverageFuelConsumptionCalculatorBorder.ConsumedFuel.ENG);
        }

        private void AverageFuelConsumptionCalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PriceForOneFuelUnitValueOptional.Text != "")
                {
                    double ConsumedFuel = Convert.ToDouble(ConsumedFuelValue.Text);
                    double NumberOfKilometersTraveled = Convert.ToDouble(NumberOfKilometersTraveledValue.Text);
                    double PriceForOneFuelUnitOptional = Convert.ToDouble(PriceForOneFuelUnitValueOptional.Text);
                    double FuelResult = ((ConsumedFuel / NumberOfKilometersTraveled) * 100);
                    double CostResult = FuelResult * PriceForOneFuelUnitOptional;

                    ResoultUsedFuelAverageFuelCOnsumption.Text = "Spalanie wynosi " + FuelResult.ToString() + " litrów/100km" + " Koszt: " + CostResult.ToString();
                }
                else
                {
                    double ConsumedFuel = Convert.ToDouble(ConsumedFuelValue.Text);
                    double NumberOfKilometersTraveled = Convert.ToDouble(NumberOfKilometersTraveledValue.Text);
                    double Result = ((ConsumedFuel / NumberOfKilometersTraveled) * 100);

                    ResoultUsedFuelAverageFuelCOnsumption.Text = "Spalanie wyniosło " + Result.ToString() + " litrów na 100/km";
                }
            }
            catch (Exception) { MessageBox.Show(PUser.UserLanguage == "PL" ? Config.MainPanel.CalculatorPage.AverageFuelConsumptionCalculatorBorder.ErrorException.PL : Config.MainPanel.CalculatorPage.AverageFuelConsumptionCalculatorBorder.ErrorException.ENG); }
        }

        private void SetBorderProps(ref Border border, int row)
        {
            Brush BackgroundBrushh = (Brush)Converter.ConvertFromString("#FF001A34");
            border.Background = BackgroundBrushh;

            border.BorderThickness = new Thickness(5);
            border.BorderBrush = (Brush)Converter.ConvertFrom("#FF407BB6");
            border.CornerRadius = new CornerRadius(30);

            border.Margin = new Thickness(15, 5, 15, 5);
            border.Padding = new Thickness(0, 0, 35, 0);
            Grid.SetRow(border, row);
        }

        private TextBlock GenerateTextBlock(string text, int row, int column)
        {
            TextBlock TextBlockName = new TextBlock();
            TextBlockName.Foreground = (Brush)Converter.ConvertFromString("#FFEDF5FD");
            TextBlockName.FontFamily = new FontFamily("Arial Black");
            TextBlockName.FontWeight = FontWeights.Bold;
            TextBlockName.Text = text;
            TextBlockName.Margin = new Thickness(0, 2, 0, 2);
            Grid.SetRow(TextBlockName, row);
            Grid.SetColumn(TextBlockName, column);

            return TextBlockName;
        }

        private TextBox HandleTryConvertValue(ref TextBox textbox, string text)
        {
            try
            {
                Convert.ToDouble(textbox.Text);
                textbox.Background = (Brush)Converter.ConvertFromString("#FF1065B9");
            }
            catch (Exception)
            {
                textbox.Text = "0";
                textbox.Background = Brushes.Red;
                MessageBox.Show("Błąd danych! Proszę wprowadzić poprawną wartość pola " +text);
            }

            return textbox;
        }

    }
}
