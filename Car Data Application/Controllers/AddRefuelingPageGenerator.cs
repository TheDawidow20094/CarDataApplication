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
        public void PageGenerator(MainWindow mw, User user, MainGrid config)
        {
            InitialAssignValue(mw, user, config);

            Grid MainGrid = new Grid();
            for (int i = 0; i <= 6; i++)
            {
                RowDefinition MainGridRow = new RowDefinition();
                MainGrid.RowDefinitions.Add(MainGridRow);
            }

            MainGrid.Children.Add(AddingTitle(Config.MainPanel.AddRefuelingPage));
            MainGrid.Children.Add(AddingVehicleName());
            MainGrid.Children.Add(MainContent(Config.MainPanel.AddRefuelingPage));
            MainGrid.Children.Add(DataContent(Config.MainPanel.AddRefuelingPage));
            MainGrid.Children.Add(CommentContent(Config.MainPanel.AddRefuelingPage));
            MainGrid.Children.Add(AddRefuelingButton(Config.MainPanel.AddRefuelingPage));

            mainWindow.ScrollViewerContent.Content = MainGrid;
        }

        private void InitialAssignValue(MainWindow mw, User user, MainGrid config)
        {
            Config = config;
            mainWindow = mw;
            PUser = user;
            mainWindow.AddButon.Visibility = Visibility.Hidden;
            mainWindow.WhereAreYou = "AddRefuelingPage";
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
            SetBorderProps(ref TitleBorder, 0, "#07A802", "#0BFF03");

            Grid TitleGrid = new Grid();
            TitleBorder.Padding = new Thickness(20);
            TitleBorder.Child = TitleGrid;
            TitleGrid.Children.Add(GenerateTextBlock(TitleText, 0,0, null, HorizontalAlignment.Center));

            return TitleBorder;
        }

        private TextBlock AddingVehicleName()
        {
            string Title = PUser.Vehicles[PUser.ActiveCarIndex].Model + " " + PUser.Vehicles[PUser.ActiveCarIndex].Brand;

            TextBlock EntriesListText = GenerateTextBlock(Title, 1, 0, "#FF407BB6", HorizontalAlignment.Center);
            EntriesListText.FontSize = 34;
            EntriesListText.Margin = new Thickness(0, 25, 0, 0);

            return EntriesListText;
        }

        private Border MainContent(AddRefuelingPage translation)
        {
            Border MainContentBorder = new Border();
            SetBorderProps(ref MainContentBorder, 2);

            Grid MainContentGrid = new Grid();
            MainContentBorder.Padding = new Thickness(20);
            MainContentBorder.Child = MainContentGrid;

            for (int i = 0; i < 2; i++) // 2 number of columns
            {
                ColumnDefinition MainContentGridColumn = new ColumnDefinition();
                MainContentGrid.ColumnDefinitions.Add(MainContentGridColumn);
            }
            for (int i = 0; i < 3; i++) // 3 number of rows
            {
                RowDefinition MainContentGridRow = new RowDefinition();
                MainContentGrid.RowDefinitions.Add(MainContentGridRow);
            }

            switch (PUser.UserLanguage)
            {
                case "PL":
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.Liters.PL, 0, 0));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.PriceForLiter.PL, 1, 0));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.TotalPrice.PL, 2, 0));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.CarMillage.PL, 0, 1));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.FuelType.PL, 1, 1));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.IsFull.PL, 2, 1));
                    break;

                case "ENG":
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.Liters.ENG, 0, 0));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.PriceForLiter.ENG, 1, 0));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.TotalPrice.ENG, 2, 0));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.CarMillage.ENG, 0, 1));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.FuelType.ENG, 1, 1));
                    MainContentGrid.Children.Add(GenerateTextBlock(translation.IsFull.ENG, 2, 1));
                    break;    
            }

            MainContentGrid.Children.Add(GenerateTextBox(translation.Liters.ENG.TrimEnd(':'), 0, 0));

            MainContentGrid.Children.Add(GenerateTextBox(translation.PriceForLiter.ENG.TrimEnd(':'), 1, 0));

            MainContentGrid.Children.Add(GenerateTextBox(translation.TotalPrice.ENG.TrimEnd(':'), 2, 0));

            MainContentGrid.Children.Add(GenerateTextBox(translation.CarMillage.ENG.TrimEnd(':'), 0, 1));

            MainContentGrid.Children.Add(GenerateTextBox(translation.FuelType.ENG.TrimEnd(':'), 1, 1));

            MainContentGrid.Children.Add(GenerateTextBox(translation.IsFull.ENG.TrimEnd(':'), 2, 1));

            return MainContentBorder;
        }

        private Border DataContent(AddRefuelingPage translation)
        {
            Border DataContentBorder = new Border();
            SetBorderProps(ref DataContentBorder, 3);

            Grid DataContentGrid = new Grid();
            DataContentBorder.Padding = new Thickness(20);
            DataContentBorder.Child = DataContentGrid;
            for (int i = 0; i < 2; i++) // 2 number of columns and rows
            {
                ColumnDefinition DataContentGridColumn = new ColumnDefinition();
                DataContentGrid.ColumnDefinitions.Add(DataContentGridColumn);
                RowDefinition DataContentGridRow = new RowDefinition();
                DataContentGrid.RowDefinitions.Add(DataContentGridRow);
            }

            switch (PUser.UserLanguage)
            {
                case "PL":
                    DataContentGrid.Children.Add(GenerateTextBlock(translation.Date.PL, 0, 0));
                    DataContentGrid.Children.Add(GenerateTextBlock(translation.Time.PL, 1, 0));
                    break;

                case "ENG":
                    DataContentGrid.Children.Add(GenerateTextBlock(translation.Date.ENG, 0, 0));
                    DataContentGrid.Children.Add(GenerateTextBlock(translation.Time.ENG, 1, 0));
                    break;
            }

            DataContentGrid.Children.Add(GenerateTextBox(translation.Date.ENG.TrimEnd(':'), 0, 0));

            DataContentGrid.Children.Add(GenerateTextBox(translation.Time.ENG.TrimEnd(':'), 1, 0));

            return DataContentBorder;
        }

        private Border CommentContent(AddRefuelingPage translation)
        {
            Border CommentContentBorder = new Border();
            SetBorderProps(ref CommentContentBorder, 4);

            Grid CommentContentGrid = new Grid();
            CommentContentBorder.Padding = new Thickness(20);
            CommentContentBorder.Child = CommentContentGrid;
            for (int i = 0; i < 2; i++) // 2 number of rows
            {
                RowDefinition CommentContentGridRow = new RowDefinition();
                CommentContentGrid.RowDefinitions.Add(CommentContentGridRow);
            }

            switch (PUser.UserLanguage)
            {
                case "PL":
                    CommentContentGrid.Children.Add(GenerateTextBlock(translation.Comment.PL , 0, 0, null, HorizontalAlignment.Center));
                    break;

                case "ENG":
                    CommentContentGrid.Children.Add(GenerateTextBlock(translation.Comment.ENG, 0, 0, null, HorizontalAlignment.Center));
                    break;
            }

            CommentContentGrid.Children.Add(GenerateTextBox(translation.Comment.ENG.TrimEnd(':'), 1, 0, true, HorizontalAlignment.Center));

            return CommentContentBorder;
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
            AddRefuelingButton.Width = 100;
            AddRefuelingButton.Click += HandleAddRefuelingButtonClick;
            AddRefuelingButton.Background = (Brush)Converter.ConvertFromString("#07A802");
            AddRefuelingButton.Foreground = Brushes.White;
            AddRefuelingButton.FontFamily = new FontFamily("Arial Black");
            AddRefuelingButton.FontWeight = FontWeights.Bold;
            AddRefuelingButton.Margin = new Thickness(0,0,0,8);
            Grid.SetRow(AddRefuelingButton, 5);

            return AddRefuelingButton;
        }

        private void HandleAddRefuelingButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("DZIAŁAM");
        }




        private void SetBorderProps(ref Border border, int row, string backgroundcolor = default, string bordercolor = default)
        {
            Brush BackgroundBrushh = (Brush)Converter.ConvertFromString(backgroundcolor == default ? "#FF001A34" : backgroundcolor);
            border.Background = BackgroundBrushh;

            border.BorderThickness = new Thickness(5);
            border.BorderBrush = (Brush)Converter.ConvertFrom(bordercolor == default ? "#FF407BB6" : bordercolor);
            border.CornerRadius = new CornerRadius(30);

            border.Margin = new Thickness(15, 5, 15, 5);
            border.Padding = new Thickness(0, 0, 35, 0);
            Grid.SetRow(border, row);

        }

        private TextBox GenerateTextBox(string textboxname, int row, int column, bool biggersize = false, HorizontalAlignment horizontalAlignment = default)
        {
            TextBox TextBoxName = new TextBox();
            TextBoxName.Width = biggersize ? 250 : 120;
            if (biggersize){TextBoxName.Height = 130;}

            TextBoxName.Margin = new Thickness(0, 2, 6, 2);
            TextBoxName.HorizontalAlignment = horizontalAlignment == default ? HorizontalAlignment.Right : horizontalAlignment;

            string TrimmedText = String.Concat(textboxname.Where(c => !Char.IsWhiteSpace(c)));

            TextBoxName.SetValue(FrameworkElement.NameProperty, TrimmedText + "_Textbox");
            Grid.SetRow(TextBoxName, row);
            Grid.SetColumn(TextBoxName, column);

            return TextBoxName;
        }

        private TextBlock GenerateTextBlock(string text, int row, int column, string foregroundcolor = default, HorizontalAlignment horizontalAlignment = default)
        {
            TextBlock TextBlockName = new TextBlock();
            TextBlockName.Foreground = (Brush)Converter.ConvertFromString(foregroundcolor == default ? "#FFEDF5FD" : foregroundcolor);
            TextBlockName.FontFamily = new FontFamily("Arial Black");
            TextBlockName.FontWeight = FontWeights.Bold;
            TextBlockName.Text = text;
            TextBlockName.Margin = new Thickness(0, 2, 0, 2);
            TextBlockName.VerticalAlignment = VerticalAlignment.Center;
            if (horizontalAlignment != default)
            {
                TextBlockName.HorizontalAlignment = horizontalAlignment;
            }
            Grid.SetRow(TextBlockName, row);
            Grid.SetColumn(TextBlockName, column);

            return TextBlockName;
        }
    }
}
