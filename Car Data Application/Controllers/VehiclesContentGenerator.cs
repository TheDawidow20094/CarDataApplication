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
using System.Text.Json;
using System.Windows.Media.Imaging;

namespace Car_Data_Application.Controllers
{
    class VehiclesContentGenerator
    {                                                                 
        private MainWindow mainWindow;
        private User User;

        public void GeneratorVechicleList(MainWindow mw, User user)
        {
            mainWindow = mw;
            User = user;
            mainWindow.AddButon.Visibility = Visibility.Visible;
            new CarDataAppController().SetButtonColor("CarPageButton", mainWindow.SidePanel.Children);

            Grid grid = new Grid();
            BrushConverter converter = new BrushConverter();

            int VehicleIndex = 0;
            foreach (Vehicle vehicle in User.Vehicles)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(120);
                grid.RowDefinitions.Add(row);

                Border ContentBorder = new Border();
                Brush BackgroundBrushh = (Brush)converter.ConvertFromString("#FF001A34");
                Brush ForegroundBrushh = (Brush)converter.ConvertFromString("#FFEDF5FD");
                ContentBorder.Background = BackgroundBrushh;
                ContentBorder.BorderThickness = new Thickness(5);
                ContentBorder.CornerRadius = new CornerRadius(45);

                //ContentBorder.Margin = new Thickness(10);
                ContentBorder.Padding = new Thickness(0, 0, 35, 0);
                ContentBorder.Height = 120;
                ContentBorder.BorderThickness = new Thickness(5);


                ContentBorder.MouseEnter += Border_MouseEnter;
                ContentBorder.MouseLeave += Border_MouseLeave;

                Border PictureBorder = new Border();
                PictureBorder.Width = 60;
                PictureBorder.Height = 60;

                BitmapImage img = new BitmapImage(new Uri("pack://application:,,,/Images/car.png"));
                ImageBrush PictureBorderImage = new ImageBrush();
                PictureBorderImage.ImageSource = img;

                //PictureBorderImage.Stretch = Stretch.Fill;

                PictureBorder.Background = PictureBorderImage;
                //PictureBorder.Background = Brushes.Red;


                //----------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------

                //Button VehicleButton = new Button();
                //VehicleButton.Name = "ButtonVehicle" + vehicle.Id.ToString();
                //VehicleButton.Content = vehicle.Brand + " " + vehicle.Model;

                //VehicleButton.Margin = new Thickness(10);
                //VehicleButton.Padding = new Thickness(0, 0, 35, 0);
                //VehicleButton.Height = 120;

                //Brush BackgroundBrush = (Brush)converter.ConvertFromString("#FF407BB6");
                //VehicleButton.Background = BackgroundBrush;
                //Brush ForegroundBrush = (Brush)converter.ConvertFromString("#FFEDF5FD");
                //VehicleButton.Foreground = ForegroundBrush;
                //VehicleButton.BorderThickness = new Thickness(5);

                //VehicleButton.Click += VehicleButtonClick;

                //VehicleButton.HorizontalContentAlignment = HorizontalAlignment.Right;
                //VehicleButton.FontFamily = new FontFamily("Arial Black");
                //VehicleButton.FontSize = 20;
                //VehicleButton.FontWeight = FontWeights.Bold;


                //border.Child = VehicleButton;



                //Image image = new Image();
                //ImageSourceConverter source = new ImageSourceConverter();
                //image.Margin = new Thickness(10);
                //image.HorizontalAlignment = HorizontalAlignment.Left;                        
                //image.SetValue(Image.SourceProperty, source.ConvertFromString("pack://application:,,,/Images/draw.png"));


                Grid.SetRow(PictureBorder, VehicleIndex);
                Grid.SetRow(ContentBorder, VehicleIndex);

                grid.Children.Add(ContentBorder);
                grid.Children.Add(PictureBorder);

                VehicleIndex++;
            }
            mainWindow.ScrollViewerContent.Content = grid;
        }

        private void Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border x = (Border)sender;
            x.Background = Brushes.Yellow;
        }

        private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border x = (Border)sender;
            x.Background = Brushes.Black;
        }

        private void AddVehicle(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("Dodajemy pojazd");
        }

        private void VehicleButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Int32.Parse(btn.Name.Substring(13));
            new VehicleDetailContentGenerator().GeneratorVehicleDetail(mainWindow, User.Vehicles[index]);
        }
    }
}
