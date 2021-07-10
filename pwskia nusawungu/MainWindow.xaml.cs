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
using pwskia_nusawungu.Models;

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

        // Function no action button


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
                    List<Admin> dataAdmin = adminVM.loginAdmin(txtUsername.Text, txtPassword.Password);
                    if (dataAdmin.Any())
                    {
                        foreach(Admin admin in dataAdmin)
                        {
                            Base basePage = new Base(admin);
                            this.Close();
                            basePage.Show();
                        }
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
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            popUpConfirmClose.IsOpen = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnbatal_Click(object sender, RoutedEventArgs e)
        {
            if (popUpConfirmClose.IsOpen == true) popUpConfirmClose.IsOpen = false;
        }

        private void btnKeluar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
