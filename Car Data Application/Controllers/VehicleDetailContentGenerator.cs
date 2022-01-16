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

namespace Car_Data_Application.Controllers
{
    class VehicleDetailContentGenerator
    {
        public void GeneratorVehicleDetail(MainWindow mainwindow, Vehicle vehicle)
        {
            Grid grid = new Grid();

            for (int i = 0; i <= 6; i++) // 6 is number of displays windows with car data
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(120);
                grid.RowDefinitions.Add(row);
            }
            
            grid.Children.Add(DisplayVehicleImage(vehicle));
            grid.Children.Add(GenarateVehicleNameGrid(vehicle));
            grid.Children.Add(GeneratePrimaryInfoGrid(vehicle));


            mainwindow.ScrollViewerContent.Content = grid;
        }
        public Image DisplayVehicleImage(Vehicle vehicle)
        {
            Image image = new Image();
            ImageSourceConverter source = new ImageSourceConverter();
            image.SetValue(Image.SourceProperty, source.ConvertFromString("pack://application:,,,/Images/draw.png"));
            Grid.SetRow(image, 0);

            return image;
        }

        public Grid GenarateVehicleNameGrid(Vehicle vehicle)
        {
            Grid vehiclenamegrid = new Grid();
            vehiclenamegrid.Margin = new Thickness(10);
            Grid.SetRow(vehiclenamegrid, 1);

            RowDefinition vehiclenamerowup = new RowDefinition();
            RowDefinition vehiclenamerowbottom = new RowDefinition();
            ColumnDefinition vehiclenamecolumnleft = new ColumnDefinition();
            ColumnDefinition vehiclenamecolumnright = new ColumnDefinition();

            vehiclenamegrid.RowDefinitions.Add(vehiclenamerowup);
            vehiclenamegrid.RowDefinitions.Add(vehiclenamerowbottom);
            vehiclenamegrid.ColumnDefinitions.Add(vehiclenamecolumnleft);
            vehiclenamegrid.ColumnDefinitions.Add(vehiclenamecolumnright);

            TextBlock vehiclebrand = new TextBlock();
            vehiclebrand.Text = "Marka";
            Grid.SetRow(vehiclebrand, 0);
            Grid.SetColumn(vehiclebrand, 0);
            vehiclenamegrid.Children.Add(vehiclebrand);

            TextBlock vehiclebrandvalue = new TextBlock();
            vehiclebrandvalue.Text = vehicle.Brand;
            Grid.SetRow(vehiclebrandvalue, 0);
            Grid.SetColumn(vehiclebrandvalue, 1);
            vehiclenamegrid.Children.Add(vehiclebrandvalue);

            TextBlock vehiclemodel = new TextBlock();
            vehiclemodel.Text = "Model";
            Grid.SetRow(vehiclemodel, 1);
            Grid.SetColumn(vehiclemodel, 0);
            vehiclenamegrid.Children.Add(vehiclemodel);

            TextBlock vehiclemodelvalue = new TextBlock();
            vehiclemodelvalue.Text = vehicle.Model;
            Grid.SetRow(vehiclemodelvalue, 1);
            Grid.SetColumn(vehiclemodelvalue, 1);
            vehiclenamegrid.Children.Add(vehiclemodelvalue);

            return vehiclenamegrid;
        }

        public Grid GeneratePrimaryInfoGrid(Vehicle vehicle)
        {
            Grid primarmaryinfogrid = new Grid();
            primarmaryinfogrid.Margin = new Thickness(10);
            Grid.SetRow(primarmaryinfogrid, 2);

            ColumnDefinition primaryinfocolumnleft = new ColumnDefinition();
            ColumnDefinition primaryinfocolumnright = new ColumnDefinition();
            primarmaryinfogrid.ColumnDefinitions.Add(primaryinfocolumnleft);
            primarmaryinfogrid.ColumnDefinitions.Add(primaryinfocolumnright);

            for (int i = 0; i <= 4; i++) // 4 is number of multiply of rows in grid
            {
                RowDefinition primarmaryinforow = new RowDefinition();
                primarmaryinfogrid.RowDefinitions.Add(primarmaryinforow);
            }

            TextBlock vehicleyearofmanufacture = new TextBlock();
            vehicleyearofmanufacture.Text = "Rok produkcji";
            Grid.SetRow(vehicleyearofmanufacture, 0);
            Grid.SetColumn(vehicleyearofmanufacture, 0);
            primarmaryinfogrid.Children.Add(vehicleyearofmanufacture);

            TextBlock vehicleyearofmanufacturevalue = new TextBlock();
            vehicleyearofmanufacturevalue.Text = vehicle.YearOfManufacture.ToString();
            Grid.SetRow(vehicleyearofmanufacturevalue, 0);
            Grid.SetColumn(vehicleyearofmanufacturevalue, 1);
            primarmaryinfogrid.Children.Add(vehicleyearofmanufacturevalue);

            TextBlock vehiclevin = new TextBlock();
            vehiclevin.Text = "VIN";
            Grid.SetRow(vehiclevin, 1);
            Grid.SetColumn(vehiclevin, 0);
            primarmaryinfogrid.Children.Add(vehiclevin);

            TextBlock vehiclevinvalue = new TextBlock();
            vehiclevinvalue.Text = vehicle.Vin.ToString();
            Grid.SetRow(vehiclevinvalue, 1);
            Grid.SetColumn(vehiclevinvalue, 1);
            primarmaryinfogrid.Children.Add(vehiclevinvalue);

            TextBlock vehicleplates = new TextBlock();
            vehicleplates.Text = "Tablice Rejestracyjne";
            Grid.SetRow(vehicleplates, 2);
            Grid.SetColumn(vehicleplates, 0);
            primarmaryinfogrid.Children.Add(vehicleplates);

            TextBlock vehicleplatesvalue = new TextBlock();
            vehicleplatesvalue.Text = vehicle.Plates.ToString();
            Grid.SetRow(vehicleplatesvalue, 2);
            Grid.SetColumn(vehicleplatesvalue, 1);
            primarmaryinfogrid.Children.Add(vehicleplatesvalue);

            TextBlock vehiclemillage = new TextBlock();
            vehiclemillage.Text = "Przebieg";
            Grid.SetRow(vehiclemillage, 3);
            Grid.SetColumn(vehiclemillage, 0);
            primarmaryinfogrid.Children.Add(vehiclemillage);

            TextBlock vehiclemillagevalue = new TextBlock();
            vehiclemillagevalue.Text = vehicle.CarMillage.ToString();
            Grid.SetRow(vehiclemillagevalue, 3);
            Grid.SetColumn(vehiclemillagevalue, 1);
            primarmaryinfogrid.Children.Add(vehiclemillagevalue);

            return primarmaryinfogrid;
        }
    }
}
