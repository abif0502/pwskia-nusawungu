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

        private void btnPersalinanNakes_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PersalinanNakes.PersalinanNakes();
            Title = "Persalinan Nakes";
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            popUpConfirmLogout.IsOpen = true;
        }

        private void btnKunjungan_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PWS_KIA.KunjunganView(adminName);
            Title = "PWS KIA - Kunjungan 1";
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void btnKeluar_Click(object sender, RoutedEventArgs e)
        {
            //var result = MessageBox.Show("Yakin ingin keluar?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (result == MessageBoxResult.Yes)
            //{
            //    MainWindow login = new MainWindow();
            //    this.Close();
            //    login.Show();
            //}

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
