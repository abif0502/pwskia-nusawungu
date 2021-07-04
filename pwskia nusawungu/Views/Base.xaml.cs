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
using pwskia_nusawungu.Views;

namespace pwskia_nusawungu.Views
{
    /// <summary>
    /// Interaction logic for Base.xaml
    /// </summary>
    public partial class Base : Window
    {
        public string adminName { get; set; }
        public Base()
        {
            InitializeComponent();
            Main.Content = new Dashboard.Dashboard();
            Title = "Dashboard";
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Dashboard.Dashboard();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            popUpConfirmLogout.IsOpen = true;
        }

        private void btnPWSKIA_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PWS_KIA.DataPWSKIAView(adminName);
            Title = "PWS KIA - Data Record";
        }

        private void btnKeluar_Click(object sender, RoutedEventArgs e)
        {

            MainWindow login = new MainWindow();
            this.Close();
            login.Show();
        }

        private void btnbatal_Click(object sender, RoutedEventArgs e)
        {

            if(popUpConfirmLogout.IsOpen == true) popUpConfirmLogout.IsOpen = false;
        }
    }
}
