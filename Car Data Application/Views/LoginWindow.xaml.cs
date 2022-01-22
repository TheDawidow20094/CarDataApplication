using Car_Data_Application.Controllers;
using Car_Data_Application.Models;
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
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private MainWindow mainWindow;
        private User PUser;

        public LoginWindow(MainWindow mw, User user)
        {
            mainWindow = mw;
            PUser = user;
            new CarDataAppController().SetButtonColor("LoginPageButton", mainWindow.SidePanel.Children);
            InitializeComponent();
            this.Closed += LoginWindow_Closed;
        }

        private void LoginWindow_Closed(object sender, EventArgs e)
        {
            new CarDataAppController().GoToHomePage(mainWindow, PUser);
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logowanie");
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            RegisterWindow registerWindow = new RegisterWindow(mainWindow, PUser);
            registerWindow.ShowDialog();
        }

    }
}
