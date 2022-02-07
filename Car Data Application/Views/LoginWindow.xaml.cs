using Car_Data_Application.Controllers;
using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Car_Data_Application.Views
{
    public partial class LoginWindow : Window
    {
        private MainWindow mainWindow;
        private User PUser;
        private Config config;

        public LoginWindow(MainWindow mw, User user, Config paramConfig)
        {
            InitialAssignValue(mw, user, paramConfig);
            InitializeComponent();
            TranslateControlersValue(config.MainPanel.LoginPanel);
            this.Closed += LoginWindow_Closed;
        }

        private void InitialAssignValue(MainWindow mw, User user, Config paramConfig)
        {
            config = paramConfig;
            mainWindow = mw;
            PUser = user;
            mainWindow.WhereAreYou = "MyAccountPage";
            new CarDataAppController().SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.MainGrid.Children[3]));
        }

        private void LoginWindow_Closed(object sender, EventArgs e)
        {
            new CarDataAppController().GoToHomePage(mainWindow, PUser, config);
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logowanie");
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            new RegisterWindow(mainWindow, PUser, config).ShowDialog();
        }

        private void TranslateControlersValue(LoginPanel translation)
        {
            switch (PUser.UserLanguage)
            {
                case "PL":
                    this.Title = translation.LogInButton.PL;

                    UserNameTextBlock.Text = translation.UserName.PL;
                    PasswordTextBlock.Text = translation.Password.PL;
                    LoginButton.Content = translation.LogInButton.PL;
                    RegisterButton.Content = translation.RegisterButton.PL;
                    break;

                case "ENG":
                    this.Title = translation.LogInButton.ENG;

                    UserNameTextBlock.Text = translation.UserName.ENG;
                    PasswordTextBlock.Text = translation.Password.ENG;
                    LoginButton.Content = translation.LogInButton.ENG;
                    RegisterButton.Content = translation.RegisterButton.ENG;
                    break;
            }
        }

    }
}
