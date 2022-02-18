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
using Car_Data_Application.Models.XML_Models;

namespace Car_Data_Application.Controllers
{
    class VehiclesContentGenerator : CarDataAppController
    {
        public void GeneratorVechicleList(MainWindow mw, User user, Config paramConfig)
        {
            InitialAssignValue(mw, user, paramConfig);

            Grid grid = new Grid();

            int VehicleIndex = 0;
            foreach (Vehicle vehicle in PUser.Vehicles)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(140) });

                Grid VehicleGrid = new Grid();
                SetGridProps(ref VehicleGrid, VehicleIndex);
                VehicleGrid.Name = "VehicleButton_" + VehicleIndex;
                VehicleGrid.MouseLeftButtonDown += HandleContentBorderClick;

                VehicleGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(190) });
                VehicleGrid.ColumnDefinitions.Add(new ColumnDefinition());

                VehicleGrid.Children.Add(GenerateVehicePicture(vehicle.PictureFileName, 0));

                VehicleGrid.Children.Add(GenerateTextBlock(null, vehicle.Brand + " " + vehicle.Model, 0, 1, "#FF2A2729", HorizontalAlignment.Center, VerticalAlignment.Center, true));

                VehicleGrid.MouseEnter += HandleContentBorderMouseEnter;
                VehicleGrid.MouseLeave += HandleContentBorderMouseLeave;

                Grid.SetRow(VehicleGrid, VehicleIndex);

                grid.Children.Add(VehicleGrid);

                VehicleIndex++;
            }
            mainWindow.ScrollViewerContent.Content = grid;
        }

        private void InitialAssignValue(MainWindow mw, User user, Config paramConfig)
        {
            mainWindow = mw;
            PUser = user;
            config = paramConfig;
            mainWindow.WhereAreYou = "VehiclesPage";
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.FindName("SidePanel")));
        }

        public Image GenerateVehicePicture(string path, int column)
        {
            Image image = new Image();
            ImageSourceConverter source = new ImageSourceConverter();
            image.Height = 140;
            image.VerticalAlignment = VerticalAlignment.Center;
            image.HorizontalAlignment = HorizontalAlignment.Left;
            if (File.Exists(@"..\..\..\Images\UserPictures\" + path))
            {
                image.SetValue(Image.SourceProperty, source.ConvertFromString(@"..\..\..\Images\UserPictures\" + path));
            }
            else
            {
                image.SetValue(Image.SourceProperty, source.ConvertFromString(@"..\..\..\Images\defaultcaricon.png"));
                image.Margin = new Thickness(25,0,0,0);
            }

            return image;
        }

        private void HandleContentBorderClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int index = Int32.Parse(((Grid)sender).Name.Substring(14));
            new VehicleDetailContentGenerator().GeneratorVehicleDetail(mainWindow, PUser.Vehicles[index], PUser, config.MainPanel.VehiclesPage);
        }

        private void HandleContentBorderMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Grid)sender).Background = Brushes.WhiteSmoke;
        }

        private void HandleContentBorderMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Grid)sender).Background = Brushes.White;
        }

    }
}
