using Car_Data_Application.Models;
using Car_Data_Application.Models.Vehicle_Classes;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class AddCostPageGenerator : CarDataAppController
    {
        private Service newService;
        private Grid Reminder;
        private Grid MainGrid;

        public void PageGenerator(MainWindow mw, User user, Config paramConfig)
        {
            InitialAssignValue(mw, user, paramConfig);

            newService = new Service();
            MainGrid = new Grid();
            
            List<int> RowsHeights = new List<int>() { 70, 150, 70, 70, 210, 80 };
            foreach (int RowHeight in RowsHeights)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(RowHeight) });
            }

            MainGrid.Children.Add(AddingVehicleName()); 
            MainGrid.Children.Add(PrimaryDataContent(config.MainPanel.AddCostPage));
            MainGrid.Children.Add(DateContent(config.MainPanel.AddCostPage));
            MainGrid.Children.Add(ReminderContent(config.MainPanel.AddCostPage));
            MainGrid.Children.Add(CommentContent(config.MainPanel.AddCostPage));
            MainGrid.Children.Add(AddServiceButton(config.MainPanel.AddCostPage));

            mainWindow.ScrollViewerContent.Content = MainGrid;
        }

        private void InitialAssignValue(MainWindow mw, User user, Config paramConfig)
        {
            config = paramConfig;
            mainWindow = mw;
            PUser = user;
            mainWindow.AddButon.Visibility = Visibility.Hidden;
            mainWindow.WhereAreYou = "AddCostPage";
            SetButtonColor("CostsPage", (Grid)mainWindow.FindName("SidePanel"));
        }

        private TextBlock AddingVehicleName()
        {
            TextBlock EntriesListText = GenerateTextBlock(null, PUser.Vehicles[PUser.ActiveCarIndex].Brand + " " + PUser.Vehicles[PUser.ActiveCarIndex].Model, 0, 0, "#FF2A2729", HorizontalAlignment.Center);
            EntriesListText.FontSize = 34;
            EntriesListText.Margin = new Thickness(0, 15, 0, 10);

            return EntriesListText;
        }

        private Grid PrimaryDataContent (AddCostPage translation)
        {
            Grid PrimaryServiceData = new();
            SetGridProps(ref PrimaryServiceData, 1);

            for (int i = 0; i < 3; i++)
            {
                PrimaryServiceData.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < 4; i++)
            {
                PrimaryServiceData.ColumnDefinitions.Add(new ColumnDefinition());
            }

            List<ComboBoxItem> CategoryList = new();
            foreach  (PropertyInfo CostsType in config.CostsTypes.GetType().GetProperties())
            {
                Translation CostsTypeValue = (Translation)CostsType.GetValue(config.CostsTypes, null);
                CategoryList.Add(new ComboBoxItem() { Tag = CostsTypeValue, Content = PUser.UserLanguage });
            }

            TextBlock NameTextBlock = GenerateTextBlock(translation.Name, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Right);
            Grid.SetColumnSpan(NameTextBlock, 2);
            PrimaryServiceData.Children.Add(NameTextBlock);
            PrimaryServiceData.Children.Add(GenerateTextBlock(translation.Price, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
            PrimaryServiceData.Children.Add(GenerateTextBlock(translation.IsNegative, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right));
            PrimaryServiceData.Children.Add(GenerateTextBlock(translation.Category, PUser.UserLanguage, 1, 2, LightTextColor, HorizontalAlignment.Right));
            PrimaryServiceData.Children.Add(GenerateTextBlock(translation.Millage, PUser.UserLanguage, 2, 2, LightTextColor, HorizontalAlignment.Right));

            TextBox NameTextBox = GenerateTextBox("Name", 0, 2);
            Grid.SetColumnSpan(NameTextBox, 2);
            PrimaryServiceData.Children.Add(NameTextBox);
            PrimaryServiceData.Children.Add(GenerateTextBoxWithHandler("Price", 1, 1));
            PrimaryServiceData.Children.Add(GenerateToggleSwitch("IsNegative", 2, 1));
            PrimaryServiceData.Children.Add(GenerateComboBox("Category", 1, 3, CategoryList));
            PrimaryServiceData.Children.Add(GenerateTextBoxWithHandler("Millage", 2, 3));

            return PrimaryServiceData;
        }

        private Grid DateContent(AddCostPage translation)
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

            DateTime Tomorow = (DateTime.Now).AddDays(1);
            DataContentGrid.Children.Add(GenerateDatePicker("Date", 0, 1, DateTime.Now, calendarDateRange: new CalendarDateRange(Tomorow, DateTime.MaxValue)));

            TextBox textBox = GenerateTextBox("Time", 0, 3, false, HorizontalAlignment.Left, DateTime.Now.TimeOfDay.ToString().Substring(0, 5));
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
            else if ((Int32.TryParse(textBox.Text.Substring(0, 2), out hour)) && (Int32.TryParse(textBox.Text.Substring(3, 2), out minutes)))
            {
                if (textBox.Text[2] != ':')
                {
                    textBox.Text = textBox.Text.Substring(0, 2) + ":" + textBox.Text.Substring(3, 2);
                }

                if ((hour < 24) && (minutes < 59) && (minutes >= 0) && (hour >= 0))
                {
                    newService.Time = textBox.Text;
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

        private Grid ReminderContent(AddCostPage translation)
        {
            Reminder = new();
            SetGridProps(ref Reminder, 3);

            for (int i = 0; i < 4; i++)
            {
                Reminder.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 2; i++)
            {
                Reminder.RowDefinitions.Add(new RowDefinition());
            }
            Reminder.RowDefinitions[1].Height = new GridLength(0);

            Reminder.Children.Add(GenerateTextBlock(translation.Reminder, PUser.UserLanguage, 0, 1,LightTextColor , HorizontalAlignment.Right));

            Reminder.Children.Add(GenerateToggleSwitchWithHandler("ReminderTitle", 0, 2));

            Reminder.Children.Add(GenerateTextBlock(translation.Date, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
            Reminder.Children.Add(GenerateTextBlock(translation.Millage, PUser.UserLanguage, 1, 2, LightTextColor, HorizontalAlignment.Right));

            DateTime Tomorow = (DateTime.Now).AddDays(1);
            DatePicker datePicker = GenerateDatePicker("ReminderDate", 1, 1, Tomorow, calendarDateRange: new CalendarDateRange(DateTime.MinValue, DateTime.Now));
            Reminder.Children.Add(datePicker);

            Reminder.Children.Add(GenerateTextBoxWithHandler("ReminderMillage", 1, 3));

            return Reminder;
        }

        private Grid CommentContent(AddCostPage translation)
        {
            Grid CommentContentGrid = new Grid();
            SetGridProps(ref CommentContentGrid, 4);

            CommentContentGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            CommentContentGrid.RowDefinitions.Add(new RowDefinition());

            CommentContentGrid.Children.Add(GenerateTextBlock(translation.Comment, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Center, VerticalAlignment.Center));

            CommentContentGrid.Children.Add(GenerateTextBox("Comment", 1, 0, true, HorizontalAlignment.Center));

            return CommentContentGrid;
        }

        private Button AddServiceButton(AddCostPage translation)
        {
            Button ApplySettingsButton = GenerateButton(translation.ButtonText, PUser.UserLanguage,5, 0, DarkTextColor);
            ApplySettingsButton.Background = (Brush)Converter.ConvertFromString("#FF93D68A");
            ApplySettingsButton.Height = 60;
            ApplySettingsButton.Width = 200;
            ApplySettingsButton.Click += HandleAddServiceButtonClick;

            return ApplySettingsButton;
        }

        private TextBox GenerateTextBoxWithHandler(string textboxname, int row, int column)
        {
            TextBox textBox = GenerateTextBox(textboxname, row, column);
            textBox.LostFocus += TextBoxLostFocus;
            textBox.TextChanged += TextBoxOnChange;
            return textBox;
        }

        private Grid GenerateToggleSwitchWithHandler(string textboxname, int row, int column)
        {
            Grid toggleSwitch = GenerateToggleSwitch(textboxname, row, column);
            toggleSwitch.MouseLeftButtonDown += ToggleSwitchOnChange;
            return toggleSwitch;
        }

        private void ToggleSwitchOnChange(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Reminder.RowDefinitions[1].ActualHeight <= 0)
            {
                MainGrid.RowDefinitions[3].Height = new GridLength(130);
                Reminder.RowDefinitions[1].Height = new GridLength(60);
            }
            else
            {
                MainGrid.RowDefinitions[3].Height = new GridLength(70);
                Reminder.RowDefinitions[1].Height = new GridLength(0);
            }

        }

        private void HandleAddServiceButtonClick(object sender, RoutedEventArgs e)
        {
            bool canConvertToJSON = true;

            TextBox nameTextBox = (TextBox)mainWindow.FindName("Name_TextBox");
            TextBox priceTextBox = (TextBox)mainWindow.FindName("Price_TextBox");
            ComboBox categoryComboBox = (ComboBox)mainWindow.FindName("Category_ComboBox");
            Grid negativeCost = (Grid)mainWindow.FindName("IsNegative_ToggleSwitch");
            DatePicker datePicker = (DatePicker)mainWindow.FindName("Date_DatePicker");
            TextBox timeTextBox = (TextBox)mainWindow.FindName("Time_TextBox");
            Grid remindME = (Grid)mainWindow.FindName("ReminderTitle_ToggleSwitch");
            DatePicker datePickerReminder = (DatePicker)mainWindow.FindName("ReminderDate_DatePicker");
            TextBox millageReminderTextBox = (TextBox)mainWindow.FindName("ReminderMillage_TextBox");
            TextBox commentTextBlock = (TextBox)mainWindow.FindName("Comment_TextBox");

            if (nameTextBox.Text == "")
            {
                ((TextBox)mainWindow.FindName("Name_TextBox")).Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                canConvertToJSON = false;
            }
            if (priceTextBox.Text == "")
            {
                ((TextBox)mainWindow.FindName("Price_TextBox")).Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                canConvertToJSON = false;
            }

            newService.Name = nameTextBox.Text;
            newService.Category = ((ComboBoxItem)categoryComboBox.SelectedItem).Content.ToString();
            newService.IsNegative = (bool)negativeCost.Tag;
            newService.Date = datePicker.SelectedDate.ToString().Substring(0, 10);
            newService.Time = timeTextBox.Text;
            if ((bool)remindME.Tag)
            {
                //CHCECK FORMAT
                Reminder reminder = new();
                reminder.Millage = int.Parse(millageReminderTextBox.Text);
                reminder.Date = datePickerReminder.SelectedDate.ToString().Substring(0, 10);
                newService.Reminder = reminder;
            }
            newService.Comment = commentTextBlock.Text;


            if (canConvertToJSON)
            {
                PUser.Vehicles[PUser.ActiveCarIndex].Services.Add(newService);
                PUser.Vehicles[PUser.ActiveCarIndex].CarMillage = newService.CarMillage;
                PUser.SerializeData();
                mainWindow.OpenPage("CostsPage");
            }
        }

        private void TextBoxOnChange(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            switch (textBox.Name)
            {
                case "Price_TextBox":


                    int PriceParseResult;
                    if (int.TryParse(textBox.Text, out PriceParseResult))
                    {
                        if (PriceParseResult >= 0)
                        {
                            textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
                        }
                        else
                        {
                            textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                        }
                    }
                    else
                    {
                        textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                    }

                    break;

                case "Millage_TextBox":

                    int MillageParseResult;
                    if (textBox.Text != "")
                    {
                        if (int.TryParse(textBox.Text, out MillageParseResult))
                        {
                            textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
                        }
                        else
                        {
                            textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                        }
                    }
                    else
                    {
                        textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
                    }

                    break;

                case "ReminderMillage_TextBox":
                    
                    int ReminderMillageParseResult;
                    if (textBox.Text != "")
                    {
                        if (int.TryParse(textBox.Text, out ReminderMillageParseResult))
                        {
                            textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
                        }
                        else
                        {
                            textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                        }
                    }
                    else
                    {
                        textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
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
                ParseResult = 0;
            }
            switch (textBox.Name)
            {
                case "Price_TextBox":
                    newService.Price = ParseResult;
                    break;

                case "Millage_TextBox":
                    if (textBox.Text != "")
                    {
                        if (ParseResult >= PUser.Vehicles[PUser.ActiveCarIndex].CarMillage)
                        {
                            textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
                        }
                        else
                        {
                            textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                        }
                    }
                    else
                    {
                        textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
                    }
                    newService.CarMillage = (int)ParseResult;

                    break;

                case "ReminderMillage_TextBox":

                    if (textBox.Text != "")
                    {
                        if (ParseResult >= PUser.Vehicles[PUser.ActiveCarIndex].CarMillage)
                        {
                            textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
                        }
                        else
                        {
                            textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                        }
                    }
                    else
                    {
                        textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
                    }
                    //newService.CarMillage = (int)ParseResult;

                    break;
            }
        }
    }
}
