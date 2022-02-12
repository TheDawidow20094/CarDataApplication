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

            for (int i = 0; i < 5; i++) // 5 is number of rows
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

        private Grid DisplayVehicleImage(Vehicle vehicle)
        {
            ActualMainGridRow = 0;

            Grid ImageGrid = new Grid();
            SetGridProps(ref ImageGrid, ActualMainGridRow);

            Image image = new Image();
            ImageSourceConverter source = new ImageSourceConverter();
            image.Stretch = Stretch.UniformToFill;
            image.VerticalAlignment = VerticalAlignment.Center;
            image.Margin = new Thickness(15);
            ImageGrid.MaxHeight = 280;


            if (File.Exists(@"..\..\..\Images\UserPictures\" + vehicle.PictureFileName))
            {
                image.SetValue(Image.SourceProperty, source.ConvertFromString(@"..\..\..\Images\UserPictures\" + vehicle.PictureFileName));
            }
            else
            {
                image.SetValue(Image.SourceProperty, source.ConvertFromString(@"..\..\..\Images\defaultcaricon.png"));
            }

            ImageGrid.Children.Add(image);

            return ImageGrid;
        }

        private Grid GenarateVehicleNameGrid(Vehicle vehicle, VehicleNameGrid translation)
        {
            ActualMainGridRow = 1;

            Grid vehicleNameGrid = new Grid();
            SetGridProps(ref vehicleNameGrid, ActualMainGridRow);

            for (int i = 0; i < 2; i++)
            {
                ColumnDefinition vehiclenamecolumn = new ColumnDefinition();
                RowDefinition vehiclenamerow = new RowDefinition();
                vehicleNameGrid.RowDefinitions.Add(vehiclenamerow);
                vehicleNameGrid.ColumnDefinitions.Add(vehiclenamecolumn);
            }

            vehicleNameGrid.Children.Add(GenerateTextBlock(translation.Brand, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Right));
            vehicleNameGrid.Children.Add(GenerateTextBlock(translation.Model, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));

            vehicleNameGrid.Children.Add(GenerateTextBlock(null, vehicle.Brand, 0, 1));
            vehicleNameGrid.Children.Add(GenerateTextBlock(null, vehicle.Model, 1, 1));

            return vehicleNameGrid;
        }

        private Grid GeneratePrimaryInfoGrid(Vehicle vehicle, PrimaryInfoGrid translation)
        {
            ActualMainGridRow = 2;

            Grid PrimarmaryInfoGrid = new Grid();
            SetGridProps(ref PrimarmaryInfoGrid, ActualMainGridRow);

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

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.YearOfManufacture, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Right));
            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Vin, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Plates, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right));
            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Millage, PUser.UserLanguage, 3, 0, LightTextColor, HorizontalAlignment.Right));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(null, vehicle.YearOfManufacture.ToString(), 0, 1));
            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(null, vehicle.Vin.ToString(), 1, 1));
            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(null, vehicle.Plates.ToString(), 2, 1));
            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(null, vehicle.CarMillage.ToString(), 3, 1));

            return PrimarmaryInfoGrid;
        }

        private Grid GenarateFuelTankInfoGrid(Vehicle vehicle, FuelTankInfoGrid translation)
        {
            ActualMainGridRow = 3;

            Grid FuelInfoGrid = new Grid();
            SetGridProps(ref FuelInfoGrid, ActualMainGridRow);

            for (int i = 0; i < 2; i++) // 2 number of columns
            {
                FuelInfoGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            FuelInfoGrid.RowDefinitions.Add(new RowDefinition());
            int RowIndex = 0;

            FuelInfoGrid.Children.Add(GenerateTextBlock(translation.FuelTankInfoTitle, PUser.UserLanguage, RowIndex, 0, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));

            if (vehicle.Tanks.Gasoline != 0)
            {
                FuelInfoGrid.RowDefinitions.Add(new RowDefinition());
                RowIndex++;

                FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Gasoline, PUser.UserLanguage, RowIndex, 0, LightTextColor, HorizontalAlignment.Right));

                FuelInfoGrid.Children.Add(GenerateTextBlock(null, vehicle.Tanks.Gasoline.ToString(), RowIndex, 1));
            }

            if (vehicle.Tanks.LPG != 0)
            {
                FuelInfoGrid.RowDefinitions.Add(new RowDefinition());
                RowIndex++;

                FuelInfoGrid.Children.Add(GenerateTextBlock(translation.LPG, PUser.UserLanguage, RowIndex, 0, LightTextColor, HorizontalAlignment.Right));

                FuelInfoGrid.Children.Add(GenerateTextBlock(null, vehicle.Tanks.LPG.ToString(), RowIndex, 1));
            }

            if (vehicle.Tanks.Diesel != 0)
            {
                FuelInfoGrid.RowDefinitions.Add(new RowDefinition());
                RowIndex++;

                FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Diesel, PUser.UserLanguage, RowIndex, 0, LightTextColor, HorizontalAlignment.Right));

                FuelInfoGrid.Children.Add(GenerateTextBlock(null, vehicle.Tanks.Diesel.ToString(), RowIndex, 1));
            }


            return FuelInfoGrid;
        }

        private Grid GenerateCyclicalCostGrid(Vehicle vehicle, CyclicalCostGrid translation)
        {
            ActualMainGridRow = 4;

            Grid CyclicalCostGrid = new Grid();
            SetGridProps(ref CyclicalCostGrid, ActualMainGridRow);

            for (int i = 0; i < 4; i++) // numbers of column and rows
            {
                CyclicalCostGrid.ColumnDefinitions.Add(new ColumnDefinition());
                CyclicalCostGrid.RowDefinitions.Add(new RowDefinition());
            }

            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceTitle, PUser.UserLanguage, 0, 0, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceStartDate, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceEndDate, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsurancePrice, PUser.UserLanguage, 3, 0, LightTextColor, HorizontalAlignment.Right));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionTitle, PUser.UserLanguage, 0, 2, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionStartDate, PUser.UserLanguage, 1, 2, LightTextColor, HorizontalAlignment.Right));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate, PUser.UserLanguage, 2, 2, LightTextColor, HorizontalAlignment.Right));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate, PUser.UserLanguage, 3, 2, LightTextColor, HorizontalAlignment.Right));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(null, vehicle.Insurance.StartDate.ToString(), 1, 1));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(null, vehicle.Insurance.EndDate.ToString(), 2, 1));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(null, vehicle.Insurance.Price.ToString() + " zł", 3, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(null, vehicle.Inspection.StartDate.ToString(), 1, 3));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(null, vehicle.Inspection.EndDate.ToString(), 2, 3));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(null, vehicle.Inspection.Price.ToString() + " zł", 3, 3));

            return CyclicalCostGrid;
        }
    }
}
