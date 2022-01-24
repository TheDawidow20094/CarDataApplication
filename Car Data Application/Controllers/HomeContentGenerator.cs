using Car_Data_Application.Models;
using Car_Data_Application.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Car_Data_Application.Models.Vehicle_Classes;

namespace Car_Data_Application.Controllers
{
    class HomeContentGenerator
    {
        private MainWindow mainWindow;
        private BrushConverter Converter = new BrushConverter();

        public void GeneratorHomeContent(MainWindow mv, User user)
        {
            mainWindow = mv;
            mainWindow.WhereAreYou = "HomePage";
            mainWindow.AddButon.Visibility = Visibility.Hidden;
            new CarDataAppController().SetButtonColor("HomePageButton", mainWindow.SidePanel.Children);

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

        public Border FuelDataGenerator(User user)
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

            FuelDataGrid.Children.Add(GenerateIcon("../../../Images/Icons/fuelicon.png", 0 , 1));

            FuelDataGrid.Children.Add(GenerateTextBlock("Średnie spalanie:" , 1, 0));

            FuelDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].AverageFuelConsumption.ToString() + " L/100km", 1, 2));

            FuelDataGrid.Children.Add(GenerateTextBlock("Ostatnie spalanie:", 2, 0));

            FuelDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].Refulings[LastRefuelingElement - 1].LatestConsumption.ToString() + " L/100km", 2, 2));

            FuelDataGrid.Children.Add(GenerateTextBlock("Ostatnia cena paliwa:", 3, 0));

            FuelDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].Refulings[LastRefuelingElement - 1].LatestFuelPrice.ToString() + " zł", 3, 2));

            return FuelDataBorder;
        }

        public Border CostDataGenerator(User user)
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

            CostDataGrid.Children.Add(GenerateTextBlock("Ten miesiąc:", 1, 0));

            CostDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].ThisMounthFuelCost.ToString() + " zł", 2, 1));

            CostDataGrid.Children.Add(GenerateTextBlock("Paliwo:", 2, 2));

            CostDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].ThisMounthOtherCost.ToString() + " zł", 3, 1));

            CostDataGrid.Children.Add(GenerateTextBlock("Pozostałe koszta", 3, 2));

            CostDataGrid.Children.Add(GenerateTextBlock("Poprzedni miesiąc", 4, 0));

            CostDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].PreviousMounthFuelCost.ToString() + " zł", 5, 1));

            CostDataGrid.Children.Add(GenerateTextBlock("Paliwo:", 5, 2));

            CostDataGrid.Children.Add(GenerateTextBlock(user.Vehicles[user.ActiveCarIndex].PreviousMounthOtherCost.ToString() + " zł", 6, 1));

            CostDataGrid.Children.Add(GenerateTextBlock("Pozostałe koszta", 6, 2));

            return CostDataBorder;
        }

        public ScrollViewer EnteriesListGenerator (User user)
        {
            Border EnteriesListBorder = new Border();
            SetBorderProps(ref EnteriesListBorder, 2);

            ScrollViewer DataViewer = new ScrollViewer();
            Grid.SetRow(DataViewer, 2);

            foreach (EntriesList entries in user.Vehicles[user.ActiveCarIndex].EntriesList)
            {
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

                DataViewer.Content = EnteriesListBorder;

                EnteriesListGrid.Children.Add(GenerateTextBlock(entries.Type.ToString(), 0, 1));

                EnteriesListGrid.Children.Add(GenerateTextBlock("Data:", 1, 0));

                EnteriesListGrid.Children.Add(GenerateTextBlock(entries.Date.ToString(), 1, 2));

                EnteriesListGrid.Children.Add(GenerateTextBlock("Koszt:", 2, 0));

                EnteriesListGrid.Children.Add(GenerateTextBlock(entries.Price.ToString() + " zł", 2, 2));

                EnteriesListGrid.Children.Add(GenerateTextBlock("Opis:", 3, 0));

                EnteriesListGrid.Children.Add(GenerateTextBlock(entries.Descryption.ToString(), 3, 2));

            }

            return DataViewer;
        }


        public void SetBorderProps(ref Border border, int row)
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

        public TextBlock GenerateTextBlock(string text, int row, int column)
        {
            TextBlock TextBlockName = new TextBlock();
            TextBlockName.Foreground = (Brush)Converter.ConvertFromString("#FFEDF5FD");
            TextBlockName.FontFamily = new FontFamily("Arial Black");
            TextBlockName.FontWeight = FontWeights.Bold;
            TextBlockName.Text = text;
            TextBlockName.Margin = new Thickness(0, 2, 0, 2);
            TextBlockName.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(TextBlockName, row);
            Grid.SetColumn(TextBlockName, column);

            return TextBlockName;
        }

        public Image GenerateIcon(string path, int row, int column)
        {
            Image Icon = new Image();
            ImageSourceConverter source = new ImageSourceConverter();
            Icon.SetValue(Image.SourceProperty, source.ConvertFromString(@path));
            Grid.SetRow(Icon, row);
            Grid.SetColumn(Icon, column);

            return Icon;
        }
    }
}
