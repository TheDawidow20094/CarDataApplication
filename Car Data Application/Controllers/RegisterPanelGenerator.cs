using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Car_Data_Application.Controllers
{
    class RegisterPanelGenerator :CarDataAppController
    {
        private string LastOpenedPage;
        private Grid GrayedGrid;

        private Storyboard EntryAnimationStoryboard = new();
        private Storyboard ExitAnimationStoryboard = new();

        public Storyboard myWidthAnimatedButtonStoryboard = new Storyboard();

        public void PageGenerator(MainWindow mw, User user, RegisterPanel translation, string lastOpenedPage, Grid grayedGrid)
        {
            InitialAssignValue(mw, user, lastOpenedPage, grayedGrid);

            Grid RegisterWindowGrid = new();
            SetGridProps(ref RegisterWindowGrid);

            RegisterWindowGrid.Width = 350;
            RegisterWindowGrid.Height = 415;
            RegisterWindowGrid.HorizontalAlignment = HorizontalAlignment.Center;
            RegisterWindowGrid.VerticalAlignment = VerticalAlignment.Center;

            TranslateTransform translateTransform = new TranslateTransform();
            if (null != mainWindow.FindName("Transform"))
            {
                mainWindow.UnregisterName("Transform");
            }
            mainWindow.RegisterName("Transform", translateTransform);
            RegisterWindowGrid.RenderTransform = translateTransform;

            GenerateAnimation(ref RegisterWindowGrid, "Entry");
            GenerateAnimation(ref RegisterWindowGrid, "Exit");

            for (int i = 0; i < 2; i++)
            {
                RegisterWindowGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            RegisterWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });
            RegisterWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60) });
            RegisterWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60) });
            RegisterWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60) });
            RegisterWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });
            RegisterWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });

            switch (PUser.UserLanguage)
            {
                case "PL":
                    RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.RegisterTitle.PL, 0, 0, horizontalAlignment: HorizontalAlignment.Center, isTitleFontSize: 28, isTitle: true));
                    RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.UserName.PL, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.Password.PL, 2, 0, LightTextColor, HorizontalAlignment.Right));
                    RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.RePassword.PL, 3, 0, LightTextColor, HorizontalAlignment.Right));
                    RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.Email.PL, 4, 0, LightTextColor, HorizontalAlignment.Right));

                    break;

                case "ENG":
                    RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.RegisterTitle.ENG, 0, 0, horizontalAlignment: HorizontalAlignment.Center, isTitleFontSize: 28, isTitle: true));
                    RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.UserName.ENG, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.Password.ENG, 2, 0, LightTextColor, HorizontalAlignment.Right));
                    RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.RePassword.ENG, 3, 0, LightTextColor, HorizontalAlignment.Right));
                    RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.Email.ENG, 4, 0, LightTextColor, HorizontalAlignment.Right));

                    break;
            }

            RegisterWindowGrid.Children.Add(GenerateTextBox("UserName", 1, 1));
            RegisterWindowGrid.Children.Add(GenerateTextBox("Password", 2, 1));
            RegisterWindowGrid.Children.Add(GenerateTextBox("RePassword", 3, 1));
            RegisterWindowGrid.Children.Add(GenerateTextBox("Email", 4, 1));

            Button RegisterButton = GenerateButton(translation.RegisterButton, PUser.UserLanguage, 5, 0);
            Grid.SetColumnSpan(RegisterButton, 2);
            RegisterButton.Click += RegisterButtonClick; ;
            RegisterWindowGrid.Children.Add(RegisterButton);


            GrayedGrid.Children.Add(RegisterWindowGrid);

            mainWindow.BeginStoryboard(EntryAnimationStoryboard);
        }

        private void InitialAssignValue(MainWindow mw, User user, string lastOpenedPage, Grid grayedGrid)
        {
            LastOpenedPage = mw.WhereAreYou;
            mw.WhereAreYou = "LoginPage";
            mainWindow = mw;
            PUser = user;
            LastOpenedPage = lastOpenedPage;
            GrayedGrid = grayedGrid;
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.MainGrid.Children[3]));
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Rejestrowanie");
        }

        private void GenerateAnimation(ref Grid LoginWindowGrid, string animationType)
        {
            double MoveForm = 0, MoveTo = 0, OpacityFrom = 0, OpacityTo = 0;
            Storyboard storyboard = new Storyboard();

            switch (animationType)
            {
                case "Entry":
                    storyboard = EntryAnimationStoryboard;
                    MoveForm = 700;
                    MoveTo = 0;

                    break;

                case "Exit":
                    storyboard = ExitAnimationStoryboard;
                    MoveForm = 0;
                    MoveTo = -700;
                    OpacityFrom = 0.6;
                    OpacityTo = 0.0;

                    break;
            }

            DoubleAnimation MoveAnimation = new DoubleAnimation();
            MoveAnimation.From = MoveForm;
            MoveAnimation.To = MoveTo;
            MoveAnimation.BeginTime = new TimeSpan(0, 0, 0);
            MoveAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(250));

            Storyboard.SetTargetName(MoveAnimation, "Transform");
            Storyboard.SetTargetProperty(MoveAnimation, new PropertyPath(TranslateTransform.XProperty));

            storyboard.Children.Clear();
            storyboard.Children.Add(MoveAnimation);

            if (animationType == "Exit")
            {
                DoubleAnimation OpacityAnimation = new DoubleAnimation();
                OpacityAnimation.From = OpacityFrom;
                OpacityAnimation.To = OpacityTo;
                OpacityAnimation.BeginTime = new TimeSpan(0, 0, 0);
                OpacityAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));


                Storyboard.SetTargetName(OpacityAnimation, "BlackOpacityGrid");
                Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath(Grid.OpacityProperty));

                storyboard.Children.Add(OpacityAnimation);
            }
        }
    }
}
