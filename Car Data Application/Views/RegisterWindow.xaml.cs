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

        public RegisterWindow(MainWindow mw)
        {
            mainWindow = mw;
            SetButtonColor();
            InitializeComponent();
            this.Closed += SetAllButtonColorToWhite;
        }

        public void SetButtonColor()
        {
            BrushConverter bc = new BrushConverter();
            mainWindow.HomePageButton.Background = Brushes.White;
            mainWindow.LoginPageButton.Background = (Brush)bc.ConvertFrom("#07EDE9");
            mainWindow.CarPageButton.Background = Brushes.White;
            mainWindow.RefuelingHistoryPageButton.Background = Brushes.White;
            mainWindow.StatsPageButton.Background = Brushes.White;
            mainWindow.CostPageButton.Background = Brushes.White;
            mainWindow.BackupPageButton.Background = Brushes.White;
            mainWindow.SetingPaneButton.Background = Brushes.White;
        }

        private void SetAllButtonColorToWhite(object sender, EventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            mainWindow.HomePageButton.Background = Brushes.White;
            mainWindow.LoginPageButton.Background = Brushes.White;
            mainWindow.CarPageButton.Background = Brushes.White;
            mainWindow.RefuelingHistoryPageButton.Background = Brushes.White;
            mainWindow.StatsPageButton.Background = Brushes.White;
            mainWindow.CostPageButton.Background = Brushes.White;
            mainWindow.BackupPageButton.Background = Brushes.White;
            mainWindow.SetingPaneButton.Background = Brushes.White;
        }
    }
}
