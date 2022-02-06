using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Car_Data_Application.Controllers
{
    class CarDataAppController
    {
        public string TextBoxBackgroundColor = "#FFD6CFD3";
        public string LightTextColor = "#FF9C9397";
        public string DarkTextColor = "#FF2A2729"; // change to set in config

        public MainWindow mainWindow;
        public User PUser;
        public BrushConverter Converter = new BrushConverter();
        public Config config;

        public void SetButtonColor(string ButtonName, Grid SidePanel)
        {
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

        public TextBox GenerateTextBox(string textboxname, int row, int column, bool biggersize = false, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, string value = "")
        {
            TextBox TextBoxName = new TextBox();
            TextBoxName.Width = biggersize ? 250 : 120;
            TextBoxName.Height = 30;
            if (biggersize) { TextBoxName.Height = 130; TextBoxName.Margin = new Thickness(0,0,0,15); }

            TextBoxName.Text = value;
            TextBoxName.Margin = new Thickness(2, 2, 6, 2);
            TextBoxName.HorizontalAlignment = horizontalAlignment;
            TextBoxName.BorderThickness = new Thickness(0);
            TextBoxName.FontWeight = FontWeights.Bold;
            TextBoxName.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
            TextBoxName.Foreground = (Brush)Converter.ConvertFromString(DarkTextColor);
            TextBoxName.FontFamily = new FontFamily("Global User Interface");

            string TrimmedText = String.Concat(textboxname.Where(c => !Char.IsWhiteSpace(c)));

            TextBoxName.SetValue(FrameworkElement.NameProperty, TrimmedText + "_Textbox");
            Grid.SetRow(TextBoxName, row);
            Grid.SetColumn(TextBoxName, column);

            return TextBoxName;
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

        public DatePicker GenerateDatePicker(string textboxname, int row, int column, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left)
        {
            DatePicker datePicker = new();

            datePicker.SelectedDate = DateTime.Now;
            datePicker.Width = 120;
            datePicker.Height = 30;
            datePicker.Margin = new Thickness(2, 2, 6, 2);
            datePicker.HorizontalAlignment = horizontalAlignment;
            datePicker.BorderThickness = new Thickness(0);
            datePicker.FontWeight = FontWeights.Bold;
            //datePicker.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
            datePicker.Foreground = (Brush)Converter.ConvertFromString(DarkTextColor);
            datePicker.FontFamily = new FontFamily("Global User Interface");

            string TrimmedText = String.Concat(textboxname.Where(c => !Char.IsWhiteSpace(c)));

            datePicker.SetValue(FrameworkElement.NameProperty, TrimmedText + "_DatePicker");
            Grid.SetRow(datePicker, row);
            Grid.SetColumn(datePicker, column);


            return datePicker;
        }
    }
}
