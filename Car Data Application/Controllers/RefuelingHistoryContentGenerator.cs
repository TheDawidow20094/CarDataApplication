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
        private MainWindow mainWindow;
        private BrushConverter Converter = new BrushConverter();

        public void GeneratorRefulingHistory(MainWindow mw, User user)
        {
            mw.WhereAreYou = "RefuelingPage";
            mainWindow = mw;
            mainWindow.AddButon.Visibility = Visibility.Visible;
            new CarDataAppController().SetButtonColor("RefuelingHistoryPageButton", mainWindow.SidePanel.Children);

            Grid MainGrid = new Grid();

            int index = 0;
            foreach (Refueling refueling in user.Vehicles[user.ActiveCarIndex].Refulings)
            {
                RowDefinition MainGridRow = new RowDefinition();
                MainGridRow.Height = new GridLength(120);
                MainGrid.RowDefinitions.Add(MainGridRow);

                Border RefuelingBorder = new Border();
                SetBorderProps(RefuelingBorder, index);

                Grid RefuelingGrid = new Grid();
                RefuelingBorder.Padding = new Thickness(15);
                RefuelingBorder.Child = RefuelingGrid;
                for (int x = 0; x <= 5; x++) //5 is number of rows
                {
                    RowDefinition RefuelingRow = new RowDefinition();
                    RefuelingGrid.RowDefinitions.Add(RefuelingRow);
                }
                for (int y = 0; y <= 3; y++) //3 is number of columns
                {
                    ColumnDefinition RefuelingColumn = new ColumnDefinition();
                    RefuelingGrid.ColumnDefinitions.Add(RefuelingColumn);
                }

                RefuelingGrid.Children.Add(GenerateIcon("../../../Images/Icons/fuelicon.png", 0, 1));

                RefuelingGrid.Children.Add(GenerateTextBlock(refueling.Date.ToString(), 0, 1));

                RefuelingGrid.Children.Add(GenerateTextBlock(refueling.CarMillage.ToString() + " km", 1, 2));

                RefuelingGrid.Children.Add(GenerateTextBlock(refueling.TotalPrice.ToString() + " zł", 2, 0));

                RefuelingGrid.Children.Add(GenerateTextBlock("milion km", 2, 2));

                RefuelingGrid.Children.Add(GenerateTextBlock(refueling.Liters.ToString() + " litrów", 3, 0));

                RefuelingGrid.Children.Add(GenerateTextBlock("500zł", 3, 2));

                RefuelingGrid.Children.Add(GenerateTextBlock("ECO DIESEL", 4, 1));

                RefuelingGrid.Children.Add(GenerateTextBlock("12L / 100km", 4, 2));

                MainGrid.Children.Add(RefuelingBorder);

                index++;
            }            
            mw.ScrollViewerContent.Content = MainGrid;
        }

        public void SetBorderProps(Border border, int row)
        {
            Brush BackgroundBrushh = (Brush)Converter.ConvertFromString("#FF001A34");
            border.Background = BackgroundBrushh;

            border.BorderThickness = new Thickness(5);
            border.BorderBrush = (Brush)Converter.ConvertFrom("#FF407BB6");
            border.CornerRadius = new CornerRadius(30);

            border.Margin = new Thickness(15, 5, 15, 5);
            border.Padding = new Thickness(0, 0, 35, 0);
            Grid.SetRow(border, row);

        }

        public Image GenerateIcon(string path, int row, int column)
        {
            Image Icon = new Image();
            ImageSourceConverter source = new ImageSourceConverter();
            Icon.SetValue(Image.SourceProperty, source.ConvertFromString(@path));
            Grid.SetRow(Icon, row);
            Grid.SetColumn(Icon, column);

            return Icon;
        }

        public TextBlock GenerateTextBlock(string text, int row, int column)
        {
            TextBlock TextBlockName = new TextBlock();
            TextBlockName.Foreground = (Brush)Converter.ConvertFromString("#FFEDF5FD");
            TextBlockName.FontFamily = new FontFamily("Arial Black");
            TextBlockName.FontWeight = FontWeights.Bold;
            TextBlockName.Text = text;
            TextBlockName.Margin = new Thickness(0);
            TextBlockName.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(TextBlockName, row);
            Grid.SetColumn(TextBlockName, column);

            return TextBlockName;
        }

    }
}
