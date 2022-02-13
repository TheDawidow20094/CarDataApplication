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
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class AddRefuelingPageGenerator : CarDataAppController
    {
        private Refueling newRefueling = new Refueling();
        private TextBox Liters_TextBox;
        private TextBox PriceForLiter_TextBox;
        private TextBox TotalPrice_TextBox;


        public void PageGenerator(MainWindow mw, User user, Config paramConfig)
        {
            InitialAssignValue(mw, user, paramConfig);

            Grid MainGrid = new Grid();

            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });
            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(140) });
            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });
            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(210) });
            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(80) });

            //MainGrid.Children.Add(AddingTitle(config.MainPanel.AddRefuelingPage));
            MainGrid.Children.Add(AddingVehicleName());
            MainGrid.Children.Add(MainContent(config.MainPanel.AddRefuelingPage));
            MainGrid.Children.Add(DataContent(config.MainPanel.AddRefuelingPage));
            MainGrid.Children.Add(CommentContent(config.MainPanel.AddRefuelingPage));
            MainGrid.Children.Add(AddRefuelingButton(config.MainPanel.AddRefuelingPage));

            mainWindow.ScrollViewerContent.Content = MainGrid;
        }

        private void InitialAssignValue(MainWindow mw, User user, Config paramConfig)
        {
            config = paramConfig;
            mainWindow = mw;
            PUser = user;
            mainWindow.AddButon.Visibility = Visibility.Hidden;
            mainWindow.WhereAreYou = "AddRefuelingPage";
            SetButtonColor("RefuelingHistoryPage", ((Grid)mainWindow.FindName("SidePanel")));
        }

        private TextBlock AddingVehicleName()
        {
            TextBlock EntriesListText = GenerateTextBlock(null, PUser.Vehicles[PUser.ActiveCarIndex].Model + " " + PUser.Vehicles[PUser.ActiveCarIndex].Brand, 0, 0, "#FF2A2729", HorizontalAlignment.Center);
            EntriesListText.FontSize = 34;
            EntriesListText.Margin = new Thickness(0, 15, 0, 10);

            return EntriesListText;
        }

        private Grid MainContent(AddRefuelingPage translation)
        {
            Grid MainContentGrid = new Grid();
            SetGridProps(ref MainContentGrid, 1);

            for (int i = 0; i < 4; i++) // 4 number of columns
            {
                MainContentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 3; i++) // 3 number of rows
            {
                MainContentGrid.RowDefinitions.Add(new RowDefinition());
            }

            MainContentGrid.Children.Add(GenerateTextBlock(translation.Liters, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Right));
            MainContentGrid.Children.Add(GenerateTextBlock(translation.PriceForLiter, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
            MainContentGrid.Children.Add(GenerateTextBlock(translation.TotalPrice, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right));
            MainContentGrid.Children.Add(GenerateTextBlock(translation.CarMillage, PUser.UserLanguage, 0, 2, LightTextColor, HorizontalAlignment.Right));
            MainContentGrid.Children.Add(GenerateTextBlock(translation.FuelType, PUser.UserLanguage, 1, 2, LightTextColor, HorizontalAlignment.Right));
            MainContentGrid.Children.Add(GenerateTextBlock(translation.IsFull, PUser.UserLanguage, 2, 2, LightTextColor, HorizontalAlignment.Right));

            List<ComboBoxItem> comboboxItems = new();
            if (PUser.Vehicles[PUser.ActiveCarIndex].Tanks.Gasoline != 0)
            {
                comboboxItems.Add(new ComboBoxItem() { Tag = config.FuelTypes.Gasoline, Content = PUser.UserLanguage });
            }
            if (PUser.Vehicles[PUser.ActiveCarIndex].Tanks.Diesel != 0)
            {
                comboboxItems.Add(new ComboBoxItem() { Tag = config.FuelTypes.Diesel, Content = PUser.UserLanguage });
            }
            if (PUser.Vehicles[PUser.ActiveCarIndex].Tanks.LPG != 0)
            {
                comboboxItems.Add(new ComboBoxItem() { Tag = config.FuelTypes.LPG, Content = PUser.UserLanguage });
            }

            Liters_TextBox = GenerateTextBoxWithHandler("Liters", 0, 1);
            PriceForLiter_TextBox = GenerateTextBoxWithHandler("PriceForLiter", 1, 1);
            TotalPrice_TextBox = GenerateTextBoxWithHandler("TotalPrice", 2, 1);

            MainContentGrid.Children.Add(Liters_TextBox);
            MainContentGrid.Children.Add(PriceForLiter_TextBox);
            MainContentGrid.Children.Add(TotalPrice_TextBox);

            MainContentGrid.Children.Add(GenerateTextBoxWithHandler("CarMillage", 0, 3));
            MainContentGrid.Children.Add(GenerateComboBox("FuelType", 1, 3, comboboxItems));
            MainContentGrid.Children.Add(GenerateToggleSwitch("IsFull", 2, 3));
            
            return MainContentGrid;
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox Liters_TextBox = (TextBox)mainWindow.FindName("Liters_TextBox");
            TextBox PriceForLiter_TextBox = (TextBox)mainWindow.FindName("PriceForLiter_TextBox");
            TextBox TotalPrice_TextBox = (TextBox)mainWindow.FindName("TotalPrice_TextBox");

            TextBox textBox = (TextBox)sender;
            double ParseResult;

            if (double.TryParse(textBox.Text, out ParseResult))
            {
                textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
            }
            else
            {
                textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                ParseResult = 0;
            }
            switch (textBox.Name)
            {
                case "Liters_TextBox":
                    newRefueling.Liters = ParseResult;
                    break;

                case "PriceForLiter_TextBox":
                    newRefueling.PriceForLiter = ParseResult;
                    break;

                case "TotalPrice_TextBox":
                    newRefueling.TotalPrice = ParseResult;
                    break;

                case "CarMillage_TextBox":
                    newRefueling.CarMillage = (int)ParseResult;
                    break;
            }
        }

        private void TextBoxOnChange(object sender, TextChangedEventArgs e)
        {

            TextBox textBox = (TextBox)sender;

            switch (textBox.Name)
            {
                case "Liters_TextBox":

                    double LitersParseResult;
                    if (Double.TryParse(Liters_TextBox.Text, out LitersParseResult))
                    {
                        if ((newRefueling.PriceForLiter != 0))
                        {
                            string result = (Math.Round(newRefueling.PriceForLiter * LitersParseResult, 2)).ToString();
                            if (TotalPrice_TextBox.Text != result)
                            {
                                TotalPrice_TextBox.Text = result;
                                newRefueling.TotalPrice = double.Parse(result);
                            }
                        }
                        if ((newRefueling.TotalPrice != 0) && (newRefueling.PriceForLiter == 0))
                        {
                            string result = (Math.Round(newRefueling.TotalPrice / LitersParseResult, 2)).ToString();
                            if (PriceForLiter_TextBox.Text != result)
                            {
                                PriceForLiter_TextBox.Text = result;
                                newRefueling.PriceForLiter = double.Parse(result);
                            }
                        }
                    }

                    break;

                case "PriceForLiter_TextBox":

                    double PriceForLiterParseResult;
                    if (Double.TryParse(PriceForLiter_TextBox.Text, out PriceForLiterParseResult))
                    {
                        if ((newRefueling.Liters != 0))
                        {
                            string result = (Math.Round(newRefueling.Liters * PriceForLiterParseResult, 2)).ToString();
                            if (TotalPrice_TextBox.Text != result)
                            {
                                TotalPrice_TextBox.Text = result;
                                newRefueling.TotalPrice = double.Parse(result);
                            }
                        }
                        if ((newRefueling.TotalPrice != 0) && (newRefueling.Liters == 0))
                        {
                            string result = (Math.Round(newRefueling.TotalPrice / PriceForLiterParseResult, 2)).ToString();
                            if (Liters_TextBox.Text != result)
                            {
                                Liters_TextBox.Text = result;
                                newRefueling.Liters = double.Parse(result);
                            }
                        }
                    }

                    break;

                case "TotalPrice_TextBox":

                    double TotalPriceParseResult;
                    if (Double.TryParse(TotalPrice_TextBox.Text, out TotalPriceParseResult))
                    {
                        if ((Liters_TextBox.Text != ""))
                        {
                            string result = (Math.Round(TotalPriceParseResult / newRefueling.Liters, 2)).ToString();
                            if (PriceForLiter_TextBox.Text != result)
                            {
                                PriceForLiter_TextBox.Text = result;
                                newRefueling.PriceForLiter = double.Parse(result);
                            }
                        }
                    }

                    break;
            }
        }

        private Grid DataContent(AddRefuelingPage translation)
        {
            Grid DataContentGrid = new Grid();
            SetGridProps(ref DataContentGrid, 2);

            DataContentGrid.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < 4; i++) // 4 number of columns
            {
                DataContentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            DataContentGrid.Children.Add(GenerateTextBlock(translation.Date, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Right));
            DataContentGrid.Children.Add(GenerateTextBlock(translation.Time, PUser.UserLanguage, 0, 2, LightTextColor, HorizontalAlignment.Right));

            DataContentGrid.Children.Add(GenerateDatePicker(translation.Date.ENG.TrimEnd(':'), 0, 1));

            TextBox textBox = GenerateTextBox(translation.Time.ENG.TrimEnd(':'), 0, 3, false, HorizontalAlignment.Left, DateTime.Now.TimeOfDay.ToString().Substring(0, 5));
            textBox.LostFocus += CheckTimeFormat;
            DataContentGrid.Children.Add(textBox);

            return DataContentGrid;
        }

        private void CheckTimeFormat(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int hour = new();
            int minutes = new();

            if (textBox.Text.Length != 5)
            {
                textBox.Text = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);
            }
            else if((Int32.TryParse(textBox.Text.Substring(0, 2), out hour)) && (Int32.TryParse(textBox.Text.Substring(3, 2), out minutes)))
            {
                if (textBox.Text[2] != ':')
                {
                    textBox.Text = textBox.Text.Substring(0, 2) + ":" + textBox.Text.Substring(3,2);
                }
                
                if ((hour < 24) &&  (minutes < 59) && (minutes >= 0) && (hour >= 0))
                {
                    newRefueling.Time = textBox.Text;
                    //textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
                }
                else
                {
                    textBox.Text = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);
                }
            }
            else
            {
                textBox.Text = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);
            }
        }

        private Grid CommentContent(AddRefuelingPage translation)
        {
            Grid CommentContentGrid = new Grid();
            SetGridProps(ref CommentContentGrid, 3);

            CommentContentGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40)});
            CommentContentGrid.RowDefinitions.Add(new RowDefinition());

            CommentContentGrid.Children.Add(GenerateTextBlock(translation.Comment, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Center, VerticalAlignment.Center));

            CommentContentGrid.Children.Add(GenerateTextBox(translation.Comment.ENG.TrimEnd(':'), 1, 0, true,  HorizontalAlignment.Center));

            return CommentContentGrid;
        }

        private Button AddRefuelingButton(AddRefuelingPage translation)
        {
            Button ApplySettingsButton = GenerateButton(translation.ButtonText, PUser.UserLanguage, 4, 0, DarkTextColor);
            ApplySettingsButton.Background = (Brush)Converter.ConvertFromString("#FF93D68A");
            ApplySettingsButton.Height = 60;
            ApplySettingsButton.Width = 200;
            ApplySettingsButton.Click += HandleAddRefuelingButtonClick;

            return ApplySettingsButton;
        }

        private TextBox GenerateTextBoxWithHandler(string textboxname, int row, int column)
        {
            TextBox textBox = GenerateTextBox(textboxname, row, column);
            textBox.LostFocus += TextBoxLostFocus;
            textBox.TextChanged += TextBoxOnChange;
            return textBox;
        }

        private void HandleAddRefuelingButtonClick(object sender, RoutedEventArgs e)
        {
            bool canConvertToJSON = true;

            if (newRefueling.Liters == 0)
            {
                ((TextBox)mainWindow.FindName("Liters_TextBox")).Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                canConvertToJSON = false;
            }
            if (newRefueling.PriceForLiter == 0)
            {
                ((TextBox)mainWindow.FindName("PriceForLiter_TextBox")).Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                canConvertToJSON = false;
            }
            if (newRefueling.TotalPrice == 0)
            {
                ((TextBox)mainWindow.FindName("TotalPrice_TextBox")).Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                canConvertToJSON = false;
            }
            if (newRefueling.CarMillage == 0)
            {
                ((TextBox)mainWindow.FindName("CarMillage_TextBox")).Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                canConvertToJSON = false;
                //sprawdź czy wiekszy niż przy poprzednim tankowaniu
            }

            ComboBox comboBox = (ComboBox)mainWindow.FindName("FuelType_ComboBox");
            ComboBoxItem comboBoxItem = (ComboBoxItem)comboBox.SelectedItem;
            Translation translation = (Translation)comboBoxItem.Tag;
            newRefueling.FuelType = translation.ENG;

            Grid toggleSwitch = (Grid)mainWindow.FindName("IsFull_ToggleSwitch");
            newRefueling.IsFull = (bool)((Grid)mainWindow.FindName("IsFull_ToggleSwitch")).Tag;
            newRefueling.Date = ((DatePicker)mainWindow.FindName("Date_DatePicker")).SelectedDate.ToString().Substring(0, 10);
            newRefueling.Time = ((TextBox)mainWindow.FindName("Time_TextBox")).Text;
            newRefueling.Comment = ((TextBox)mainWindow.FindName("Comment_TextBox")).Text;

            newRefueling.Distance = CalculateDistance(newRefueling.CarMillage, newRefueling.FuelType);
            newRefueling.Consumption = CalculateConsumption();


            if (canConvertToJSON)
            {
                PUser.Vehicles[PUser.ActiveCarIndex].Refulings.Add(newRefueling);
                PUser.SerializeData();

                mainWindow.OpenPage("RefuelingHistoryPage");
            }
        }

        private double CalculateDistance(double vehicleMillage, string fuelType)
        {
            double result = 0;
            List<Refueling> refuelings = PUser.Vehicles[PUser.ActiveCarIndex].Refulings;

            for (int i = (refuelings.Count - 1); i >= 0; i--)
            {
                if (refuelings[i].FuelType == fuelType)
                {
                    result = vehicleMillage - refuelings[i].CarMillage;
                }
                
            }
            return result;
        }

        private double CalculateConsumption()
        {
            double result = 0;
            if (newRefueling.IsFull)
            {
                result = (newRefueling.Liters / newRefueling.Distance) * 100;
            }
            return result;
        }

    }
}
