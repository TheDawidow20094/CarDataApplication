﻿using System;
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
using System.Text.Json;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Car_Data_Application.Models.XML_Models;

namespace Car_Data_Application.Controllers
{
    class VehiclesContentGenerator : CarDataAppController
    {
        public void GeneratorVechicleList(MainWindow mw, User user, MainGrid config)
        {
            InitialAssignValue(mw, user, config);

            Grid grid = new Grid();

            int VehicleIndex = 0;
            foreach (Vehicle vehicle in PUser.Vehicles)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(140);
                grid.RowDefinitions.Add(row);

                Border ContentBorder = new Border();
                Brush BackgroundBrushh = (Brush)Converter.ConvertFromString("#FF001A34");
                Brush ForegroundBrushh = (Brush)Converter.ConvertFromString("#FFEDF5FD");
                ContentBorder.Background = BackgroundBrushh;

                ContentBorder.Name = "VehicleButton_" + VehicleIndex;
                ContentBorder.MouseLeftButtonDown += HandleContentBorderClick;

                ContentBorder.BorderThickness = new Thickness(5);
                ContentBorder.BorderBrush = (Brush)Converter.ConvertFrom("#FF407BB6");
                ContentBorder.CornerRadius = new CornerRadius(30);

                ContentBorder.Margin = new Thickness(15,0,15,0);
                ContentBorder.Padding = new Thickness(0, 0, 35, 0);
                ContentBorder.Height = 120;
                ContentBorder.BorderThickness = new Thickness(5);

                Grid ContentBorderGrid = new Grid();

                Image image = new Image();
                ImageSourceConverter source = new ImageSourceConverter();
                image.Margin = new Thickness(10);
                image.HorizontalAlignment = HorizontalAlignment.Left;
                if (File.Exists(@"..\..\..\Images\UserPictures\" + vehicle.PictureFileName))
                {
                    image.SetValue(Image.SourceProperty, source.ConvertFromString(@"..\..\..\Images\UserPictures\" + vehicle.PictureFileName));
                }
                else
                {
                    image.SetValue(Image.SourceProperty, source.ConvertFromString(@"..\..\..\Images\defaultcaricon.png"));
                }

                Border ImageBorder = new Border();
                ImageBorder.HorizontalAlignment = HorizontalAlignment.Left;
                ImageBorder.CornerRadius = new CornerRadius(44,10,10,41);
                ImageBorder.Child = image;

                TextBlock VehicleName = new TextBlock();
                VehicleName.Text = vehicle.Brand + " " + vehicle.Model;
                VehicleName.HorizontalAlignment = HorizontalAlignment.Right;
                VehicleName.VerticalAlignment = VerticalAlignment.Center;
                VehicleName.Padding = new Thickness(0,0,20,0);
                VehicleName.FontSize = 20;
                VehicleName.FontFamily = new FontFamily("Arial Black");
                VehicleName.FontWeight = FontWeights.Bold;
                VehicleName.Foreground = (Brush)Converter.ConvertFrom("#FFEDF5FD");

                ContentBorderGrid.Children.Add(ImageBorder);
                ContentBorderGrid.Children.Add(VehicleName);

                ContentBorder.Child = ContentBorderGrid;

                ContentBorder.MouseEnter += HandleContentBorderMouseEnter;
                ContentBorder.MouseLeave += HandleContentBorderMouseLeave;

                Grid.SetRow(ContentBorder, VehicleIndex);

                grid.Children.Add(ContentBorder);

                VehicleIndex++;
            }
            mainWindow.ScrollViewerContent.Content = grid;
        }

        private void InitialAssignValue(MainWindow mw, User user, MainGrid config)
        {
            mainWindow = mw;
            PUser = user;
            Config = config;
            mainWindow.AddButon.Visibility = Visibility.Visible;
            mainWindow.WhereAreYou = "VehiclesPage";
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.MainGrid.Children[2]).Children);
        }

        private void HandleContentBorderClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Border sendedBorder = (Border)sender;
            int index = Int32.Parse(sendedBorder.Name.Substring(14));
            new VehicleDetailContentGenerator().GeneratorVehicleDetail(mainWindow, PUser.Vehicles[index], PUser, Config.MainPanel.VehiclesPage);
        }

        private void HandleContentBorderMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border sendedBorder = (Border)sender;
            sendedBorder.Background = (Brush)Converter.ConvertFrom("#FF001A34");
            Grid grid = sendedBorder.Child as Grid;
            TextBlock textBlock = grid.Children[1] as TextBlock;
            textBlock.Foreground = (Brush)Converter.ConvertFrom("#FFEDF5FD");
        }

        private void HandleContentBorderMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border sendedBorder = (Border)sender;
            sendedBorder.Background = (Brush)Converter.ConvertFrom("#FFEDF5FD");
            Grid grid = sendedBorder.Child as Grid;
            TextBlock textBlock = grid.Children[1] as TextBlock;
            textBlock.Foreground = (Brush)Converter.ConvertFrom("#FF001A34");
        }

        private void VehicleButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Int32.Parse(btn.Name.Substring(13));
            new VehicleDetailContentGenerator().GeneratorVehicleDetail(mainWindow, PUser.Vehicles[index], PUser, Config.MainPanel.VehiclesPage);
        }

    }
}
