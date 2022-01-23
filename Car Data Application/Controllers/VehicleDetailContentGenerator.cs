using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Data_Application.Views;
using Car_Data_Application.Models;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Text.Json;

namespace Car_Data_Application.Controllers
{
    class VehicleDetailContentGenerator
    {
        private BrushConverter converter = new BrushConverter();
        private int ActualMainGridRow = new int();

        public void GeneratorVehicleDetail(MainWindow mainwindow, Vehicle vehicle)
        {
            mainwindow.AddButon.Visibility = Visibility.Hidden;

            Grid MainGrid = new Grid();

            for (int i = 0; i <= 6; i++) // 6 is number of displays blocks with car data
            {
                RowDefinition MainGridRow = new RowDefinition();
                MainGrid.RowDefinitions.Add(MainGridRow);
            }

            MainGrid.Children.Add(DisplayVehicleImage(vehicle));
            MainGrid.Children.Add(GenarateVehicleNameGrid(vehicle));
            MainGrid.Children.Add(GeneratePrimaryInfoGrid(vehicle));
            MainGrid.Children.Add(GenarateFuelTankInfoGrid(vehicle));
            MainGrid.Children.Add(GenerateCyclicalCostGrid(vehicle));


            mainwindow.ScrollViewerContent.Content = MainGrid;
        }

        public Border DisplayVehicleImage(Vehicle vehicle)
        {
            ActualMainGridRow = 0;

            Border ImageBorder = new Border();
            ImageBorder.BorderThickness = new Thickness(5);
            ImageBorder.Margin = new Thickness(15, 5, 15, 5);
            ImageBorder.BorderBrush = (Brush)converter.ConvertFrom("#FF407BB6");

            Image image = new Image();
            ImageSourceConverter source = new ImageSourceConverter();
            image.Stretch = Stretch.UniformToFill;
            image.VerticalAlignment = VerticalAlignment.Center;
            ImageBorder.MaxHeight = 280;


            if (File.Exists(@"..\..\..\Images\UserPictures\" + vehicle.PictureFileName))
            {
                image.SetValue(Image.SourceProperty, source.ConvertFromString(@"..\..\..\Images\UserPictures\" + vehicle.PictureFileName));
            }
            else
            {
                image.SetValue(Image.SourceProperty, source.ConvertFromString(@"..\..\..\Images\defaultcaricon.png"));
            }

            ImageBorder.Child = image;

            return ImageBorder;
        }

        public Border GenarateVehicleNameGrid(Vehicle vehicle)
        {
            ActualMainGridRow = 1;

            Border VehicleNameBorder = new Border();
            GenerateBorderProps(ref VehicleNameBorder, ActualMainGridRow);

            Grid vehicleNameGrid = new Grid();
            VehicleNameBorder.Padding = new Thickness(20);
            VehicleNameBorder.Child = vehicleNameGrid;

            for (int i = 0; i < 2; i++)
            {
                ColumnDefinition vehiclenamecolumn = new ColumnDefinition();
                RowDefinition vehiclenamerow = new RowDefinition();
                vehicleNameGrid.RowDefinitions.Add(vehiclenamerow);
                vehicleNameGrid.ColumnDefinitions.Add(vehiclenamecolumn);
            }

            vehicleNameGrid.Children.Add(GenerateTextBlock("Marka:" , 0, 0));

            vehicleNameGrid.Children.Add(GenerateTextBlock(vehicle.Brand, 0, 1));

            vehicleNameGrid.Children.Add(GenerateTextBlock("Model:", 1, 0));

            vehicleNameGrid.Children.Add(GenerateTextBlock(vehicle.Model, 1, 1));

            return VehicleNameBorder;
        }

        public Border GeneratePrimaryInfoGrid(Vehicle vehicle)
        {
            ActualMainGridRow = 2;

            Border PrimaryInfoBorder = new Border();
            GenerateBorderProps(ref PrimaryInfoBorder, ActualMainGridRow);

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

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock("Rok produkcji:",0,0));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(vehicle.YearOfManufacture.ToString(), 0, 1));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock("VIN:", 1, 0));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(vehicle.Vin.ToString(), 1, 1));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock("Tablice Rejestracyjne:", 2, 0));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(vehicle.Plates.ToString(), 2, 1));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock("Przebieg:", 3, 0));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(vehicle.CarMillage.ToString(), 3, 1));

            return PrimaryInfoBorder;
        }

        public Border GenarateFuelTankInfoGrid(Vehicle vehicle)
        {
            ActualMainGridRow = 3;

            Border FuelInfoBorder = new Border();
            GenerateBorderProps(ref FuelInfoBorder, ActualMainGridRow);

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

            FuelInfoGrid.Children.Add(GenerateTextBlock("Pojemność baku Paliwa:",0,0));

            FuelInfoGrid.Children.Add(GenerateTextBlock(vehicle.Tanks.Gasoline.ToString(), 0, 1));

            FuelInfoGrid.Children.Add(GenerateTextBlock("Pojemność baku Diesel:", 1, 0));

            FuelInfoGrid.Children.Add(GenerateTextBlock(vehicle.Tanks.Diesel.ToString(), 1, 1));

            FuelInfoGrid.Children.Add(GenerateTextBlock("Pojemność baku LPG:", 2, 0));

            FuelInfoGrid.Children.Add(GenerateTextBlock(vehicle.Tanks.LPG.ToString(), 2, 1));

            return FuelInfoBorder;
        }

        public Border GenerateCyclicalCostGrid(Vehicle vehicle)
        {
            ActualMainGridRow = 4;

            Border CyclicalCostBorder = new Border();
            GenerateBorderProps(ref CyclicalCostBorder, ActualMainGridRow);

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

            CyclicalCostGrid.Children.Add(GenerateTextBlock("Rozpoczęcie okresu ubezpieczenia:",0,0));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Insurance.StartDate.ToString(), 0, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock("Koniec okresu ubezpieczenia:", 1, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Insurance.EndDate.ToString(), 1, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock("Koszt odecnego ubezpieczenia:", 2, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Insurance.Price.ToString() + " zł", 2, 1));

            //============================DIVIDE ELEMENTS=======================================

            CyclicalCostGrid.Children.Add(GenerateTextBlock("", 3, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBlock("", 3, 1));

            //===================================================================================

            CyclicalCostGrid.Children.Add(GenerateTextBlock("Data wykonania przeglądu technicznego", 4, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Inspection.StartDate.ToString(), 4, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock("Koniec ważności przeglądu technicznego", 5, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Inspection.EndDate.ToString(), 5, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock("Koszt wykonania badania technicznego", 6, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Inspection.Price.ToString() + " zł", 6, 1));

            return CyclicalCostBorder;
        }



        public TextBlock GenerateTextBlock(string text, int row, int column)
        {
            TextBlock TextBlockName = new TextBlock();
            TextBlockName.Foreground = (Brush)converter.ConvertFromString("#FFEDF5FD");
            TextBlockName.FontFamily = new FontFamily("Arial Black");
            TextBlockName.FontWeight = FontWeights.Bold;
            TextBlockName.Text = text;
            TextBlockName.Margin = new Thickness(0, 2, 0, 2);
            Grid.SetRow(TextBlockName, row);
            Grid.SetColumn(TextBlockName, column);

            if (column == 1)
            {
                TextBlockName.HorizontalAlignment = HorizontalAlignment.Right;
            }
            if (ActualMainGridRow == 1) 
            {
                TextBlockName.HorizontalAlignment = HorizontalAlignment.Center;
            }

            return TextBlockName;
        }

        public void GenerateBorderProps(ref Border border, int row)
        {
            Brush BackgroundBrushh = (Brush)converter.ConvertFromString("#FF001A34");
            border.Background = BackgroundBrushh;

            border.BorderThickness = new Thickness(5);
            border.BorderBrush = (Brush)converter.ConvertFrom("#FF407BB6");
            border.CornerRadius = new CornerRadius(30);

            border.Margin = new Thickness(15,5,15,5);
            border.Padding = new Thickness(0, 0, 35, 0);
            Grid.SetRow(border, row);
        }
    }
}
