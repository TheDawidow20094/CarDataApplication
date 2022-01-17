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
    class CostContentGenerator
    {
        public void CostGenerator(MainWindow mw, List<Vehicle> myvehicle)
        {
            Grid MainGrid = new Grid();

            List<Service> ServicesList = new List<Service>();
            for (int i = 0; i < myvehicle[0].Services.Count; i++)
            {
                ServicesList.Add(myvehicle[0].Services[i]);

                RowDefinition MainGridRow = new RowDefinition();
                MainGridRow.Height = new GridLength(120);
                MainGrid.RowDefinitions.Add(MainGridRow);

                Grid CostInfoGrid = new Grid();
                CostInfoGrid.Margin = new Thickness(10);
                CostInfoGrid.Background = Brushes.DarkGray;
                Grid.SetRow(CostInfoGrid, i);

                for (int x = 0; x < 4; x++)
                {
                    RowDefinition CostInfoRow = new RowDefinition();
                    CostInfoGrid.RowDefinitions.Add(CostInfoRow);
                }

                for (int y = 0; y < 3; y++)
                {
                    ColumnDefinition CostInfoColumn = new ColumnDefinition();
                    CostInfoGrid.ColumnDefinitions.Add(CostInfoColumn);
                }

                Image image = new Image();
                ImageSourceConverter source = new ImageSourceConverter();
                image.SetValue(Image.SourceProperty, source.ConvertFromString(@"../../../Images/Icons/cost.png"));
                Grid.SetRow(image, 0);
                Grid.SetColumn(image, 1);
                CostInfoGrid.Children.Add(image);

                TextBlock Name = new TextBlock();
                Name.Text = ServicesList[i].Name.ToString();
                Grid.SetRow(Name, 0);
                Grid.SetColumn(Name, 0);
                CostInfoGrid.Children.Add(Name);

                TextBlock Cost = new TextBlock();
                Cost.Text = ServicesList[i].Price.ToString() + " zł";
                Grid.SetRow(Cost, 0);
                Grid.SetColumn(Cost, 2);
                CostInfoGrid.Children.Add(Cost);

                TextBlock Data = new TextBlock();
                Data.Text = ServicesList[i].Date.ToString();
                Grid.SetRow(Data, 1);
                Grid.SetColumn(Data, 0);
                CostInfoGrid.Children.Add(Data);

                TextBlock ServiceType = new TextBlock();
                ServiceType.Text = ServicesList[i].Category.ToString();
                Grid.SetRow(ServiceType, 2);
                Grid.SetColumn(ServiceType, 0);
                CostInfoGrid.Children.Add(ServiceType);

                TextBlock Descryption = new TextBlock();
                Descryption.Text = ServicesList[i].Comment.ToString();
                Grid.SetRow(Descryption, 3);
                CostInfoGrid.Children.Add(Descryption);

                MainGrid.Children.Add(CostInfoGrid);
            }

            mw.ScrollViewerContent.Content = MainGrid;
        }
    }
}
