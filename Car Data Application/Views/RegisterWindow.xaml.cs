﻿using Car_Data_Application.Controllers;
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
    public partial class RegisterWindow : Window
    {
        private MainWindow mainWindow;
        private User PUser;
        private Config config;

        public RegisterWindow(MainWindow mw, User user, Config paramConfig)
        {
            InitialAssignValue(mw, user, paramConfig);
            InitializeComponent();
            TranslateControlersValue(config.MainPanel.RegisterPanel);
            this.Closed += RegisterWindow_Closed;
        }

        private void InitialAssignValue(MainWindow mw, User user, Config paramConfig)
        {
            config = paramConfig;
            PUser = user;
            mainWindow = mw;
            mainWindow.WhereAreYou = "MyAccountPage";
            new CarDataAppController().SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.MainGrid.Children[3]));
        }

        private void RegisterWindow_Closed(object sender, EventArgs e)
        {
            new CarDataAppController().GoToHomePage(mainWindow, PUser, config);
        }

        private void TranslateControlersValue(RegisterPanel translation)
        {
            switch (PUser.UserLanguage)
            {
                case "PL":
                    this.Title = translation.RegisterButton.PL;

                    UserNameTextBlock.Text = translation.UserName.PL;
                    PasswordTextBlock.Text = translation.Password.PL;
                    RePasswordTextBlock.Text = translation.RePassword.PL;
                    EmailTextBlock.Text = translation.Email.PL;
                    RegisterButton.Content = translation.RegisterButton.PL;
                    break;

                case "ENG":
                    this.Title = translation.RegisterButton.ENG;

                    UserNameTextBlock.Text = translation.UserName.ENG;
                    PasswordTextBlock.Text = translation.Password.ENG;
                    RePasswordTextBlock.Text = translation.RePassword.ENG;
                    EmailTextBlock.Text = translation.Email.ENG;
                    RegisterButton.Content = translation.RegisterButton.ENG;
                    break;
            }
        }
    }
}
