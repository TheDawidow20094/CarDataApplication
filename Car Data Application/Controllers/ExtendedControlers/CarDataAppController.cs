using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Car_Data_Application.Controllers
{
    class CarDataAppController
    {
        public MainWindow mainWindow;
        public User PUser;
        public BrushConverter Converter = new BrushConverter();
        public Config config;

        public void SetButtonColor(string ButtonName, Grid SidePanel)
        {
            //Grid SmallButtonsGrid = (Grid)SidePanel.Children;
            foreach (Grid Button in SidePanel.Children)
            {
                Border ButtonBorder = (Border)Button.Children[2];
                if (ButtonBorder.Name == ButtonName)
                {
                    ButtonBorder.Background = (Brush)Converter.ConvertFrom("#0970c4");
                }
                else
                {
                    ButtonBorder.Background = Brushes.Transparent;
                }
            }
        }

        public void GoToHomePage(MainWindow mainWindow, User user, Config paramConfig)
        {
            HomeContentGenerator OpenHomePage = new HomeContentGenerator();
            OpenHomePage.GeneratorHomeContent(mainWindow, user, paramConfig.MainPanel.HomePage);
        }

        public void SetGridProps(ref Grid Grid, int row)
        {
            Grid.Background = Brushes.WhiteSmoke;

            Grid.Margin = new Thickness(25, 10, 25, 10);

            DropShadowBitmapEffect myDropShadowEffect = new DropShadowBitmapEffect();
            myDropShadowEffect.Color = Colors.Black;
            myDropShadowEffect.Direction = 320;
            myDropShadowEffect.ShadowDepth = 5;
            myDropShadowEffect.Softness = 1;
            myDropShadowEffect.Opacity = 0.25;
            Grid.BitmapEffect = myDropShadowEffect;

            Grid.SetRow(Grid, row);

        }

        public TextBlock GenerateTextBlock(string text, int row, int column, string foregroundcolor = "#FF2A2729", HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, VerticalAlignment verticalAlignment = VerticalAlignment.Center, bool isTitle = false)
        {
            TextBlock TextBlockName = new TextBlock();
            TextBlockName.Foreground = (Brush)Converter.ConvertFromString(foregroundcolor);
            TextBlockName.FontFamily = new FontFamily("Arial Black");
            TextBlockName.FontWeight = FontWeights.Bold;
            TextBlockName.Text = text;
            TextBlockName.Margin = new Thickness(3);
            TextBlockName.VerticalAlignment = VerticalAlignment.Center;
            TextBlockName.HorizontalAlignment = horizontalAlignment;

            if (isTitle)
            {
                Grid.SetColumnSpan(TextBlockName, 2);
                TextBlockName.FontSize = 18;
                TextBlockName.FontWeight = FontWeights.Bold;
                TextBlockName.Margin = new Thickness(3, 3, 3, 3);
            }

            Grid.SetRow(TextBlockName, row);
            Grid.SetColumn(TextBlockName, column);

            return TextBlockName;
        }

        public Image GenerateIcon(string path, int row, int column)
        {
            Image Icon = new Image();
            ImageSourceConverter source = new ImageSourceConverter();
            Icon.SetValue(Image.SourceProperty, source.ConvertFromString(@path));
            Icon.Width = 64;
            Icon.Margin = new Thickness(30, 20, 20, 20);
            Icon.HorizontalAlignment = HorizontalAlignment.Left;
            Icon.VerticalAlignment = VerticalAlignment.Center;

            //Grid.SetRow(Icon, row);
            Grid.SetColumn(Icon, column);
            Grid.SetRowSpan(Icon, 3);

            return Icon;
        }
    }
}
