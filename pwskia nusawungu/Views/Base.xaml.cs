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
            var result = MessageBox.Show("Yakin ingin keluar?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                MainWindow login = new MainWindow();
                this.Close();
                login.Show();
            }
        }

        private void btnKunjungan1_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PWS_KIA.Kunjungan1View(adminName);
            Title = "PWS KIA - Kunjungan 1";
        }
    }
}
