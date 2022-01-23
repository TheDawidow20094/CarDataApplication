using Car_Data_Application.Models;
using Car_Data_Application.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Car_Data_Application.Controllers
{
    class AddVehiclePageGenerator
    {
        private BrushConverter converter = new BrushConverter();
        private MainWindow mainwindow;

        public void PageGenerator(MainWindow mw, User user)
        {
            mainwindow = mw;
            mainwindow.AddButon.Visibility = Visibility.Hidden;

            Grid MainGrid = new Grid();

            for (int i = 0; i <= 6; i++) // 6 number of rows
            {
                RowDefinition MainGridRow = new RowDefinition();
                MainGrid.RowDefinitions.Add(MainGridRow);
            }

            //MainGrid.Children.Add(AddingPictureBorder());
            MainGrid.Children.Add(AddingVehiclePrimaryDataBorder());
            MainGrid.Children.Add(AddingPrimaryInfoBorder());
            MainGrid.Children.Add(AddingFuelTankInfoBorder());
            MainGrid.Children.Add(AddingCyclicalCostBorder());


            mainwindow.ScrollViewerContent.Content = MainGrid;

        }

        public Border AddingPictureBorder()
        {
            return null; //napis
        }

        public Grid AddingVehiclePrimaryDataBorder()
        {
            Grid VehiclePrimaryDataGrid = new Grid();
            VehiclePrimaryDataGrid.Margin = new Thickness(0, 5, 0, 5);
            Grid.SetRow(VehiclePrimaryDataGrid, 1);

            for (int i = 0; i < 2; i++) // 2 number of column
            {
                ColumnDefinition VehiclePrimaryDataColumn = new ColumnDefinition();
                VehiclePrimaryDataGrid.ColumnDefinitions.Add(VehiclePrimaryDataColumn);
            }

            Grid AddImageGrid = new Grid();

            Rectangle AddImageRectangle = new Rectangle();
            AddImageRectangle.Margin = new Thickness(0, 0, 15, 0);
            AddImageRectangle.Stroke = (Brush)converter.ConvertFromString("#FF6D90B4");
            AddImageRectangle.StrokeThickness = 4;
            AddImageRectangle.StrokeDashArray = new DoubleCollection() { 4 };

            TextBlock AddPictureText = new TextBlock();
            AddPictureText.Text = "+";
            AddPictureText.HorizontalAlignment = HorizontalAlignment.Center;
            AddPictureText.VerticalAlignment = VerticalAlignment.Center;
            AddPictureText.FontSize = 70;
            AddPictureText.FontWeight = FontWeights.UltraBold;
            AddPictureText.Foreground = (Brush)converter.ConvertFromString("#FF6D90B4");


            AddImageGrid.Children.Add(AddImageRectangle);
            AddImageGrid.Children.Add(AddPictureText);

            Grid.SetColumn(AddImageGrid, 1);

            Border VehicleNameBorder = new Border();
            Grid.SetColumn(VehicleNameBorder, 0);
            GenerateBorderProps(ref VehicleNameBorder, 1);

            Grid VehicleNameGrid = new Grid();
            VehicleNameBorder.Padding = new Thickness(20);
            VehicleNameBorder.Child = VehicleNameGrid;

            for (int i = 0; i < 2; i++)
            {
                ColumnDefinition VehicleNameGridColumn = new ColumnDefinition();
                RowDefinition VehicleNameGridRow = new RowDefinition();
                VehicleNameGrid.RowDefinitions.Add(VehicleNameGridRow);
                VehicleNameGrid.ColumnDefinitions.Add(VehicleNameGridColumn);
            }

            VehicleNameGrid.Children.Add(GenerateTextBlock("Marka:", 0, 0));

            VehicleNameGrid.Children.Add(GenerateTextBox("VehicleBrand" , 0, 1));

            VehicleNameGrid.Children.Add(GenerateTextBlock("Model:", 1, 0));

            VehicleNameGrid.Children.Add(GenerateTextBox("VehicleModel", 1, 1));

            VehiclePrimaryDataGrid.Children.Add(VehicleNameBorder);
            VehiclePrimaryDataGrid.Children.Add(AddImageGrid);

            return VehiclePrimaryDataGrid;
        }

        public Border AddingPrimaryInfoBorder()
        {
            Border PrimaryInfoBorder = new Border();
            GenerateBorderProps(ref PrimaryInfoBorder, 2);

            Grid PrimarmaryInfoGrid = new Grid();
            PrimaryInfoBorder.Padding = new Thickness(20);
            PrimaryInfoBorder.Child = PrimarmaryInfoGrid;

            for (int i = 0; i < 2; i++) // 2 number of columns
            {
                ColumnDefinition PrimaryInfoGridColumn = new ColumnDefinition();
                PrimarmaryInfoGrid.ColumnDefinitions.Add(PrimaryInfoGridColumn);
            }
            for (int i = 0; i <= 4; i++) // 4 number of rows
            {
                RowDefinition PrimarmaryInfoGridRow = new RowDefinition();
                PrimarmaryInfoGrid.RowDefinitions.Add(PrimarmaryInfoGridRow);
            }

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock("Rok produkcji:", 0, 0));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBox("YearOfManufacture", 0, 1));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock("VIN:", 1, 0));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBox("VIN", 1, 1));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock("Tablice Rejestracyjne:", 2, 0));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBox("Plates", 2, 1));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock("Przebieg:", 3, 0));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBox("CarMillage", 3, 1));

            return PrimaryInfoBorder;
        }

        public Border AddingFuelTankInfoBorder()
        {
            Border FuelInfoBorder = new Border();
            GenerateBorderProps(ref FuelInfoBorder, 3);

            Grid FuelInfoGrid = new Grid();
            FuelInfoBorder.Child = FuelInfoGrid;
            FuelInfoBorder.Padding = new Thickness(20);

            for (int i = 0; i < 2; i++) // 2 number of columns
            {
                ColumnDefinition FuelInfoGridColumn = new ColumnDefinition();
                FuelInfoGrid.ColumnDefinitions.Add(FuelInfoGridColumn);
            }
            for (int i = 0; i <= 3; i++) //3 number of rows
            {
                RowDefinition FuelInfGridRow = new RowDefinition();
                FuelInfoGrid.RowDefinitions.Add(FuelInfGridRow);
            }

            FuelInfoGrid.Children.Add(GenerateTextBlock("Pojemność baku Paliwa:", 0, 0));

            FuelInfoGrid.Children.Add(GenerateTextBox("GasolineTank", 0, 1));

            FuelInfoGrid.Children.Add(GenerateTextBlock("Pojemność baku Diesel:", 1, 0));

            FuelInfoGrid.Children.Add(GenerateTextBox("DieselTank", 1, 1));

            FuelInfoGrid.Children.Add(GenerateTextBlock("Pojemność baku LPG:", 2, 0));

            FuelInfoGrid.Children.Add(GenerateTextBox("LPGTank", 2, 1));

            return FuelInfoBorder;
        }

        public Border AddingCyclicalCostBorder()
        {
            Border CyclicalCostBorder = new Border();
            GenerateBorderProps(ref CyclicalCostBorder, 4);

            Grid CyclicalCostGrid = new Grid();
            CyclicalCostBorder.Padding = new Thickness(20);
            CyclicalCostBorder.Child = CyclicalCostGrid;

            for (int i = 0; i < 2; i++) // numbers of column
            {
                ColumnDefinition CyclicalCostColumn = new ColumnDefinition();
                CyclicalCostGrid.ColumnDefinitions.Add(CyclicalCostColumn);
            }
            for (int i = 0; i <= 7; i++) // 7 number of rows
            {
                RowDefinition CyclicalCostRow = new RowDefinition();
                CyclicalCostGrid.RowDefinitions.Add(CyclicalCostRow);
            }

            CyclicalCostGrid.Children.Add(GenerateTextBlock("Rozpoczęcie okresu ubezpieczenia:", 0, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBox("InsuranceStartDate", 0, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock("Koniec okresu ubezpieczenia:", 1, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBox("InsuranceEndDate", 1, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock("Koszt odecnego ubezpieczenia:", 2, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBox("InsurancePrice", 2, 1));

            //============================DIVIDE ELEMENTS=======================================

            CyclicalCostGrid.Children.Add(GenerateTextBlock("", 3, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBlock("", 3, 1));

            //===================================================================================

            CyclicalCostGrid.Children.Add(GenerateTextBlock("Data wykonania przeglądu technicznego", 4, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBox("InspectionStartDate", 4, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock("Koniec ważności przeglądu technicznego", 5, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBox("InspectionEndDate", 5, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock("Koszt wykonania badania technicznego", 6, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBox("InspectionPrice", 6, 1));

            return CyclicalCostBorder;
        }

        public TextBox GenerateTextBox(string textboxname, int row, int column)
        {
            TextBox TextBoxName = new TextBox();
            TextBoxName.Width = 150;
            TextBoxName.Margin = new Thickness(0, 2, 0, 2);
            TextBoxName.HorizontalAlignment = HorizontalAlignment.Right;
            TextBoxName.SetValue(FrameworkElement.NameProperty, textboxname + "_Textbox");
            Grid.SetRow(TextBoxName, row);
            Grid.SetColumn(TextBoxName, column);
            
            return TextBoxName;
        }

        public TextBlock GenerateTextBlock(string text, int row, int column)
        {
            TextBlock TextBlockName = new TextBlock();
            TextBlockName.Foreground = (Brush)converter.ConvertFromString("#FFEDF5FD");
            TextBlockName.FontFamily = new FontFamily("Arial Black");
            TextBlockName.FontWeight = FontWeights.Bold;
            TextBlockName.Text = text;
            TextBlockName.Margin = new Thickness(0, 2, 0, 2);
            TextBlockName.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(TextBlockName, row);
            Grid.SetColumn(TextBlockName, column);

            return TextBlockName;
        }

        public void GenerateBorderProps(ref Border border, int row)
        {
            Brush BackgroundBrushh = (Brush)converter.ConvertFromString("#FF001A34");
            border.Background = BackgroundBrushh;

            border.BorderThickness = new Thickness(5);
            border.BorderBrush = (Brush)converter.ConvertFrom("#FF407BB6");
            border.CornerRadius = new CornerRadius(30);

            border.Margin = new Thickness(15, 5, 15, 5);
            border.Padding = new Thickness(0, 0, 35, 0);
            Grid.SetRow(border, row);
        }
    }
}
