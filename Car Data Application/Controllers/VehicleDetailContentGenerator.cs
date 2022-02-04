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

        private string LightTextColor = "#FF9C9397";
        private string DarkTextColor = "#FF2A2729"; // change to set in config

        private int ActualMainGridRow = new int();

        public void GeneratorVehicleDetail(MainWindow mainwindow, Vehicle vehicle, User user, VehiclesPage vehiclesPage)
        {
            InitialAssignValue(mainwindow, user);

            Grid MainGrid = new Grid();

            for (int i = 0; i < 5; i++) // 6 is number of rows
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
            switch (PUser.UserLanguage)
            {
                case "PL":
                    vehicleNameGrid.Children.Add(GenerateTextBlock(translation.Brand.PL, 0, 0, LightTextColor, HorizontalAlignment.Right));
                    vehicleNameGrid.Children.Add(GenerateTextBlock(translation.Model.PL, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    break;
                case "ENG":
                    vehicleNameGrid.Children.Add(GenerateTextBlock(translation.Brand.ENG, 0, 0, LightTextColor, HorizontalAlignment.Right));
                    vehicleNameGrid.Children.Add(GenerateTextBlock(translation.Model.ENG, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    break;
            }

            vehicleNameGrid.Children.Add(GenerateTextBlock(vehicle.Brand, 0, 1));

            vehicleNameGrid.Children.Add(GenerateTextBlock(vehicle.Model, 1, 1));

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

            switch (PUser.UserLanguage)
            {
                case "PL":
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.YearOfManufacture.PL, 0, 0, LightTextColor, HorizontalAlignment.Right));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Vin.PL, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Plates.PL, 2, 0, LightTextColor, HorizontalAlignment.Right));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Millage.PL, 3, 0, LightTextColor, HorizontalAlignment.Right));
                    break;
                case "ENG":
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.YearOfManufacture.ENG, 0, 0, LightTextColor, HorizontalAlignment.Right));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Vin.ENG, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Plates.ENG, 2, 0, LightTextColor, HorizontalAlignment.Right));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Millage.ENG, 3, 0, LightTextColor, HorizontalAlignment.Right));
                    break;
            }


            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(vehicle.YearOfManufacture.ToString(), 0, 1));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(vehicle.Vin.ToString(), 1, 1));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(vehicle.Plates.ToString(), 2, 1));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(vehicle.CarMillage.ToString(), 3, 1));

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

            switch (PUser.UserLanguage)
            {
                case "PL":
                    FuelInfoGrid.Children.Add(GenerateTextBlock(translation.FuelTankInfoTitle.PL, RowIndex, 0, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
                    break;
                case "ENG":
                    FuelInfoGrid.Children.Add(GenerateTextBlock(translation.FuelTankInfoTitle.ENG, RowIndex, 0, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
                    break;
            }

            if (vehicle.Tanks.Gasoline != 0)
            {
                FuelInfoGrid.RowDefinitions.Add(new RowDefinition());
                RowIndex++;

                switch (PUser.UserLanguage)
                {
                    case "PL":
                        FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Gasoline.PL, RowIndex, 0, LightTextColor, HorizontalAlignment.Right));
                        break;
                    case "ENG":
                        FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Gasoline.ENG, RowIndex, 0, LightTextColor, HorizontalAlignment.Right));
                        break;
                }
                FuelInfoGrid.Children.Add(GenerateTextBlock(vehicle.Tanks.Gasoline.ToString(), RowIndex, 1));
            }

            if (vehicle.Tanks.LPG != 0)
            {
                FuelInfoGrid.RowDefinitions.Add(new RowDefinition());
                RowIndex++;

                switch (PUser.UserLanguage)
                {
                    case "PL":
                        FuelInfoGrid.Children.Add(GenerateTextBlock(translation.LPG.PL, RowIndex, 0, LightTextColor, HorizontalAlignment.Right));
                        break;
                    case "ENG":
                        FuelInfoGrid.Children.Add(GenerateTextBlock(translation.LPG.ENG, RowIndex, 0, LightTextColor, HorizontalAlignment.Right));
                        break;
                }
                FuelInfoGrid.Children.Add(GenerateTextBlock(vehicle.Tanks.LPG.ToString(), RowIndex, 1));
            }

            if (vehicle.Tanks.Diesel != 0)
            {
                FuelInfoGrid.RowDefinitions.Add(new RowDefinition());
                RowIndex++;

                switch (PUser.UserLanguage)
                {
                    case "PL":
                        FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Diesel.PL, RowIndex, 0, LightTextColor, HorizontalAlignment.Right));
                        break;
                    case "ENG":
                        FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Diesel.ENG, RowIndex, 0, LightTextColor, HorizontalAlignment.Right));
                        break;
                }
                FuelInfoGrid.Children.Add(GenerateTextBlock(vehicle.Tanks.Diesel.ToString(), RowIndex, 1));
            }


            return FuelInfoGrid;
        }

        //private Grid GenarateFuelTankInfoGrid(Vehicle vehicle, FuelTankInfoGrid translation)
        //{
        //    ActualMainGridRow = 3;

        //    Grid FuelInfoGrid = new Grid();
        //    SetGridProps(ref FuelInfoGrid, ActualMainGridRow);

        //    for (int i = 0; i < 2; i++) // 2 number of columns
        //    {
        //        ColumnDefinition FuelInfoGridColumn = new ColumnDefinition();
        //        FuelInfoGrid.ColumnDefinitions.Add(FuelInfoGridColumn);
        //    }
        //    for (int i = 0; i <= 3; i++) //3 number of rows
        //    {
        //        RowDefinition FuelInfGridRow = new RowDefinition();
        //        FuelInfoGrid.RowDefinitions.Add(FuelInfGridRow);
        //    }

        //    switch (PUser.UserLanguage)
        //    {
        //        case "PL":
        //            FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Gasoline.PL, 0, 0, LightTextColor, HorizontalAlignment.Right));
        //            FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Diesel.PL, 1, 0, LightTextColor, HorizontalAlignment.Right));
        //            FuelInfoGrid.Children.Add(GenerateTextBlock(translation.LPG.PL, 2, 0, LightTextColor, HorizontalAlignment.Right));
        //            break;
        //        case "ENG":
        //            FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Gasoline.ENG, 0, 0, LightTextColor, HorizontalAlignment.Right));
        //            FuelInfoGrid.Children.Add(GenerateTextBlock(translation.Diesel.ENG, 1, 0, LightTextColor, HorizontalAlignment.Right));
        //            FuelInfoGrid.Children.Add(GenerateTextBlock(translation.LPG.ENG, 2, 0, LightTextColor, HorizontalAlignment.Right));
        //            break;
        //    }


        //    FuelInfoGrid.Children.Add(GenerateTextBlock(vehicle.Tanks.Gasoline.ToString(), 0, 1));

        //    FuelInfoGrid.Children.Add(GenerateTextBlock(vehicle.Tanks.Diesel.ToString(), 1, 1));

        //    FuelInfoGrid.Children.Add(GenerateTextBlock(vehicle.Tanks.LPG.ToString(), 2, 1));

        //    return FuelInfoGrid;
        //}

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

            switch (PUser.UserLanguage)
            {
                case "PL":
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceTitle.PL, 0, 0, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceStartDate.PL, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceEndDate.PL, 2, 0, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsurancePrice.PL, 3, 0, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionTitle.PL, 0, 2, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionStartDate.PL, 1, 2, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate.PL, 2, 2, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate.PL, 3, 2, LightTextColor, HorizontalAlignment.Right));
                    break;
                case "ENG":
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceTitle.ENG, 0, 0, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceStartDate.ENG, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceEndDate.ENG, 2, 0, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsurancePrice.ENG, 3, 0, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionTitle.ENG, 0, 2, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionStartDate.ENG, 1, 2, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate.ENG, 2, 2, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate.ENG, 3, 2, LightTextColor, HorizontalAlignment.Right));
                    break;
            }


            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Insurance.StartDate.ToString(), 1, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Insurance.EndDate.ToString(), 2, 1));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Insurance.Price.ToString() + " zł", 3, 1));

            

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Inspection.StartDate.ToString(), 1, 3));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Inspection.EndDate.ToString(), 2, 3));

            CyclicalCostGrid.Children.Add(GenerateTextBlock(vehicle.Inspection.Price.ToString() + " zł", 3, 3));

            return CyclicalCostGrid;
        }
    }
}
