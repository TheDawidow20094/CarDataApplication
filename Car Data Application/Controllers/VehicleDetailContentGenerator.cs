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
        public void GeneratorVehicleDetail(MainWindow mainwindow, Vehicle vehicle)
        {
            mainwindow.AddButon.Visibility = Visibility.Hidden;

            Grid maingrid = new Grid();

            for (int i = 0; i <= 6; i++) // 6 is number of displays blocks with car data
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(120);
                maingrid.RowDefinitions.Add(row);
            }

            maingrid.Children.Add(DisplayVehicleImage(vehicle));
            maingrid.Children.Add(GenarateVehicleNameGrid(vehicle));
            maingrid.Children.Add(GeneratePrimaryInfoGrid(vehicle));
            maingrid.Children.Add(GenarateFuelTankInfoGrid(vehicle));
            maingrid.Children.Add(GenerateCyclicalCostGrid(vehicle));


            mainwindow.ScrollViewerContent.Content = maingrid;
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
            for (int i = 0; i < 2; i++)
            {
                RowDefinition vehiclenamerow = new RowDefinition();
                vehiclenamegrid.RowDefinitions.Add(vehiclenamerow);
            }
            for (int i = 0; i < 2; i++)
            {
                ColumnDefinition vehiclenamecolumn = new ColumnDefinition();
                vehiclenamegrid.ColumnDefinitions.Add(vehiclenamecolumn);
            }

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

        public Grid GenarateFuelTankInfoGrid(Vehicle vehicle)
        {
            Grid fuelinfogrid = new Grid();
            fuelinfogrid.Margin = new Thickness(10);
            Grid.SetRow(fuelinfogrid, 3);

            ColumnDefinition fuelinfocolumnleft = new ColumnDefinition();
            ColumnDefinition fuelinfocolumnright = new ColumnDefinition();
            fuelinfogrid.ColumnDefinitions.Add(fuelinfocolumnleft);
            fuelinfogrid.ColumnDefinitions.Add(fuelinfocolumnright);

            for (int i = 0; i <= 3; i++) // number of user fueltanks
            {
                RowDefinition fuelinforow = new RowDefinition();
                fuelinfogrid.RowDefinitions.Add(fuelinforow);
            }

            TextBlock gasolinetank = new TextBlock();
            gasolinetank.Text = "Pojemność baku Paliwa";
            Grid.SetRow(gasolinetank, 0);
            Grid.SetColumn(gasolinetank, 0);
            fuelinfogrid.Children.Add(gasolinetank);

            TextBlock gasolinetankvalue = new TextBlock();
            gasolinetankvalue.Text = vehicle.Tanks.Gasoline.ToString();
            Grid.SetRow(gasolinetankvalue, 0);
            Grid.SetColumn(gasolinetankvalue, 1);
            fuelinfogrid.Children.Add(gasolinetankvalue);

            TextBlock dieseltank = new TextBlock();
            dieseltank.Text = "Pojemność baku Diesel";
            Grid.SetRow(dieseltank, 1);
            Grid.SetColumn(dieseltank, 0);
            fuelinfogrid.Children.Add(dieseltank);

            TextBlock dieseltankvalue = new TextBlock();
            dieseltankvalue.Text = vehicle.Tanks.Diesel.ToString();
            Grid.SetRow(dieseltankvalue, 1);
            Grid.SetColumn(dieseltankvalue, 1);
            fuelinfogrid.Children.Add(dieseltankvalue);

            TextBlock lpgtank = new TextBlock();
            lpgtank.Text = "Pojemność baku LPG";
            Grid.SetRow(lpgtank, 2);
            Grid.SetColumn(lpgtank, 0);
            fuelinfogrid.Children.Add(lpgtank);

            TextBlock lpgtankvalue = new TextBlock();
            lpgtankvalue.Text = vehicle.Tanks.LPG.ToString();
            Grid.SetRow(lpgtankvalue, 2);
            Grid.SetColumn(lpgtankvalue, 1);
            fuelinfogrid.Children.Add(lpgtankvalue);

            return fuelinfogrid;
        }

        public Grid GenerateCyclicalCostGrid(Vehicle vehicle)
        {
            Grid cyclicalcostgrid = new Grid();
            cyclicalcostgrid.Margin = new Thickness(10);
            Grid.SetRow(cyclicalcostgrid, 4);

            ColumnDefinition cyclicalcoscolumnleft = new ColumnDefinition();
            ColumnDefinition cyclicalcoscolumnright = new ColumnDefinition();
            cyclicalcostgrid.ColumnDefinitions.Add(cyclicalcoscolumnleft);
            cyclicalcostgrid.ColumnDefinitions.Add(cyclicalcoscolumnright);

            for (int i = 0; i <= 7; i++) // 7 number of cyclicalcost + (value) + empty space to divide insurance and inspection
            {
                RowDefinition cyclicalcostrow = new RowDefinition();
                cyclicalcostgrid.RowDefinitions.Add(cyclicalcostrow);
            }

            TextBlock insurancestart = new TextBlock();
            insurancestart.Text = "Rozpoczęcie okresu ubezpieczenia";
            Grid.SetRow(insurancestart, 0);
            Grid.SetColumn(insurancestart, 0);
            cyclicalcostgrid.Children.Add(insurancestart);

            TextBlock insurancestartvalue = new TextBlock();
            insurancestartvalue.Text = vehicle.Insurance.StartDate.ToString();
            Grid.SetRow(insurancestartvalue, 0);
            Grid.SetColumn(insurancestartvalue, 1);
            cyclicalcostgrid.Children.Add(insurancestartvalue);

            TextBlock insuranceend = new TextBlock();
            insuranceend.Text = "Koniec okresu ubezpieczenia";
            Grid.SetRow(insuranceend, 1);
            Grid.SetColumn(insuranceend, 0);
            cyclicalcostgrid.Children.Add(insuranceend);

            TextBlock insuranceendvalue = new TextBlock();
            insuranceendvalue.Text = vehicle.Insurance.EndDate.ToString();
            Grid.SetRow(insuranceendvalue, 1);
            Grid.SetColumn(insuranceendvalue, 1);
            cyclicalcostgrid.Children.Add(insuranceendvalue);

            TextBlock insurancecost = new TextBlock();
            insurancecost.Text = "Koszt odecnego ubezpieczenia";
            Grid.SetRow(insurancecost, 2);
            Grid.SetColumn(insurancecost, 0);
            cyclicalcostgrid.Children.Add(insurancecost);

            TextBlock insurancecostvalue = new TextBlock();
            insurancecostvalue.Text = vehicle.Insurance.Price.ToString() + " zł";
            Grid.SetRow(insurancecostvalue, 2);
            Grid.SetColumn(insurancecostvalue, 1);
            cyclicalcostgrid.Children.Add(insurancecostvalue);
            
            //============================DIVIDE ELEMENTS=======================================

            TextBlock divideelementscolumn = new TextBlock();
            divideelementscolumn.Text = "";
            Grid.SetRow(divideelementscolumn, 3);
            Grid.SetColumn(divideelementscolumn, 0);
            cyclicalcostgrid.Children.Add(divideelementscolumn);

            TextBlock divideelementsrow = new TextBlock();
            divideelementsrow.Text = "";
            Grid.SetRow(divideelementsrow, 3);
            Grid.SetColumn(divideelementsrow, 1);
            cyclicalcostgrid.Children.Add(divideelementsrow);

            //===================================================================================

            TextBlock inspectionstart = new TextBlock();
            inspectionstart.Text = "Data wykonania przeglądu technicznego";
            Grid.SetRow(inspectionstart, 4);
            Grid.SetColumn(inspectionstart, 0);
            cyclicalcostgrid.Children.Add(inspectionstart);

            TextBlock inspectionstartvalue = new TextBlock();
            inspectionstartvalue.Text = vehicle.Inspection.StartDate.ToString();
            Grid.SetRow(inspectionstartvalue, 4);
            Grid.SetColumn(inspectionstartvalue, 1);
            cyclicalcostgrid.Children.Add(inspectionstartvalue);

            TextBlock inspectionend = new TextBlock();
            inspectionend.Text = "Koniec ważności przeglądu technicznego";
            Grid.SetRow(inspectionend, 5);
            Grid.SetColumn(inspectionend, 0);
            cyclicalcostgrid.Children.Add(inspectionend);

            TextBlock inspectionendvalue = new TextBlock();
            inspectionendvalue.Text = vehicle.Inspection.EndDate.ToString();
            Grid.SetRow(inspectionendvalue, 5);
            Grid.SetColumn(inspectionendvalue, 1);
            cyclicalcostgrid.Children.Add(inspectionendvalue);

            TextBlock inspectioncost = new TextBlock();
            inspectioncost.Text = "Koszt wykonania badania technicznego";
            Grid.SetRow(inspectioncost, 6);
            Grid.SetColumn(inspectioncost, 0);
            cyclicalcostgrid.Children.Add(inspectioncost);

            TextBlock inspectioncostvalue = new TextBlock();
            inspectioncostvalue.Text = vehicle.Inspection.Price.ToString() + " zł";
            Grid.SetRow(inspectioncostvalue, 6);
            Grid.SetColumn(inspectioncostvalue, 1);
            cyclicalcostgrid.Children.Add(inspectioncostvalue);

            return cyclicalcostgrid;
        }
    }
}
