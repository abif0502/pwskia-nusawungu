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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NETCore.Encrypt;
using pwskia_nusawungu.ViewModels;
using pwskia_nusawungu.Views;

namespace pwskia_nusawungu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //Get admins


        // Login button action
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if(String.IsNullOrEmpty(txtUsername.Text) == true || String.IsNullOrEmpty(txtPassword.Password) == true)
            {
                MessageBox.Show("Username atau password tidak boleh kosong", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    AdminViewModel adminVM = new AdminViewModel();
                    string name = adminVM.loginAdmin(txtUsername.Text, txtPassword.Password);
                    if (name != null)
                    {
                        Base basePage = new Base();
                        basePage.btnProfile.Content = name;
                        basePage.adminName = name;
                        this.Close();
                        basePage.Show();
                    }
                    else
                    {
                        MessageBox.Show("Username atau Password salah", "Gagal masuk", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Gagal masuk", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Cancel button action for clearing textbox
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Apakah yakin ingin keluar?", "Konfirmasi!", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if(confirm == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
