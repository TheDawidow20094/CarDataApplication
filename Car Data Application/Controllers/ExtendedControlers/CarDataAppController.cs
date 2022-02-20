
using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace Car_Data_Application.Controllers
{
    class CarDataAppController
    {
        public string TextBoxBackgroundColor = "#FFD6CFD3";
        public string TextBoxBackgroundRedColor = "#FFD68A8A";
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

        public void SetGridProps(ref Grid grid, int row = 0)
        {
            grid.Background = Brushes.WhiteSmoke;

            grid.Margin = new Thickness(25, 10, 25, 10);

            DropShadowBitmapEffect myDropShadowEffect = new DropShadowBitmapEffect();
            myDropShadowEffect.Color = Colors.Black;
            myDropShadowEffect.Direction = 320;
            myDropShadowEffect.ShadowDepth = 5;
            myDropShadowEffect.Softness = 1;
            myDropShadowEffect.Opacity = 0.25;
            grid.BitmapEffect = myDropShadowEffect;

            Grid.SetRow(grid, row);

        }

        public TextBlock GenerateTextBlock(
            Translation text,
            string language,
            int row,
            int column,
            string foregroundcolor = "#FF2A2729",
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment verticalAlignment = VerticalAlignment.Center,
            bool isTitle = false,
            int isTitleFontSize = 18,
            string textBlockName = null,
            Visibility visibility = Visibility.Visible)
        {
            TextBlock TextBlockName = new TextBlock();

            if (text != null)
            {
                switch (language)
                {
                    case "PL":
                        TextBlockName.Text = text.PL;
                        break;

                    case "ENG":
                        TextBlockName.Text = text.ENG;
                        break;
                }
            }
            else
            {
                TextBlockName.Text = language;
            }
            if (textBlockName != null)
            {
                textBlockName += "_TextBlock";
                if (null != mainWindow.FindName(textBlockName))
                {
                    mainWindow.UnregisterName(textBlockName);
                }
                mainWindow.RegisterName(textBlockName, TextBlockName);
            }

            TextBlockName.Foreground = (Brush)Converter.ConvertFromString(foregroundcolor);
            TextBlockName.FontFamily = new FontFamily("Arial Black");
            TextBlockName.FontWeight = FontWeights.Bold;
            TextBlockName.Margin = new Thickness(3);
            TextBlockName.VerticalAlignment = VerticalAlignment.Center;
            TextBlockName.HorizontalAlignment = horizontalAlignment;
            TextBlockName.Visibility = visibility;

            if (isTitle)
            {
                Grid.SetColumnSpan(TextBlockName, 2);
                TextBlockName.FontSize = isTitleFontSize;
                TextBlockName.FontWeight = FontWeights.Bold;
                TextBlockName.Margin = new Thickness(3, 3, 3, 3);
            }

            Grid.SetRow(TextBlockName, row);
            Grid.SetColumn(TextBlockName, column);

            return TextBlockName;
        }

        public TextBox GenerateTextBox(string textboxname, int row, int column, bool biggersize = false, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, string value = "", bool smallersize = false, Visibility visibility = Visibility.Visible)
        {
            TextBox textBox = new TextBox();
            textBox.Width = 120;
            textBox.Height = 30;
            textBox.Margin = new Thickness(2, 2, 6, 2);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            if (biggersize)
            {
                textBox.VerticalContentAlignment = VerticalAlignment.Top;
                textBox.TextWrapping = TextWrapping.Wrap;
                textBox.Width = 250;
                textBox.Height = 140;
                textBox.Margin = new Thickness(2,2,2,15);
            }
            if (smallersize)
            {
                textBox.Width = 100;
            }

            textBox.Text = value;
            textBox.FontSize = 16;
            textBox.TextAlignment = TextAlignment.Center;
            textBox.HorizontalAlignment = horizontalAlignment;
            textBox.BorderThickness = new Thickness(0);
            textBox.FontWeight = FontWeights.Bold;
            textBox.Visibility = visibility;
            textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
            textBox.Foreground = (Brush)Converter.ConvertFromString(DarkTextColor);
            textBox.FontFamily = new FontFamily("Global User Interface");

            string TextBoxName = String.Concat(textboxname.Where(c => !Char.IsWhiteSpace(c))) + "_TextBox";

            
            if (null != mainWindow.FindName(TextBoxName))
            {
                mainWindow.UnregisterName(TextBoxName);
            }
            mainWindow.RegisterName(TextBoxName, textBox);
            textBox.SetValue(FrameworkElement.NameProperty, TextBoxName);

            Grid.SetRow(textBox, row);
            Grid.SetColumn(textBox, column);

            return textBox;
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

        public DatePicker GenerateDatePicker(string textboxname, int row, int column, DateTime selectedDate, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, CalendarDateRange calendarDateRange = null)
        {
            DatePicker datePicker = new();

            datePicker.SelectedDate = selectedDate;
            datePicker.BlackoutDates.Add(calendarDateRange);

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
            string DatePickerName = TrimmedText + "_DatePicker";

            if (mainWindow.FindName(DatePickerName) != null)
            {
                mainWindow.UnregisterName(DatePickerName);
            }
            mainWindow.RegisterName(DatePickerName, datePicker);
            Grid.SetRow(datePicker, row);
            Grid.SetColumn(datePicker, column);


            return datePicker;
        }
        
        public Button GenerateButton(Translation text, string language, int row, int column, string foregroundcolor = "#FF9C9397", int fontSize = 18, bool RegisterName = true, string buttonName = null)
        {
            Button button = new();

            switch (language)
            {
                case "PL":
                    button.Content = text.PL;
                    break;

                case "ENG":
                    button.Content = text.ENG;
                    break;
            }

            button.FontFamily = new FontFamily("Global User Interface");
            button.FontSize = fontSize;
            button.FontWeight = FontWeights.Bold;

            button.Height = 45;
            button.Width = 140;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Margin = new Thickness(10);
            button.BorderThickness = new Thickness(0);

            button.Foreground = (Brush)Converter.ConvertFromString(foregroundcolor);
            button.Background = Brushes.WhiteSmoke;

            DropShadowBitmapEffect myDropShadowEffect = new DropShadowBitmapEffect();
            myDropShadowEffect.Color = Colors.Black;
            myDropShadowEffect.Direction = 320;
            myDropShadowEffect.ShadowDepth = 5;
            myDropShadowEffect.Softness = 1;
            myDropShadowEffect.Opacity = 0.25;
            button.BitmapEffect = myDropShadowEffect;

            if (buttonName == null)
            {
                string ButtonName = String.Concat(text.ENG.Where(c => !Char.IsWhiteSpace(c))) + "_Button";
                button.SetValue(FrameworkElement.NameProperty, ButtonName);
                if (null != mainWindow.FindName(ButtonName))
                {
                    mainWindow.UnregisterName(ButtonName);
                }
                mainWindow.RegisterName(ButtonName, button);
            }
            else 
            {
                button.SetValue(FrameworkElement.NameProperty, buttonName);
            }
            
            Grid.SetRow(button, row);
            Grid.SetColumn(button, column);

            return button;
        }

        public ToggleButton GenerateToggleButton(Translation text, string language, int row, int column)
        {
            ToggleButton toggleButton = new ToggleButton();
            switch (language)
            {
                case "PL":
                    toggleButton.Content = text.PL;
                    break;

                case "ENG":
                    toggleButton.Content = text.ENG;
                    break;
            }
            toggleButton.FontFamily = new FontFamily("Global User Interface");
            toggleButton.FontSize = 18;
            toggleButton.FontWeight = FontWeights.Bold;

            toggleButton.Height = 45;
            toggleButton.Width = 140;
            toggleButton.HorizontalAlignment = HorizontalAlignment.Center;
            toggleButton.VerticalAlignment = VerticalAlignment.Center;
            toggleButton.Margin = new Thickness(10);
            toggleButton.BorderThickness = new Thickness(0);

            toggleButton.Foreground = (Brush)Converter.ConvertFromString(LightTextColor);
            toggleButton.Background = Brushes.WhiteSmoke;

            DropShadowBitmapEffect myDropShadowEffect = new DropShadowBitmapEffect();
            myDropShadowEffect.Color = Colors.Black;
            myDropShadowEffect.Direction = 320;
            myDropShadowEffect.ShadowDepth = 5;
            myDropShadowEffect.Softness = 1;
            myDropShadowEffect.Opacity = 0.25;
            toggleButton.BitmapEffect = myDropShadowEffect;

            toggleButton.SetValue(FrameworkElement.NameProperty, text.ENG + "_ToggleButton");
            if (null != mainWindow.FindName(text.ENG + "_ToggleButton"))
            {
                mainWindow.UnregisterName(text.ENG + "_ToggleButton");
            }
            mainWindow.RegisterName(text.ENG + "_ToggleButton", toggleButton);

            Grid.SetRow(toggleButton, row);
            Grid.SetColumn(toggleButton, column);

            return toggleButton;
        }

        public Grid GenerateToggleSwitch(string toggleSwitchName, int row, int column, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left)
        {
            Grid toggleSwitch = new Grid();
            toggleSwitch.VerticalAlignment = VerticalAlignment.Center;
            toggleSwitch.HorizontalAlignment = horizontalAlignment;
            toggleSwitch.Margin = new Thickness(3);

            Rectangle rectangle = new Rectangle();
            rectangle.Height = 28;
            rectangle.Width = 56;
            rectangle.RadiusX = 14;
            rectangle.RadiusY = 14;
            rectangle.Fill = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
            toggleSwitch.Children.Add(rectangle);

            Ellipse ellipse = new Ellipse();
            ellipse.Fill = Brushes.Gray;
            ellipse.Width = 19;
            ellipse.Height = 19;
            ellipse.Margin = new Thickness(5, 0, 5, 0);
            ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            ellipse.VerticalAlignment = VerticalAlignment.Center;

            toggleSwitch.Children.Add(ellipse);
            toggleSwitch.MouseLeftButtonDown += ToggleSwitchClick;
            toggleSwitch.Tag = false;

            string TrimmedText = String.Concat(toggleSwitchName.Where(c => !Char.IsWhiteSpace(c)));
            if (null != mainWindow.FindName(TrimmedText + "_ToggleSwitch"))
            {
                mainWindow.UnregisterName(TrimmedText + "_ToggleSwitch");
            }
            mainWindow.RegisterName(TrimmedText + "_ToggleSwitch", toggleSwitch);

            Grid.SetRow(toggleSwitch, row);
            Grid.SetColumn(toggleSwitch, column);

            return toggleSwitch;
        }

        public ComboBox GenerateComboBox(string comboBoxName, int row, int column, List<ComboBoxItem> ComboBoxItems, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, int width = 120)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Foreground = (Brush)Converter.ConvertFromString(DarkTextColor);
            comboBox.FontWeight = FontWeights.Bold;
            comboBox.Margin = new Thickness(2, 2, 6, 2);
            comboBox.HorizontalAlignment = horizontalAlignment;
            comboBox.FontSize = 16;
            comboBox.Height = 30;
            comboBox.Width = width;

            foreach (ComboBoxItem itemValue in ComboBoxItems)
            {
                if (itemValue.Tag != null)
                {
                    switch (itemValue.Content)
                    {
                        case"PL":
                            itemValue.Content = ((Translation)itemValue.Tag).PL;
                            break;

                        case "ENG":
                            itemValue.Content = ((Translation)itemValue.Tag).ENG;
                            break;
                    }
                }

                comboBox.Items.Add(itemValue);
            }
            comboBox.SelectedIndex = 0;

            string TrimmedText = String.Concat(comboBoxName.Where(c => !Char.IsWhiteSpace(c)));
            if (null != mainWindow.FindName(TrimmedText + "_ComboBox"))
            {
                mainWindow.UnregisterName(TrimmedText + "_ComboBox");
            }
            mainWindow.RegisterName(TrimmedText + "_ComboBox", comboBox);

            Grid.SetRow(comboBox, row);
            Grid.SetColumn(comboBox, column);


            return comboBox;
        }

        private void ToggleSwitchClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid grid = (Grid)sender;
            Rectangle rectangle = ((Rectangle)grid.Children[0]);
            Ellipse ellipse = ((Ellipse)grid.Children[1]);

            if ((bool)grid.Tag)
            {
                rectangle.Fill = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
                ellipse.Fill = Brushes.Gray;
                ellipse.HorizontalAlignment = HorizontalAlignment.Left;
                grid.Tag = false;
            }
            else
            {
                rectangle.Fill = (Brush)Converter.ConvertFromString("#FF93D68A");
                ellipse.Fill = Brushes.WhiteSmoke;
                ellipse.HorizontalAlignment = HorizontalAlignment.Right;
                grid.Tag = true;
            }
        }

        private string RemoveSpecialCharacters(string str)
        {
            List<char> charsToRemove = new List<char>() {'_', '-', '+', '*', '/', '>', '<', ';', ':', '|', '=', '!', '@', '#'};
            //str = "";
            foreach (char c in charsToRemove)
            {
                str = str.Replace(c.ToString(), string.Empty); 
            }
            return str;
        }
    }
}
