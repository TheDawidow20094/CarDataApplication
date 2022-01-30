using Car_Data_Application.Models;
using Car_Data_Application.Models.Vehicle_Classes;
using Car_Data_Application.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class CostContentGenerator : CarDataAppController
    {
        public void CostGenerator(MainWindow mw, User user)
        {
            InitialAssignValue(mw, user);

            Grid MainGrid = new Grid();

            int index = 0;
            foreach (Service servis in user.Vehicles[user.ActiveCarIndex].Services)
            {
                RowDefinition MainGridRow = new RowDefinition();
                MainGrid.RowDefinitions.Add(MainGridRow);

                Border CostInfoBorder = new Border();
                SetBorderProps(CostInfoBorder, index);

                Grid CostInfoGrid = new Grid();
                CostInfoBorder.Padding = new Thickness(20);
                CostInfoBorder.Child = CostInfoGrid;
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

                CostInfoGrid.Children.Add(GenerateIcon("../../../Images/Icons/cost.png", 0, 1));

                CostInfoGrid.Children.Add(GenerateTextBlock(servis.Name.ToString(), 0, 0));

                CostInfoGrid.Children.Add(GenerateTextBlock(servis.Price.ToString() + " zł", 0, 2));

                CostInfoGrid.Children.Add(GenerateTextBlock(servis.Date.ToString(), 1, 0));

                CostInfoGrid.Children.Add(GenerateTextBlock(servis.Category.ToString(), 2, 0));

                CostInfoGrid.Children.Add(GenerateTextBlock(servis.Comment.ToString(), 3, 0));

                MainGrid.Children.Add(CostInfoBorder);
                index++;
            }        
            mw.ScrollViewerContent.Content = MainGrid;
        }

        private void InitialAssignValue(MainWindow mw, User user)
        {
            mw.WhereAreYou = "CostsPage";
            mainWindow = mw;
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.MainGrid.Children[3]).Children);
        }

        private void SetBorderProps(Border border, int row)
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

        private Image GenerateIcon(string path, int row, int column)
        {
            Image Icon = new Image();
            ImageSourceConverter source = new ImageSourceConverter();
            Icon.SetValue(Image.SourceProperty, source.ConvertFromString(@path));
            Icon.Width = 30;
            Grid.SetRow(Icon, row);
            Grid.SetColumn(Icon, column);

            return Icon;
        }

        private TextBlock GenerateTextBlock(string text, int row, int column)
        {
            TextBlock TextBlockName = new TextBlock();
            TextBlockName.Foreground = (Brush)Converter.ConvertFromString("#FFEDF5FD");
            TextBlockName.FontFamily = new FontFamily("Arial Black");
            TextBlockName.FontWeight = FontWeights.Bold;
            TextBlockName.Text = text;
            TextBlockName.Margin = new Thickness(0, 2, 0, 2);
            TextBlockName.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(TextBlockName, row);
            Grid.SetColumn(TextBlockName, column);

            return TextBlockName;
        }
    }
}
