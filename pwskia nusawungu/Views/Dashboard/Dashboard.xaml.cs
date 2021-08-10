using System;
using System.Globalization;
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

using pwskia_nusawungu.ViewModels.PWSKIA;

namespace pwskia_nusawungu.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();
            GetDesa();
            GetMonthsAndYear();
            //GetNilaiSasaran();

            Tanggal tanggal = new Tanggal();
            string[] detailTanggal = tanggal.TanggalSekarang().Split(' ');
            txtTTahun.Text = detailTanggal[2];

        }

        public void GetNilaiSasaran(string tahun)
        {
            DesaContext desaContext = new DesaContext();

            dgSasaran.Items.Clear();
            try
            {
                txtJudulTabelSasaran.Text = $"Data Sasaran \"{tahun}\"";
                foreach (Desa desa in desaContext.GetSasaranPerTahun(tahun))
                {
                    dgSasaran.Items.Add(desa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetMonthsAndYear()
        {
            Tanggal tanggal = new Tanggal();

            DesaContext desaContext = new DesaContext();
            foreach (string tahun in desaContext.GetTahun())
            {
                comBoxTahun.Items.Add(tahun);
            }
        }

        public void GetDesa()
        {
            foreach (EnumDesa desa in Enum.GetValues(typeof(EnumDesa)))
            {
                comBoxDesaSasaran.Items.Add(desa.ToString());
                comBoxUDesaSasaran.Items.Add(desa.ToString());
            }
        }

        public string windowTitle()
        {
            return "Dashboard";
        }

        private void txtNumberFormat_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void btnSimpanSasaran_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(comBoxDesaSasaran.Text) || string.IsNullOrEmpty(txtBumil.Text) || string.IsNullOrEmpty(txtBulin.Text) || string.IsNullOrEmpty(txtBumilRisti.Text) || string.IsNullOrEmpty(txtTTahun.Text))
            {
                MessageBox.Show("Data tidak boleh kosong", "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DesaContext context = new DesaContext();
                if (!context.GetSasaranPerTahun(txtTTahun.Text, comBoxDesaSasaran.Text).Any())
                {
                    Desa desa = new Desa();
                    desa.id = context.GetIdDesa(comBoxDesaSasaran.Text);
                    desa.sasaran = new Sasaran()
                    {
                        tahun = txtTTahun.Text,
                        bumil = int.Parse(txtBumil.Text),
                        bulin = int.Parse(txtBulin.Text),
                        bumilRisti = int.Parse(txtBumilRisti.Text)
                    };

                    try
                    {
                        string message = context.TambahSasaran(desa);
                        if (message != null)
                        {
                            var yes = MessageBox.Show(message, "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
                            dgSasaran.Items.Clear();
                            GetNilaiSasaran(txtTTahun.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Gagal!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    txtBulin.Clear();
                    txtBumil.Clear();
                    txtBumilRisti.Clear();
                }
                else
                {
                    MessageBox.Show($"Anda sudah menambahkan data sasaran desa \" {comBoxDesaSasaran.Text} \", tahun \" {txtTTahun.Text}\"", "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }

        }

        private void btnTriggerInputSasaran_Click(object sender, RoutedEventArgs e)
        {
            popUpTambahSasaran.IsOpen = true;
        }

        private void comBoxTahun_DropDownClosed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comBoxTahun.Text))
            {
                GetNilaiSasaran(comBoxTahun.Text);
            }
            
        }

        private void btnUbahSasaran_Click(object sender, RoutedEventArgs e)
        {
            popUpUbahSasaran.IsOpen = true;
            Desa dataDesa = dgSasaran.SelectedItem as Desa;

            txtUTahun.Text = dataDesa.sasaran.tahun;
            comBoxUDesaSasaran.Text = dataDesa.nama;
            txtUBumil.Text = dataDesa.sasaran.bumil.ToString();
            txtUBulin.Text = dataDesa.sasaran.bulin.ToString();
            txtUBumilRisti.Text = dataDesa.sasaran.bumilRisti.ToString();

        }

        private void btnTriggerHapus_Click(object sender, RoutedEventArgs e)
        {
            popUpConfirmHapus.IsOpen = true;
        }

        private void btnSimpanUSasaran_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(comBoxUDesaSasaran.Text) || string.IsNullOrEmpty(txtUBumil.Text) || string.IsNullOrEmpty(txtUBulin.Text) || string.IsNullOrEmpty(txtUBumilRisti.Text) || string.IsNullOrEmpty(txtUTahun.Text))
            {
                MessageBox.Show("Data tidak boleh kosong", "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DesaContext context = new DesaContext();
                Desa desa = new Desa();
                desa.id = context.GetIdDesa(comBoxUDesaSasaran.Text);
                desa.sasaran = new Sasaran()
                {
                    tahun = txtUTahun.Text,
                    bumil = int.Parse(txtUBumil.Text),
                    bulin = int.Parse(txtUBulin.Text),
                    bumilRisti = int.Parse(txtUBumilRisti.Text)
                };

                try
                {
                    string message = context.EditSasaran(desa);
                    if (message != null)
                    {
                        var yes = MessageBox.Show(message, "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
                        dgSasaran.Items.Clear();
                        GetNilaiSasaran(txtUTahun.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Gagal!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                txtUBulin.Clear();
                txtUBumil.Clear();
                txtUBumilRisti.Clear();
            }
        }

        private void btnHapusData_Click(object sender, RoutedEventArgs e)
        {
            Desa desa = dgSasaran.SelectedItem as Desa;
            DesaContext context = new DesaContext();
            try
            {
                context.DeleteSasaran(desa.sasaran.tahun, context.GetIdDesa(desa.nama));
                MessageBox.Show("Berhasil menghapus data", "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
                dgSasaran.Items.Clear();
                GetNilaiSasaran(desa.sasaran.tahun);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Gagal!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnbatalHapus_Click(object sender, RoutedEventArgs e)
        {
            if(popUpConfirmHapus.IsOpen == true)
            {
                popUpConfirmHapus.IsOpen = false;
            }
        }
    }
}
