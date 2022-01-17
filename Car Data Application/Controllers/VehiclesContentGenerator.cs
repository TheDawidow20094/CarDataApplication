using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Car_Data_Application.Views;
using Car_Data_Application.Models;
using Newtonsoft.Json;

namespace Car_Data_Application.Controllers
{
    class VehiclesContentGenerator
    {                                                                 
        public MainWindow Mainwindow;
        public User User;

        public void GeneratorVechicleList(MainWindow mw, User user)
        {
            Mainwindow = mw;
            User = user;

            Image AddButton = new Image();
            AddButton.SetValue(FrameworkElement.NameProperty, "AddButton");
            ImageSourceConverter source = new ImageSourceConverter();
            AddButton.Margin = new Thickness(0, 0, 30, 10);
            AddButton.HorizontalAlignment = HorizontalAlignment.Right;
            AddButton.VerticalAlignment = VerticalAlignment.Bottom;
            AddButton.SetValue(Image.SourceProperty, source.ConvertFromString(@"../../../Images/Icons/AddIcon.png"));
            AddButton.Width = 55;
            AddButton.Height = 55;
            AddButton.MouseLeftButtonDown += AddVehicle;

            Grid.SetRow(AddButton, 0);
            Grid.SetColumn(AddButton, 1);
            Mainwindow.MainGrid.Children.Add(AddButton);

            Grid grid = new Grid();

            int VehicleIndex = 0;
            foreach (Vehicle vehicle in User.Vehicles)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(120);
                grid.RowDefinitions.Add(row);

                Button VehicleButton = new Button();
                VehicleButton.Name = "ButtonVehicle" + vehicle.Id.ToString();
                VehicleButton.Margin = new Thickness(10);
                VehicleButton.Content = vehicle.Brand + " " + vehicle.Model;
                VehicleButton.Height = 120;
                BrushConverter converter = new BrushConverter();
                Brush brush = (Brush)converter.ConvertFromString("#6bbbf7");
                VehicleButton.Background = brush;
                VehicleButton.Click += VehicleButtonClick;

                Image image = new Image();

                image.Margin = new Thickness(10);
                image.HorizontalAlignment = HorizontalAlignment.Left;                        
                image.SetValue(Image.SourceProperty, source.ConvertFromString("pack://application:,,,/Images/draw.png"));


                Grid.SetRow(image, VehicleIndex);
                Grid.SetRow(VehicleButton, VehicleIndex);

                grid.Children.Add(VehicleButton);
                grid.Children.Add(image);

                VehicleIndex++;
            }
            Mainwindow.ScrollViewerContent.Content = grid;
        }

        private void AddVehicle(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("Dodajemy pojazd");
        }

        private void VehicleButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Int32.Parse(btn.Name.Substring(13));
            new VehicleDetailContentGenerator().GeneratorVehicleDetail(Mainwindow, User.Vehicles[index]);
        }
    }
}
