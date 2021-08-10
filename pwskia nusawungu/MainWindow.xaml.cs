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
using System.ComponentModel.DataAnnotations;

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

        private void btnLupaSandi_Click(object sender, RoutedEventArgs e)
        {
            popUpCekAdmin.IsOpen = true;

        }

        private void txtNumberFormat_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void btnCek_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            AdminViewModel context = new AdminViewModel();

            admin.username = txtLupaUsername.Text;
            admin.nik = txtNIK.Text;

            try
            {
                if (context.CariAdmin(admin).Any())
                {
                    popUpGantiSandi.IsOpen = true;
                    popUpCekAdmin.IsOpen = false;
                }
                else
                {
                    MessageBox.Show("Tidak Ditemukan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSimpanSandi_Click(object sender, RoutedEventArgs e)
        {
            AdminViewModel adminContext = new AdminViewModel();
            Admin admin = new Admin();
            if(string.IsNullOrEmpty(txtLupaUsername.Text) || string.IsNullOrEmpty(txtSandiBaru.Password) || string.IsNullOrEmpty(txtKonfirmasiSandi.Password))
            {
                MessageBox.Show("Data Harus Diisi", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                admin.username = txtLupaUsername.Text;
                admin.nik = txtNIK.Text;
                if (txtSandiBaru.Password == txtKonfirmasiSandi.Password)
                {
                    admin.passw = txtKonfirmasiSandi.Password;
                    try
                    {
                        ValidationContext context = new ValidationContext(admin);
                        var errors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                        string messageValidator = "";

                        if (!Validator.TryValidateObject(admin, context, errors, true))
                        {
                            foreach (System.ComponentModel.DataAnnotations.ValidationResult res in errors)
                            {
                                messageValidator += $"{res.ErrorMessage} \n";
                            }

                            MessageBox.Show(messageValidator, "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            adminContext.LupaPassowrd(admin);
                            MessageBox.Show("Berhasil Merubah Kata Sandi", "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Konfimasi password harus sama", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
