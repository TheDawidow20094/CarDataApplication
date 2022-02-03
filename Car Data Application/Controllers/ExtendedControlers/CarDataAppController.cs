using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System.Windows.Controls;
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class CarDataAppController
    {
        public MainWindow mainWindow;
        public User PUser;
        public BrushConverter Converter = new BrushConverter();
        public MainGrid Config;

        public void SetButtonColor(string ButtonName, UIElementCollection Buttons)
        {
            BrushConverter Converter = new BrushConverter();
            foreach (Grid Button in Buttons)
            {
                Border ButtonBorder = (Border)Button.Children[2];
                if (Button.Name == ButtonName)
                {
                    ButtonBorder.Background = (Brush)Converter.ConvertFrom("#0970c4");
                }
                else
                {
                    ButtonBorder.Background = Brushes.Transparent;
                }
            }
        }

        public void GoToHomePage(MainWindow mainWindow, User user, MainGrid config)
        {
            HomeContentGenerator OpenHomePage = new HomeContentGenerator();
            OpenHomePage.GeneratorHomeContent(mainWindow, user, config.MainPanel.HomePage);
        }
    }
}
