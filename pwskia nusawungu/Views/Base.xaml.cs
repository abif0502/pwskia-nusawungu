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
using pwskia_nusawungu.Models;

using pwskia_nusawungu.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace pwskia_nusawungu.Views
{
    /// <summary>
    /// Interaction logic for Base.xaml
    /// </summary>
    public partial class Base : Window
    {
        public Admin dataAdmin { get; set; }
        public Base(Admin admin)
        {
            InitializeComponent();
            Main.Content = new Dashboard.Dashboard();
            Title = "Dashboard";
            this.dataAdmin = admin;
            btnProfile.Content = dataAdmin.nama;

            if(dataAdmin.super == "1")
            {
                btnDaftarAdmin.Visibility = Visibility.Visible;
            }

        }


        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Dashboard.Dashboard();
            Title = "PWSKIA - Dashboard";
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            popUpConfirmLogout.IsOpen = true;
        }

        private void btnPWSKIA_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PWS_KIA.DataPWSKIAView(dataAdmin);
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

        private void btnGrafik_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Grafik.GrafikView(dataAdmin);
            Title = "PWS KIA - Grafik";
        }

        private void btnDaftarAdmin_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AdminView.DaftarAdmin();
            Title = "Daftar Admin";
        }

        private void btnTriggerUbahPassword_Click(object sender, RoutedEventArgs e)
        {
            popUpUbahPassword.IsOpen = true;
        }

        private void btnUbahPassword_Click(object sender, RoutedEventArgs e)
        {
            AdminViewModel adminContext = new AdminViewModel();
            try
            {
                if (string.IsNullOrEmpty(profilPasswordBaru.Password) || string.IsNullOrEmpty(profilPasswordBaru.Password) || string.IsNullOrEmpty(profilKonfirmasiPassword.Password))
                {
                    MessageBox.Show("Harap isi data dengan lengkap", "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if(profilPasswordBaru.Password == profilKonfirmasiPassword.Password)
                    {
                        dataAdmin.passw = profilKonfirmasiPassword.Password;

                        ValidationContext context = new ValidationContext(dataAdmin);
                        var errors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                        string messageValidator = "";

                        if (!Validator.TryValidateObject(dataAdmin, context, errors, true))
                        {
                            foreach (System.ComponentModel.DataAnnotations.ValidationResult res in errors)
                            {
                                messageValidator += $"{res.ErrorMessage} \n";
                            }

                            MessageBox.Show(messageValidator, "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            if (adminContext.UbahPasswordAdmin(dataAdmin, profilPasswordLama.Password) == true)
                            {
                                MessageBox.Show("Berhasil Mengubah Sandi, Harap keluar dan login kembali", "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
                                MainWindow login = new MainWindow();
                                this.Close();
                                login.Show();
                            }
                            else
                            {
                                MessageBox.Show("Sandi Anda Salah", "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Konfirmasi password tidak sama", "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

                profilPasswordLama.Clear();
                profilPasswordBaru.Clear();
                profilKonfirmasiPassword.Clear();
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
