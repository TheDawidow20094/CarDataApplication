using Car_Data_Application.Models;
using Car_Data_Application.Models.Vehicle_Classes;
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
    class RefuelingHistoryContentGenerator
    {

        public void GeneratorRefulingHistory(MainWindow mw, List<Vehicle> myvehicles)
        {
            Grid MainGrid = new Grid();

            List<Refueling> RefuelingList = new List<Refueling>();
            for (int i = 0; i < myvehicles[0].Refulings.Count; i++)
            {
                RefuelingList.Add(myvehicles[0].Refulings[i]);

                RowDefinition MainGridRow = new RowDefinition();
                MainGridRow.Height = new GridLength(120);
                MainGrid.RowDefinitions.Add(MainGridRow);

                Grid RefuelingGrid = new Grid();
                RefuelingGrid.Margin = new Thickness(10);
                RefuelingGrid.Background = Brushes.DarkGray;
                Grid.SetRow(RefuelingGrid, i);

                for (int x = 0; x <= 5; x++)
                {
                    RowDefinition RefuelingRow = new RowDefinition();
                    RefuelingGrid.RowDefinitions.Add(RefuelingRow);
                }

                for (int y = 0; y <= 3; y++)
                {
                    ColumnDefinition RefuelingColumn = new ColumnDefinition();
                    RefuelingGrid.ColumnDefinitions.Add(RefuelingColumn);
                }

                Image image = new Image();
                ImageSourceConverter source = new ImageSourceConverter();
                image.SetValue(Image.SourceProperty, source.ConvertFromString(@"../../../Images/Icons/fuelicon.png"));
                Grid.SetRow(image, 0);
                Grid.SetColumn(image, 1);
                RefuelingGrid.Children.Add(image);

                TextBlock Date = new TextBlock();
                Date.Text = RefuelingList[i].Date.ToString();
                Grid.SetRow(Date, 1);
                Grid.SetColumn(Date, 0);
                RefuelingGrid.Children.Add(Date);

                TextBlock Millage = new TextBlock();
                Millage.Text = RefuelingList[i].CarMillage.ToString() + " km";
                Grid.SetRow(Millage, 1);
                Grid.SetColumn(Millage, 2);
                RefuelingGrid.Children.Add(Millage);

                TextBlock Cost = new TextBlock();
                Cost.Text = RefuelingList[i].TotalPrice.ToString() + " zł";
                Grid.SetRow(Cost, 2);
                Grid.SetColumn(Cost, 0);
                RefuelingGrid.Children.Add(Cost);

                TextBlock Kilometers = new TextBlock();
                Kilometers.Text = "milion km";
                Grid.SetRow(Kilometers, 2);
                Grid.SetColumn(Kilometers, 2);
                RefuelingGrid.Children.Add(Kilometers);

                TextBlock Liters = new TextBlock();
                Liters.Text = RefuelingList[i].Liters.ToString() + " litrów";
                Grid.SetRow(Liters, 3);
                Grid.SetColumn(Liters, 0);
                RefuelingGrid.Children.Add(Liters);

                TextBlock PriceForOneLiter = new TextBlock();
                PriceForOneLiter.Text = "500zł";
                Grid.SetRow(PriceForOneLiter, 3);
                Grid.SetColumn(PriceForOneLiter, 2);
                RefuelingGrid.Children.Add(PriceForOneLiter);

                TextBlock TypeOfFuel = new TextBlock();
                TypeOfFuel.Text = "ECO DIESEL";
                Grid.SetRow(TypeOfFuel, 4);
                Grid.SetColumn(TypeOfFuel, 1);
                RefuelingGrid.Children.Add(TypeOfFuel);

                TextBlock AverageFuelConsumption = new TextBlock();
                AverageFuelConsumption.Text = "12L / 100km";
                Grid.SetRow(AverageFuelConsumption, 4);
                Grid.SetColumn(AverageFuelConsumption, 2);
                RefuelingGrid.Children.Add(AverageFuelConsumption);

                MainGrid.Children.Add(RefuelingGrid);
            }

            
            mw.ScrollViewerContent.Content = MainGrid;



        }
    }
}
