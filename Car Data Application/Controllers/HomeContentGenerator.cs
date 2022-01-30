using Car_Data_Application.Models;
using Car_Data_Application.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Car_Data_Application.Models.Vehicle_Classes;
using System;
using Car_Data_Application.Models.XML_Models;

namespace Car_Data_Application.Controllers
{
    class HomeContentGenerator : CarDataAppController
    {
        public void GeneratorHomeContent(MainWindow mw, User user, HomePage homePage)
        {
            InitialAssignValue(mw, user);
           
            Grid Grid = new Grid();
            for (int i = 0; i < 4; i++) // 4 is number of displays blocks with data
            {
                RowDefinition row = new RowDefinition();
                Grid.RowDefinitions.Add(row);
            }

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
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.MainGrid.Children[3]).Children);
        }

        private Border FuelDataGenerator(User user, FuelData translation)
        {
            Border FuelDataBorder = new Border();
            SetBorderProps(ref FuelDataBorder, 0);

            Grid FuelDataGrid = new Grid();
            FuelDataBorder.Padding = new Thickness(20);
            FuelDataBorder.Child = FuelDataGrid;
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

            FuelDataGrid.Children.Add(GenerateIcon("../../../Images/Icons/fuelicon.png", 0, 1));

            switch (PUser.UserLanguage)
            {
                case "PL":
                    FuelDataGrid.Children.Add(GenerateTextBlock(translation.AverageConsumption.PL, 1, 0));
                    FuelDataGrid.Children.Add(GenerateTextBlock(translation.LastConsumption.PL, 2, 0));
                    FuelDataGrid.Children.Add(GenerateTextBlock(translation.LastFuelPrice.PL, 3, 0));
                    break;

                case "ENG":
                    FuelDataGrid.Children.Add(GenerateTextBlock(translation.AverageConsumption.ENG, 1, 0));
                    FuelDataGrid.Children.Add(GenerateTextBlock(translation.LastConsumption.ENG, 2, 0));
                    FuelDataGrid.Children.Add(GenerateTextBlock(translation.LastFuelPrice.ENG, 3, 0));
                    break;
            }

            FuelDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].AverageFuelConsumption.ToString() + " L/100km", 1, 2));

            FuelDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].Refulings[LastRefuelingElement - 1].LatestConsumption.ToString() + " L/100km", 2, 2));

            FuelDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].Refulings[LastRefuelingElement - 1].LatestFuelPrice.ToString() + " zł", 3, 2));

            return FuelDataBorder;
        }

        private Border CostDataGenerator(User user, CostData translation)
        {
            Border CostDataBorder = new Border();
            SetBorderProps(ref CostDataBorder, 1);

            Grid CostDataGrid = new Grid();
            CostDataBorder.Padding = new Thickness(20);
            CostDataBorder.Child = CostDataGrid;
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

            CostDataGrid.Children.Add(GenerateIcon("../../../Images/Icons/cost.png", 0, 1));

            switch (PUser.UserLanguage)
            {
                case "PL":
                    CostDataGrid.Children.Add(GenerateTextBlock(translation.ThisMounth.PL, 1, 0));
                    CostDataGrid.Children.Add(GenerateTextBlock(translation.ThisMounthFuelCost.PL, 2, 2));
                    CostDataGrid.Children.Add(GenerateTextBlock(translation.ThisMounthOtherCost.PL, 3, 2));
                    CostDataGrid.Children.Add(GenerateTextBlock(translation.PreviousMounth.PL, 4, 0));
                    CostDataGrid.Children.Add(GenerateTextBlock(translation.PreviousMounthFuelCost.PL, 5, 2));
                    CostDataGrid.Children.Add(GenerateTextBlock(translation.PreviousMounthOtherCost.PL, 6, 2));
                    break;

                case "ENG":
                    CostDataGrid.Children.Add(GenerateTextBlock(translation.ThisMounth.ENG, 1, 0));
                    CostDataGrid.Children.Add(GenerateTextBlock(translation.ThisMounthFuelCost.ENG, 2, 2));
                    CostDataGrid.Children.Add(GenerateTextBlock(translation.ThisMounthOtherCost.ENG, 3, 2));
                    CostDataGrid.Children.Add(GenerateTextBlock(translation.PreviousMounth.ENG, 4, 0));
                    CostDataGrid.Children.Add(GenerateTextBlock(translation.PreviousMounthFuelCost.ENG, 5, 2));
                    CostDataGrid.Children.Add(GenerateTextBlock(translation.PreviousMounthOtherCost.ENG, 6, 2));
                    break;
            }

            CostDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].ThisMounthFuelCost.ToString() + " zł", 2, 1));

            CostDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].ThisMounthOtherCost.ToString() + " zł", 3, 1));

            CostDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].PreviousMounthFuelCost.ToString() + " zł", 5, 1));

            CostDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].PreviousMounthOtherCost.ToString() + " zł", 6, 1));

            return CostDataBorder;
        }

        private TextBlock EntriesListText(XMLEntriesList translation)
        {
            string EntriesListTitle = string.Empty;
            switch (PUser.UserLanguage)
            {
                case "PL":
                    EntriesListTitle = translation.EntriesListText.PL;
                    break;

                case "ENG":
                    EntriesListTitle = translation.EntriesListText.ENG;
                    break;
            }
            TextBlock EntriesListText = GenerateTextBlock(EntriesListTitle, 2, 0, "#FF407BB6", HorizontalAlignment.Center);
            EntriesListText.FontSize = 34;
            EntriesListText.Margin = new Thickness(0,25,0,0);

            return EntriesListText;
        }

        private Border EnteriesListGenerator (User user, XMLEntriesList translation)
        {
            Border MainBorder = new Border();
            SetBorderProps(ref MainBorder, 3, false, "#FFEDF5FD", "#FF7DB5EC");
            MainBorder.MaxHeight = 200;

            ScrollViewer DataViewer = new ScrollViewer();

            Grid AuxiliaryGrid = new Grid();

            int index = 0;
            foreach (EntriesList entries in user.Vehicles[user.ActiveCarIndex].EntriesList)
            {
                RowDefinition AuxiliaryGridGridRow = new RowDefinition();
                AuxiliaryGrid.RowDefinitions.Add(AuxiliaryGridGridRow);

                Border EnteriesListBorder = new Border();
                SetBorderProps(ref EnteriesListBorder, index);

                Grid EnteriesListGrid = new Grid();
                EnteriesListBorder.Padding = new Thickness(20);
                EnteriesListBorder.Child = EnteriesListGrid;
                for (int i = 0; i < 3; i++) // 3 is number of columns
                {
                    ColumnDefinition EnteriesListGridColumn = new ColumnDefinition();
                    EnteriesListGrid.ColumnDefinitions.Add(EnteriesListGridColumn);
                }
                for (int x = 0; x < 4; x++) // 4 is number of rows
                {
                    RowDefinition EnteriesListGridRow = new RowDefinition();
                    EnteriesListGrid.RowDefinitions.Add(EnteriesListGridRow);
                }

                switch (PUser.UserLanguage)
                {
                    case "PL":
                        EnteriesListGrid.Children.Add(GenerateTextBlock(translation.Date.PL, 1, 0));
                        EnteriesListGrid.Children.Add(GenerateTextBlock(translation.Price.PL, 2, 0));
                        EnteriesListGrid.Children.Add(GenerateTextBlock(translation.Descryption.PL, 3, 0));
                        break;

                    case "ENG":
                        EnteriesListGrid.Children.Add(GenerateTextBlock(translation.Date.ENG, 1, 0));
                        EnteriesListGrid.Children.Add(GenerateTextBlock(translation.Price.ENG, 2, 0));
                        EnteriesListGrid.Children.Add(GenerateTextBlock(translation.Descryption.ENG, 3, 0));
                        break;
                }

                EnteriesListGrid.Children.Add(GenerateTextBlock(entries.Type.ToString(), 0, 1));

                EnteriesListGrid.Children.Add(GenerateTextBlock(entries.Date.ToString(), 1, 2));

                EnteriesListGrid.Children.Add(GenerateTextBlock(entries.Price.ToString() + " Zł", 2, 2));

                EnteriesListGrid.Children.Add(GenerateTextBlock(entries.Descryption.ToString(), 3, 2));

                AuxiliaryGrid.Children.Add(EnteriesListBorder);
                index++;
            }

            DataViewer.Content = AuxiliaryGrid;
            MainBorder.Child = DataViewer;

            return MainBorder;
        }

        private void SetBorderProps(ref Border border, int row, bool transparentborder = false, string backgroundcolor = default, string bordercolor = default)
        {
            Brush BackgroundBrushh = (Brush)Converter.ConvertFromString(backgroundcolor == default ? "#FF001A34" : backgroundcolor);

            border.Background = BackgroundBrushh;
            border.BorderThickness = new Thickness(5);

            border.BorderBrush = (Brush)Converter.ConvertFromString(bordercolor == default ? "#FF407BB6" : bordercolor);
            if (transparentborder == true)
            {
                border.BorderBrush = Brushes.Transparent;
            }

            border.CornerRadius = new CornerRadius(30);

            border.Margin = new Thickness(15, 5, 15, 5);
            border.Padding = new Thickness(0, 0, 35, 0);
            Grid.SetRow(border, row);

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
            //if (horizontalAlignment != default)
            //{
            //    TextBlockName.HorizontalAlignment = horizontalAlignment;
            //}
            TextBlockName.HorizontalAlignment = horizontalAlignment;

            Grid.SetRow(TextBlockName, row);
            Grid.SetColumn(TextBlockName, column);

            return TextBlockName;
        }

        private Image GenerateIcon(string path, int row, int column)
        {
            Image Icon = new Image();
            ImageSourceConverter source = new ImageSourceConverter();
            Icon.SetValue(Image.SourceProperty, source.ConvertFromString(@path));
            Icon.Width = 30;
            Grid.SetRow(Icon, row);
            Grid.SetColumn(Icon, column);

            return Icon;
        }
    }
}
