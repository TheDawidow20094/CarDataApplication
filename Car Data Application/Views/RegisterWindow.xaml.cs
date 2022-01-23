﻿using Car_Data_Application.Controllers;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private MainWindow mainWindow;
        private User PUser;

        public RegisterWindow(MainWindow mw, User user)
        {
            PUser = user;
            mainWindow = mw;
            new CarDataAppController().SetButtonColor("LoginPageButton", mainWindow.SidePanel.Children);
            InitializeComponent();
            this.Closed += RegisterWindow_Closed;
        }

        private void RegisterWindow_Closed(object sender, EventArgs e)
        {
            new CarDataAppController().GoToHomePage(mainWindow, PUser);
        }
    }
}