using Car_Data_Application.Models;
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
        public void PageGenerator(MainWindow mw, User user, Config paramConfig)
        {
            InitialAssignValue(mw, user, paramConfig);

            Grid MainGrid = new Grid();

            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
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
            SetButtonColor("RefuelingHistoryPage", ((Grid)mainWindow.MainGrid.Children[3]));
        }

        private Border AddingTitle(AddRefuelingPage translation)
        {
            string TitleText = string.Empty;
            switch (PUser.UserLanguage)
            {
                case "PL":
                    TitleText = translation.PageTitle.PL;
                    break;

                case "ENG":
                    TitleText = translation.PageTitle.ENG;
                    break;
            }

            Border TitleBorder = new Border();

            Grid TitleGrid = new Grid();
            TitleBorder.Padding = new Thickness(20);
            TitleBorder.Child = TitleGrid;
            TitleGrid.Children.Add(GenerateTextBlock(TitleText, 0,0, DarkTextColor, HorizontalAlignment.Center));

            return TitleBorder;
        }

        private TextBlock AddingVehicleName()
        {
            TextBlock EntriesListText = GenerateTextBlock(PUser.Vehicles[PUser.ActiveCarIndex].Model + " " + PUser.Vehicles[PUser.ActiveCarIndex].Brand, 0, 0, "#FF2A2729", HorizontalAlignment.Center);
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

            switch (PUser.UserLanguage)
            {
                case "PL":
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.Liters.PL, 0, 0, LightTextColor, HorizontalAlignment.Right));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.PriceForLiter.PL, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.TotalPrice.PL, 2, 0, LightTextColor, HorizontalAlignment.Right));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.CarMillage.PL, 0, 2, LightTextColor, HorizontalAlignment.Right));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.FuelType.PL, 1, 2, LightTextColor, HorizontalAlignment.Right));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.IsFull.PL, 2, 2, LightTextColor, HorizontalAlignment.Right));
                    break;

                case "ENG":
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.Liters.ENG, 0, 0, LightTextColor, HorizontalAlignment.Right));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.PriceForLiter.ENG, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.TotalPrice.ENG, 2, 0, LightTextColor, HorizontalAlignment.Right));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.CarMillage.ENG, 0, 2, LightTextColor, HorizontalAlignment.Right));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.FuelType.ENG, 1, 2, LightTextColor, HorizontalAlignment.Right));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.IsFull.ENG, 2, 2, LightTextColor, HorizontalAlignment.Right));
                    break;    
            }

            MainContentGrid.Children.Add(GenerateTextBox(translation.Liters.ENG.TrimEnd(':'), 0, 1));

            MainContentGrid.Children.Add(GenerateTextBox(translation.PriceForLiter.ENG.TrimEnd(':'), 1, 1));

            MainContentGrid.Children.Add(GenerateTextBox(translation.TotalPrice.ENG.TrimEnd(':'), 2, 1));

            MainContentGrid.Children.Add(GenerateTextBox(translation.CarMillage.ENG.TrimEnd(':'), 0, 3));

            MainContentGrid.Children.Add(GenerateTextBox(translation.FuelType.ENG.TrimEnd(':'), 1, 3));

            MainContentGrid.Children.Add(GenerateTextBox(translation.IsFull.ENG.TrimEnd(':'), 2, 3));

            return MainContentGrid;
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

            switch (PUser.UserLanguage)
            {
                case "PL":
                    DataContentGrid.Children.Add(GenerateTextBlock(translation.Date.PL, 0, 0, LightTextColor, HorizontalAlignment.Right));
                    DataContentGrid.Children.Add(GenerateTextBlock(translation.Time.PL, 0, 2, LightTextColor, HorizontalAlignment.Right));
                    break;

                case "ENG":
                    DataContentGrid.Children.Add(GenerateTextBlock(translation.Date.ENG, 0, 0, LightTextColor, HorizontalAlignment.Right));
                    DataContentGrid.Children.Add(GenerateTextBlock(translation.Time.ENG, 0, 2, LightTextColor, HorizontalAlignment.Right));
                    break;
            }

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
                //textBox.Background = Brushes.IndianRed;
            }
            else if((Int32.TryParse(textBox.Text.Substring(0, 2), out hour)) && (Int32.TryParse(textBox.Text.Substring(3, 2), out minutes)))
            {
                if (textBox.Text[2] != ':')
                {
                    textBox.Text = textBox.Text.Substring(0, 2) + ":" + textBox.Text.Substring(3,2);
                }
                
                if ((hour < 24) &&  (minutes < 59) && (minutes >= 0) && (hour >= 0))
                {
                    textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
                    //textBox.Background = Brushes.IndianRed;
                }
                else
                {
                    textBox.Text = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);
                }
            }
            else
            {
                textBox.Text = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);
                //textBox.Background = Brushes.IndianRed;
            }
        }

        private Grid CommentContent(AddRefuelingPage translation)
        {
            Grid CommentContentGrid = new Grid();
            SetGridProps(ref CommentContentGrid, 3);

            CommentContentGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40)});
            CommentContentGrid.RowDefinitions.Add(new RowDefinition());

            switch (PUser.UserLanguage)
            {
                case "PL":
                    CommentContentGrid.Children.Add(GenerateTextBlock(translation.Comment.PL , 0, 0, LightTextColor, HorizontalAlignment.Center, VerticalAlignment.Center));
                    break;

                case "ENG":
                    CommentContentGrid.Children.Add(GenerateTextBlock(translation.Comment.ENG, 0, 0, LightTextColor, HorizontalAlignment.Center, VerticalAlignment.Center));
                    break;
            }

            CommentContentGrid.Children.Add(GenerateTextBox(translation.Comment.ENG.TrimEnd(':'), 1, 0, true,  HorizontalAlignment.Center));

            return CommentContentGrid;
        }

        private Button AddRefuelingButton(AddRefuelingPage translation)
        {
            Button AddRefuelingButton = new Button();

            switch (PUser.UserLanguage)
            {
                case "PL":
                    AddRefuelingButton.Content = translation.ButtonText.PL;
                    break;

                case "ENG":
                    AddRefuelingButton.Content = translation.ButtonText.ENG;
                    break;
            } 
            AddRefuelingButton.Height = 60;
            AddRefuelingButton.Width = 140;
            AddRefuelingButton.Click += HandleAddRefuelingButtonClick;
            AddRefuelingButton.Background = (Brush)Converter.ConvertFromString("#07A802");
            AddRefuelingButton.Foreground = Brushes.White;
            AddRefuelingButton.FontFamily = new FontFamily("Arial Black");
            AddRefuelingButton.FontWeight = FontWeights.Bold;
            AddRefuelingButton.Margin = new Thickness(0,0,0,8);
            Grid.SetRow(AddRefuelingButton, 4);

            return AddRefuelingButton;
        }

        private void HandleAddRefuelingButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("DZIAŁAM");
        }
    }
}
