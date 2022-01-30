using Car_Data_Application.Views;
using Car_Data_Application.Models;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.IO;
using Car_Data_Application.Models.XML_Models;

namespace Car_Data_Application.Controllers
{
    class VehicleDetailContentGenerator : CarDataAppController
    {
        private int ActualMainGridRow = new int();

        public void GeneratorVehicleDetail(MainWindow mainwindow, Vehicle vehicle, User user, VehiclesPage vehiclesPage)
        {
            InitialAssignValue(mainwindow, user);

            Grid MainGrid = new Grid();

            for (int i = 0; i < 6; i++) // 6 is number of rows
            {
                RowDefinition MainGridRow = new RowDefinition();
                MainGrid.RowDefinitions.Add(MainGridRow);
            }

            MainGrid.Children.Add(DisplayVehicleImage(vehicle));
            MainGrid.Children.Add(GenarateVehicleNameGrid(vehicle, vehiclesPage.VehicleNameGrid));
            MainGrid.Children.Add(GeneratePrimaryInfoGrid(vehicle, vehiclesPage.PrimaryInfoGrid));
            MainGrid.Children.Add(GenarateFuelTankInfoGrid(vehicle, vehiclesPage.FuelTankInfoGrid));
            MainGrid.Children.Add(GenerateCyclicalCostGrid(vehicle, vehiclesPage.CyclicalCostGrid));


            mainwindow.ScrollViewerContent.Content = MainGrid;
        }

        private void InitialAssignValue(MainWindow mainwindow, User user)
        {
            PUser = user;
        }

        private Border DisplayVehicleImage(Vehicle vehicle)
        {
            ActualMainGridRow = 0;

            Border ImageBorder = new Border();
            ImageBorder.BorderThickness = new Thickness(5);
            ImageBorder.Margin = new Thickness(15, 5, 15, 5);
            ImageBorder.BorderBrush = (Brush)Converter.ConvertFrom("#FF407BB6");

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

        private Border GenarateVehicleNameGrid(Vehicle vehicle, VehicleNameGrid translation)
        {
            ActualMainGridRow = 1;

            Border VehicleNameBorder = new Border();
            SetBorderProps(ref VehicleNameBorder, ActualMainGridRow);

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
            switch (PUser.UserLanguage)
            {
                case "PL":
                    vehicleNameGrid.Children.Add(GenerateTextBlock(translation.Brand.PL, 0, 0));
                    vehicleNameGrid.Children.Add(GenerateTextBlock(translation.Model.PL, 1, 0));
                    break;
                case "ENG":
                    vehicleNameGrid.Children.Add(GenerateTextBlock(translation.Brand.ENG, 0, 0));
                    vehicleNameGrid.Children.Add(GenerateTextBlock(translation.Model.ENG, 1, 0));
                    break;
            }

            vehicleNameGrid.Children.Add(GenerateTextBlock(vehicle.Brand, 0, 1));

            vehicleNameGrid.Children.Add(GenerateTextBlock(vehicle.Model, 1, 1));

            return VehicleNameBorder;
        }

        private Border GeneratePrimaryInfoGrid(Vehicle vehicle, PrimaryInfoGrid translation)
        {
            ActualMainGridRow = 2;

            Border PrimaryInfoBorder = new Border();
            SetBorderProps(ref PrimaryInfoBorder, ActualMainGridRow);

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

            switch (PUser.UserLanguage)
            {
                case "PL":
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.YearOfManufacture.PL, 0, 0));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Vin.PL, 1, 0));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Plates.PL, 2, 0));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Millage.PL, 3, 0));
                    break;
                case "ENG":
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.YearOfManufacture.ENG, 0, 0));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Vin.ENG, 1, 0));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Plates.ENG, 2, 0));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Millage.ENG, 3, 0));
                    break;
            }


            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(vehicle.YearOfManufacture.ToString(), 0, 1));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(vehicle.Vin.ToString(), 1, 1));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(vehicle.Plates.ToString(), 2, 1));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(vehicle.CarMillage.ToString(), 3, 1));

            return PrimaryInfoBorder;
        }

        private Border GenarateFuelTankInfoGrid(Vehicle vehicle, FuelTankInfoGrid translation)
        {
            ActualMainGridRow = 3;

            Border FuelInfoBorder = new Border();
            SetBorderProps(ref FuelInfoBorder, ActualMainGridRow);

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

            switch (PUser.UserLanguage)
            {
                case "PL":
                    FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Gasoline.PL, 0, 0));
                    FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Diesel.PL, 1, 0));
                    FuelInfoGrid.Children.Add(GenerateTextBlock(translation.LPG.PL, 2, 0));
                    break;
                case "ENG":
                    FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Gasoline.ENG, 0, 0));
                    FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Diesel.ENG, 1, 0));
                    FuelInfoGrid.Children.Add(GenerateTextBlock(translation.LPG.ENG, 2, 0));
                    break;
            }


            FuelInfoGrid.Children.Add(GenerateTextBlock(vehicle.Tanks.Gasoline.ToString(), 0, 1));

            FuelInfoGrid.Children.Add(GenerateTextBlock(vehicle.Tanks.Diesel.ToString(), 1, 1));

            FuelInfoGrid.Children.Add(GenerateTextBlock(vehicle.Tanks.LPG.ToString(), 2, 1));

            return FuelInfoBorder;
        }

        private Border GenerateCyclicalCostGrid(Vehicle vehicle, CyclicalCostGrid translation)
        {
            ActualMainGridRow = 4;

            Border CyclicalCostBorder = new Border();
            SetBorderProps(ref CyclicalCostBorder, ActualMainGridRow);

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

            switch (PUser.UserLanguage)
            {
                case "PL":
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceStartDate.PL, 0, 0));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceEndDate.PL, 1, 0));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsurancePrice.PL, 2, 0));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionStartDate.PL, 4, 0));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate.PL, 5, 0));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate.PL, 6, 0));
                    break;
                case "ENG":
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceStartDate.ENG, 0, 0));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceEndDate.ENG, 1, 0));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsurancePrice.ENG, 2, 0));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionStartDate.ENG, 4, 0));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate.ENG, 5, 0));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate.ENG, 6, 0));
                    break;
            }


            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Insurance.StartDate.ToString(), 0, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Insurance.EndDate.ToString(), 1, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Insurance.Price.ToString() + " zł", 2, 1));

            //============================DIVIDE ELEMENTS=======================================

            CyclicalCostGrid.Children.Add(GenerateTextBlock("", 3, 0));

            CyclicalCostGrid.Children.Add(GenerateTextBlock("", 3, 1));

            //===================================================================================


            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Inspection.StartDate.ToString(), 4, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Inspection.EndDate.ToString(), 5, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Inspection.Price.ToString() + " zł", 6, 1));

            return CyclicalCostBorder;
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

        private void SetBorderProps(ref Border border, int row)
        {
            Brush BackgroundBrushh = (Brush)Converter.ConvertFromString("#FF001A34");
            border.Background = BackgroundBrushh;

            border.BorderThickness = new Thickness(5);
            border.BorderBrush = (Brush)Converter.ConvertFrom("#FF407BB6");
            border.CornerRadius = new CornerRadius(30);

            border.Margin = new Thickness(15,5,15,5);
            border.Padding = new Thickness(0, 0, 35, 0);
            Grid.SetRow(border, row);
        }
    }
}
