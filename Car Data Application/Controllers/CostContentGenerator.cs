using Car_Data_Application.Models;
using Car_Data_Application.Models.Vehicle_Classes;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class CostContentGenerator : CarDataAppController
    {
        public void CostGenerator(MainWindow mw, User user, CostPage translation)
        {
            InitialAssignValue(mw, user);

            Grid MainGrid = new Grid();

            int index = 0;
            foreach (Service servis in user.Vehicles[user.ActiveCarIndex].Services)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(150) });

                Grid CostInfoGrid = new Grid();
                SetGridProps(ref CostInfoGrid, index);

                CostInfoGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(114) });
                CostInfoGrid.ColumnDefinitions.Add(new ColumnDefinition());

                CostInfoGrid.Children.Add(GenerateIcon("../../../Images/Icons/dollar.png", 0, 0));

                Grid CostInfoGridContent = new Grid();
                CostInfoGridContent.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(CostInfoGridContent, 1);

                for (int x = 0; x < 5; x++)
                {
                    RowDefinition CostInfoRow = new RowDefinition();
                    CostInfoGridContent.RowDefinitions.Add(CostInfoRow);
                }
                for (int y = 0; y < 2; y++)
                {
                    ColumnDefinition CostInfoColumn = new ColumnDefinition();
                    CostInfoGridContent.ColumnDefinitions.Add(CostInfoColumn);
                }

                string LightTextColor = "#FF9C9397";
                string DarkTextColor = "#FF2A2729"; // change to set in config

                switch (PUser.UserLanguage)
                {
                    case "PL":
                        CostInfoGridContent.Children.Add(GenerateTextBlock(translation.Type.PL, 1, 0, LightTextColor, HorizontalAlignment.Right));
                        CostInfoGridContent.Children.Add(GenerateTextBlock(translation.Date.PL, 2, 0, LightTextColor, HorizontalAlignment.Right));
                        CostInfoGridContent.Children.Add(GenerateTextBlock(translation.Cost.PL, 3, 0, LightTextColor, HorizontalAlignment.Right));
                        CostInfoGridContent.Children.Add(GenerateTextBlock(translation.Comment.PL, 4, 0, LightTextColor, HorizontalAlignment.Right));
                        break;

                    case "ENG":
                        CostInfoGridContent.Children.Add(GenerateTextBlock(translation.Type.ENG, 1, 0, LightTextColor, HorizontalAlignment.Right));
                        CostInfoGridContent.Children.Add(GenerateTextBlock(translation.Date.ENG, 2, 0, LightTextColor, HorizontalAlignment.Right));
                        CostInfoGridContent.Children.Add(GenerateTextBlock(translation.Cost.ENG, 3, 0, LightTextColor, HorizontalAlignment.Right));
                        CostInfoGridContent.Children.Add(GenerateTextBlock(translation.Comment.ENG, 4, 0, LightTextColor, HorizontalAlignment.Right));
                        break;
                }

                CostInfoGridContent.Children.Add(GenerateTextBlock(servis.Name.ToString(), 0, 0, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));

                CostInfoGridContent.Children.Add(GenerateTextBlock(servis.Category.ToString(), 1, 1));

                CostInfoGridContent.Children.Add(GenerateTextBlock(servis.Date.ToString(), 2, 1));

                CostInfoGridContent.Children.Add(GenerateTextBlock(servis.Price.ToString() + " zł", 3, 1));

                CostInfoGridContent.Children.Add(GenerateTextBlock(servis.Comment.ToString(), 4, 1));

                CostInfoGrid.Children.Add(CostInfoGridContent);

                MainGrid.Children.Add(CostInfoGrid);
                index++;
            }        
            mw.ScrollViewerContent.Content = MainGrid;
        }

        private void InitialAssignValue(MainWindow mw, User user)
        {
            mw.WhereAreYou = "CostsPage";
            mainWindow = mw;
            PUser = user;
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.MainGrid.Children[3]));
        }

        //private void SetBorderProps(Border border, int row)
        //{
        //    Brush BackgroundBrushh = (Brush)Converter.ConvertFromString("#FF001A34");
        //    border.Background = BackgroundBrushh;

        //    border.BorderThickness = new Thickness(5);
        //    border.BorderBrush = (Brush)Converter.ConvertFrom("#FF407BB6");
        //    border.CornerRadius = new CornerRadius(30);

        //    border.Margin = new Thickness(15, 5, 15, 5);
        //    border.Padding = new Thickness(0, 0, 35, 0);
        //    Grid.SetRow(border, row);

        //}

        //private Image GenerateIcon(string path, int row, int column)
        //{
        //    Image Icon = new Image();
        //    ImageSourceConverter source = new ImageSourceConverter();
        //    Icon.SetValue(Image.SourceProperty, source.ConvertFromString(@path));
        //    Icon.Width = 30;
        //    Grid.SetRow(Icon, row);
        //    Grid.SetColumn(Icon, column);

        //    return Icon;
        //}

        //private TextBlock GenerateTextBlock(string text, int row, int column)
        //{
        //    TextBlock TextBlockName = new TextBlock();
        //    TextBlockName.Foreground = (Brush)Converter.ConvertFromString("#FFEDF5FD");
        //    TextBlockName.FontFamily = new FontFamily("Arial Black");
        //    TextBlockName.FontWeight = FontWeights.Bold;
        //    TextBlockName.Text = text;
        //    TextBlockName.Margin = new Thickness(0, 2, 0, 2);
        //    TextBlockName.VerticalAlignment = VerticalAlignment.Center;
        //    Grid.SetRow(TextBlockName, row);
        //    Grid.SetColumn(TextBlockName, column);

        //    return TextBlockName;
        //}
    }
}
