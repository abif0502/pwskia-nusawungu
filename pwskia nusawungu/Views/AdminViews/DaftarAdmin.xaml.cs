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

using pwskia_nusawungu.Models;
using pwskia_nusawungu.ViewModels;

namespace pwskia_nusawungu.Views.AdminView
{
    /// <summary>
    /// Interaction logic for DaftarAdmin.xaml
    /// </summary>
    public partial class DaftarAdmin : Page
    {
        public DaftarAdmin()
        {
            InitializeComponent();
            GetAllAdmin();
        }

        public void GetAllAdmin()
        {
            dgDaftarAdmin.Items.Clear();
            AdminViewModel adminContext = new AdminViewModel();
            foreach(Admin admin in adminContext.GetAllAdmin())
            {
                dgDaftarAdmin.Items.Add(admin);
            }
        }

        private void btnUbahData_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = dgDaftarAdmin.SelectedItem as Admin;
            coBoxUbahSuper.Text = admin.super;
            txtUsernameUpdate.Text = admin.username;
            popUbahData.IsOpen = true;
        }

        private void btnTriggerHapus_Click(object sender, RoutedEventArgs e)
        {
            popUpConfirmDelete.IsOpen = true;
        }

        private void btnTambahDataAdmin_Click(object sender, RoutedEventArgs e)
        {
            popTambahAdmin.IsOpen = true;
        }

        private void btnSimpanAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminViewModel adminContext = new AdminViewModel();
            Admin admin = new Admin();

            if(string.IsNullOrEmpty(txtNama.Text) || string.IsNullOrEmpty(txtNIP.Text)|| string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword1.Password) || string.IsNullOrEmpty(txtPassword2.Password))
            {
                MessageBox.Show("Harap isi data dengan lengkap", "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string password1 = txtPassword1.Password;
                string password2 = txtPassword2.Password;


                if (password1 == password2)
                {

                    admin.nama = txtNama.Text;
                    admin.nip = txtNIP.Text;
                    admin.username = txtUsername.Text;
                    admin.passw = txtPassword2.Password;
                    admin.super = "0";

                    try
                    {
                        if(adminContext.GetAllAdmin(admin.username, admin.nip).Any())
                        {
                            MessageBox.Show($"Username '{admin.username}' atau NIP '{admin.nip}' sudah terdaftar", "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            adminContext.registration(admin);
                            MessageBox.Show("Berhasil mendaftarkan admin baru", "Info!", MessageBoxButton.OK, MessageBoxImage.Information);

                            GetAllAdmin();
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Konfirmasi password harus sama", "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btnHapusData_Click(object sender, RoutedEventArgs e)
        {
            Admin selectedAdmin = dgDaftarAdmin.SelectedItem as Admin;
            AdminViewModel adminContext = new AdminViewModel();

            try
            {
                adminContext.DeleteAdmin(selectedAdmin.username);
                MessageBox.Show("Berhasil menghapus admin", "Info!", MessageBoxButton.OK, MessageBoxImage.Information);

                GetAllAdmin();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnbatalHapus_Click(object sender, RoutedEventArgs e)
        {
            if (popUpConfirmDelete.IsOpen == true)
            {
                popUpConfirmDelete.IsOpen = false;
            }
        }

        private void btnSimpanUbahData_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = dgDaftarAdmin.SelectedItem as Admin;
            admin.username = txtUsernameUpdate.Text;
            admin.super = coBoxUbahSuper.Text;

            AdminViewModel adminContext = new AdminViewModel();

            try
            {
                adminContext.UpdateSuperAdmin(admin);
                MessageBox.Show("Berhasil mengubah admin", "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
                GetAllAdmin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
