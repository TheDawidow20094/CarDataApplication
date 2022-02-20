using Car_Data_Application.Models;
using Car_Data_Application.Models.Vehicle_Classes;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Car_Data_Application.Controllers
{
    class CalculatorContentGenerator : CarDataAppController
    {
        private Grid PTravelCostCalculatorGrid;
        private Grid PAverageFuelConsumptionGrid;

        private Storyboard PTravelCostAnimationStoryboard = new();
        private Storyboard PAverageConsumptionAnimationStoryboard = new();

        public void CalculatorGenerator(MainWindow mw, User user, Config paramConfig)
        {
            InitialAssignValue(mw, user, paramConfig);

            Grid MainGrid = new();

            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(80)});
            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(300) });

            MainGrid.Children.Add(GenerateSelectorPanel(paramConfig.MainPanel.CalculatorPage));
            MainGrid.Children.Add(GenerateTravelCostCalculatorPanel(paramConfig.MainPanel.CalculatorPage.TravelCostCalculatorBorder));
            MainGrid.Children.Add(GenerateAverageFuelConsumptionCalculatorPanel(paramConfig.MainPanel.CalculatorPage.AverageFuelConsumptionCalculatorBorder));

            mainWindow.ScrollViewerContent.Content = MainGrid; 

        }

        private void InitialAssignValue(MainWindow mw, User user, Config paramConfig)
        {
            config = paramConfig;
            mainWindow = mw;
            PUser = user;
            mainWindow.WhereAreYou = "CalculatorPage";
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.FindName("SidePanel")));
        }


        private Grid GenerateSelectorPanel(CalculatorPage translation)
        {
            Grid SelectorPanelGrid = new();
            SetGridProps(ref SelectorPanelGrid, 0);
            for (int i = 0; i < 2; i++)
            {
                SelectorPanelGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            List<ComboBoxItem> CalculatorTypesList = new();
            CalculatorTypesList.Add(new ComboBoxItem() { Content = PUser.UserLanguage, Tag = translation.TravelCostCalculator });
            CalculatorTypesList.Add(new ComboBoxItem() { Content = PUser.UserLanguage, Tag = translation.AverageFuelConsumptionCalculator });
            
            ComboBox CalculatorType = GenerateComboBox("SelectCalculatorType", 0, 0, CalculatorTypesList, HorizontalAlignment.Center, 250);
            CalculatorType.SelectionChanged += CalculatorTypeSelectionChanged;
            SelectorPanelGrid.Children.Add(CalculatorType);


            List<ComboBoxItem> FuelTypeList = new();
            if (PUser.Vehicles[PUser.ActiveCarIndex].Tanks.Gasoline != 0)
            {
                FuelTypeList.Add(new ComboBoxItem() { Tag = translation.Gasoline, Content = PUser.UserLanguage, Name = translation.Gasoline.ENG });
            }
            if (PUser.Vehicles[PUser.ActiveCarIndex].Tanks.Diesel != 0)
            {
                FuelTypeList.Add(new ComboBoxItem() { Tag = translation.Diesel, Content = PUser.UserLanguage, Name = translation.Diesel.ENG });
            }
            if (PUser.Vehicles[PUser.ActiveCarIndex].Tanks.LPG != 0)
            {
                FuelTypeList.Add(new ComboBoxItem() { Tag = translation.LPG, Content = PUser.UserLanguage, Name = translation.LPG.ENG });
            }

            ComboBox FuelType = GenerateComboBox("SelectFuelType", 0, 1, FuelTypeList, HorizontalAlignment.Center, 250);
            FuelType.SelectionChanged += FuelTypeSelectionChanged;
            SelectorPanelGrid.Children.Add(FuelType);

            return SelectorPanelGrid;
        }

        private Grid GenerateTravelCostCalculatorPanel(TravelCostCalculatorBorder translation)
        {
            PTravelCostCalculatorGrid = new();
            SetGridProps(ref PTravelCostCalculatorGrid, 1);

            TranslateTransform translateTransform = new();
            if (null != mainWindow.FindName("TravelCostCalculatorTransform"))
            {
                mainWindow.UnregisterName("TravelCostCalculatorTransform");
            }
            mainWindow.RegisterName("TravelCostCalculatorTransform", translateTransform);
            PTravelCostCalculatorGrid.RenderTransform = translateTransform;

            GenerateAnimation("TravelCostCalculatorTransform");

            for (int i = 0; i < 2; i++)
            {
                PTravelCostCalculatorGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            PTravelCostCalculatorGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            PTravelCostCalculatorGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            PTravelCostCalculatorGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            PTravelCostCalculatorGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            PTravelCostCalculatorGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            PTravelCostCalculatorGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });

            PTravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.Distance, PUser.UserLanguage, 0, 0, horizontalAlignment: HorizontalAlignment.Right));
            PTravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.PriceForLiter, PUser.UserLanguage, 1, 0, horizontalAlignment: HorizontalAlignment.Right));
            PTravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.Consumption, PUser.UserLanguage, 2, 0, horizontalAlignment: HorizontalAlignment.Right));

            PTravelCostCalculatorGrid.Children.Add(GenerateTextBoxWithHandler("Distance", 0, 1));
            PTravelCostCalculatorGrid.Children.Add(GenerateTextBoxWithHandler("PriceForLiter", 1, 1));
            PTravelCostCalculatorGrid.Children.Add(GenerateTextBoxWithHandler("Consumption", 2, 1));

            PTravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.ResultTravelCost, PUser.UserLanguage, 3, 0, LightTextColor, horizontalAlignment: HorizontalAlignment.Right, textBlockName: "TravelCostResult", visibility: Visibility.Hidden));
            PTravelCostCalculatorGrid.Children.Add(GenerateTextBlock(null, "", 3, 1, textBlockName: "TravelCostResultValue", visibility: Visibility.Hidden));

            PTravelCostCalculatorGrid.Children.Add(GenerateTextBlock(translation.ResultUsedFuel, PUser.UserLanguage, 4, 0, LightTextColor, horizontalAlignment: HorizontalAlignment.Right, textBlockName: "UsedFuelResult", visibility: Visibility.Hidden));
            PTravelCostCalculatorGrid.Children.Add(GenerateTextBlock(null, "", 4, 1, textBlockName: "UsedFuelResultValue", visibility: Visibility.Hidden));

            Button TravelCostCalculatorButton = GenerateButton(translation.CalculateButton, PUser.UserLanguage, 5, 0, buttonName: "TravelCostCalculatorButton");
            Grid.SetColumnSpan(TravelCostCalculatorButton, 2);
            TravelCostCalculatorButton.Click += CalculatorButtonClick;
            PTravelCostCalculatorGrid.Children.Add(TravelCostCalculatorButton);


            return PTravelCostCalculatorGrid;
        }

        private Grid GenerateAverageFuelConsumptionCalculatorPanel(AverageFuelConsumptionCalculatorBorder translation)
        {
            PAverageFuelConsumptionGrid = new();
            SetGridProps(ref PAverageFuelConsumptionGrid, 1);
            PAverageFuelConsumptionGrid.Visibility = Visibility.Hidden;

            TranslateTransform translateTransform = new();
            if (null != mainWindow.FindName("AverageFuelConsumptionTransform"))
            {
                mainWindow.UnregisterName("AverageFuelConsumptionTransform");
            }
            mainWindow.RegisterName("AverageFuelConsumptionTransform", translateTransform);
            PAverageFuelConsumptionGrid.RenderTransform = translateTransform;

            GenerateAnimation("AverageFuelConsumptionTransform");

            for (int i = 0; i < 2; i++)
            {
                PAverageFuelConsumptionGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            PAverageFuelConsumptionGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            PAverageFuelConsumptionGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            PAverageFuelConsumptionGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            PAverageFuelConsumptionGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            PAverageFuelConsumptionGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            PAverageFuelConsumptionGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });

            PAverageFuelConsumptionGrid.Children.Add(GenerateTextBlock(translation.ConsumedFuel, PUser.UserLanguage, 0, 0, horizontalAlignment: HorizontalAlignment.Right));
            PAverageFuelConsumptionGrid.Children.Add(GenerateTextBlock(translation.NumberOfKilometersTraveled, PUser.UserLanguage, 1, 0, horizontalAlignment: HorizontalAlignment.Right));
            PAverageFuelConsumptionGrid.Children.Add(GenerateTextBlock(translation.PriceForLiterOptional, PUser.UserLanguage, 2, 0, horizontalAlignment: HorizontalAlignment.Right));

            PAverageFuelConsumptionGrid.Children.Add(GenerateTextBoxWithHandler("ConsumedFuel", 0, 1));
            PAverageFuelConsumptionGrid.Children.Add(GenerateTextBoxWithHandler("NumberOfKilometersTraveled", 1, 1));
            PAverageFuelConsumptionGrid.Children.Add(GenerateTextBoxWithHandler("PriceForLiterOptional", 2, 1));

            PAverageFuelConsumptionGrid.Children.Add(GenerateTextBlock(translation.ResultAverageConsumption, PUser.UserLanguage, 3, 0, LightTextColor, horizontalAlignment: HorizontalAlignment.Right, textBlockName: "AverageConsumptionResult", visibility: Visibility.Hidden));
            PAverageFuelConsumptionGrid.Children.Add(GenerateTextBlock(null, "", 3, 1, textBlockName: "AverageConsumptionResultValue", visibility: Visibility.Hidden));

            PAverageFuelConsumptionGrid.Children.Add(GenerateTextBlock(translation.ResultPrice, PUser.UserLanguage, 4, 0, LightTextColor, horizontalAlignment: HorizontalAlignment.Right, textBlockName: "ResultPrice", visibility: Visibility.Hidden));
            PAverageFuelConsumptionGrid.Children.Add(GenerateTextBlock(null, "", 4, 1, textBlockName: "ResultPriceValue", visibility: Visibility.Hidden));

            Button AverageFuelConsumptionButton = GenerateButton(translation.CalculateButton, PUser.UserLanguage, 5, 0, buttonName: "AverageFuelConsumptionButton");
            Grid.SetColumnSpan(AverageFuelConsumptionButton, 2);
            AverageFuelConsumptionButton.Click += CalculatorButtonClick;
            PAverageFuelConsumptionGrid.Children.Add(AverageFuelConsumptionButton);


            return PAverageFuelConsumptionGrid;
        }

        
        private TextBox GenerateTextBoxWithHandler(string textboxname, int row, int column)
        {
            TextBox textBox = GenerateTextBox(textboxname, row, column);
            textBox.LostFocus += TextBoxLostFocus;
            return textBox;
        }

        private void CalculatorButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            switch (button.Name)
            {
                case "TravelCostCalculatorButton":
                    TextBox DistanceTextBox = (TextBox)mainWindow.FindName("Distance_TextBox");
                    TextBox PriceForLiterTextBox = (TextBox)mainWindow.FindName("PriceForLiter_TextBox");
                    TextBox ConsumptionTextBox = (TextBox)mainWindow.FindName("Consumption_TextBox");

                    double DistanceResult;
                    double PriceForLiterResult;
                    double ConsumptionResult;

                    if ((Double.TryParse(DistanceTextBox.Text, out DistanceResult)) && (Double.TryParse(PriceForLiterTextBox.Text, out PriceForLiterResult)) && (Double.TryParse(ConsumptionTextBox.Text, out ConsumptionResult)))
                    {
                        TextBlock TravelCostResultValue_TextBlock = (TextBlock)mainWindow.FindName("TravelCostResultValue_TextBlock");
                        TextBlock UsedFuelResultValue_TextBlock = (TextBlock)mainWindow.FindName("UsedFuelResultValue_TextBlock");
                        TextBlock TravelCostResult_TextBlock = (TextBlock)mainWindow.FindName("TravelCostResult_TextBlock");
                        TextBlock UsedFuelResult_TextBlock = (TextBlock)mainWindow.FindName("UsedFuelResult_TextBlock");

                        TravelCostResultValue_TextBlock.Visibility = Visibility.Visible;
                        UsedFuelResultValue_TextBlock.Visibility = Visibility.Visible;
                        TravelCostResult_TextBlock.Visibility = Visibility.Visible;
                        UsedFuelResult_TextBlock.Visibility = Visibility.Visible;

                        double TravelCost = (DistanceResult * ConsumptionResult / 100) * PriceForLiterResult;
                        double UsedFuel = (DistanceResult / ConsumptionResult);
                        TravelCostResultValue_TextBlock.Text = TravelCost + " zł".ToString();
                        UsedFuelResultValue_TextBlock.Text = UsedFuel + " litrów";
                    }
                    break;

                case "AverageFuelConsumptionButton":
                    TextBox ConsumedFuel = (TextBox)mainWindow.FindName("ConsumedFuel_TextBox");
                    TextBox NumberOfKilometersTraveled = (TextBox)mainWindow.FindName("NumberOfKilometersTraveled_TextBox");
                    TextBox PriceForLiterOptional = (TextBox)mainWindow.FindName("PriceForLiterOptional_TextBox");

                    double ConsumedFuelResult;
                    double NumberOfKilometersTraveledResult;
                    double PriceForLiterOptionalResult;

                    if ((Double.TryParse(ConsumedFuel.Text, out ConsumedFuelResult)) && (Double.TryParse(NumberOfKilometersTraveled.Text, out NumberOfKilometersTraveledResult)))
                    {
                        TextBlock AverageConsumptionResultValue_TextBlock = (TextBlock)mainWindow.FindName("AverageConsumptionResultValue_TextBlock");
                        TextBlock ResultPriceValue_TextBlock = (TextBlock)mainWindow.FindName("ResultPriceValue_TextBlock");
                        TextBlock AverageConsumptionResult_TextBlock = (TextBlock)mainWindow.FindName("AverageConsumptionResult_TextBlock");
                        TextBlock ResultPrice_TextBlock = (TextBlock)mainWindow.FindName("ResultPrice_TextBlock");

                        AverageConsumptionResultValue_TextBlock.Visibility = Visibility.Visible;
                        AverageConsumptionResult_TextBlock.Visibility = Visibility.Visible;

                        double AverageConsumption = (ConsumedFuelResult / NumberOfKilometersTraveledResult) * 100;
                        AverageConsumptionResultValue_TextBlock.Text = AverageConsumption + " l/100km";

                        if (Double.TryParse(PriceForLiterOptional.Text, out PriceForLiterOptionalResult))
                        {
                            ResultPriceValue_TextBlock.Visibility = Visibility.Visible;
                            ResultPrice_TextBlock.Visibility = Visibility.Visible;

                            double AverageConsumptionCost = AverageConsumption * PriceForLiterOptionalResult;
                            ResultPriceValue_TextBlock.Text = AverageConsumptionCost + " zł";
                        }
                        else
                        {
                            ResultPriceValue_TextBlock.Visibility = Visibility.Hidden;
                            ResultPrice_TextBlock.Visibility = Visibility.Hidden;
                        }
                    }
                    break;
            }
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double ParseResult;

            if (double.TryParse(textBox.Text, out ParseResult))
            {
                textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
            }
            else
            {
                textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
            }
        }

        private void FuelTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            switch (comboBox.Name)
            {
                case "Gasoline":
                    List<Refueling> refulings = PUser.Vehicles[PUser.ActiveCarIndex].Refulings;
                    ((TextBox)mainWindow.FindName("PriceForLiter")).Text = refulings[refulings.Count - 1].PriceForLiter.ToString();
                    break;

                case "Diesel":
                    break;

                case "LPG":
                    break;
            }
        }

        private async void CalculatorTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    PTravelCostCalculatorGrid.Visibility = Visibility.Visible;
                    mainWindow.BeginStoryboard(PTravelCostAnimationStoryboard);
                    await Task.Delay(500);
                    PAverageFuelConsumptionGrid.Visibility = Visibility.Hidden;
                    break;

                case 1:
                    PAverageFuelConsumptionGrid.Visibility = Visibility.Visible;
                    mainWindow.BeginStoryboard(PAverageConsumptionAnimationStoryboard);
                    await Task.Delay(500);
                    PTravelCostCalculatorGrid.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void GenerateAnimation(string transformName)
        {
            Storyboard storyboard = new();
            string EntryPanelTransformName = string.Empty;
            string ExitPanelTransformName = string.Empty;

            switch (transformName)
            {
                case "TravelCostCalculatorTransform":
                    storyboard = PTravelCostAnimationStoryboard;
                    EntryPanelTransformName = transformName;
                    ExitPanelTransformName = "AverageFuelConsumptionTransform";
                    break;

                case "AverageFuelConsumptionTransform":
                    storyboard = PAverageConsumptionAnimationStoryboard;
                    EntryPanelTransformName = transformName;
                    ExitPanelTransformName = "TravelCostCalculatorTransform";
                    break;
            }

            DoubleAnimation EntryAnimation = new DoubleAnimation();
            EntryAnimation.From = 1400;
            EntryAnimation.To = 0;
            EntryAnimation.BeginTime = new TimeSpan(0, 0, 0);
            EntryAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));

            Storyboard.SetTargetName(EntryAnimation, EntryPanelTransformName);
            Storyboard.SetTargetProperty(EntryAnimation, new PropertyPath(TranslateTransform.XProperty));


            DoubleAnimation ExitAnimation = new DoubleAnimation();
            ExitAnimation.From = 0;
            ExitAnimation.To = -1400;
            ExitAnimation.BeginTime = new TimeSpan(0, 0, 0);
            ExitAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));

            Storyboard.SetTargetName(ExitAnimation, ExitPanelTransformName);
            Storyboard.SetTargetProperty(ExitAnimation, new PropertyPath(TranslateTransform.XProperty));

            storyboard.Children.Clear();
            storyboard.Children.Add(EntryAnimation);
            storyboard.Children.Add(ExitAnimation);
        }
    }
}
