using Car_Data_Application.Models;
using Car_Data_Application.Views;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class CarDataAppController
    {
        public void SetButtonColor(string ButtonName, UIElementCollection Buttons)
        {
            BrushConverter Converter = new BrushConverter();
            foreach (Button Button in Buttons)
            {
                if (Button.Name == ButtonName)
                {
                    Button.Foreground = (Brush)Converter.ConvertFrom("#FF001A34");
                    Button.Background = (Brush)Converter.ConvertFrom("#FFEDF5FD");
                }
                else
                {
                    Button.Foreground = (Brush)Converter.ConvertFrom("#FFEDF5FD");
                    Button.Background = Brushes.Transparent;
                }
            }
        }

        public void GoToHomePage(MainWindow mainWindow, User user)
        {
            HomeContentGenerator OpenHomePage = new HomeContentGenerator();
            OpenHomePage.GeneratorHomeContent(mainWindow, user);
        }
    }
}
