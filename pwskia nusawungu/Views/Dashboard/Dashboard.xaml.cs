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
            GetNilaiSasaran();
        }

        public void GetNilaiSasaran()
        {
            dgSasaran.Items.Clear();
            DesaContext context = new DesaContext();
            foreach(Desa desa in context.GetSasaran())
            {
                dgSasaran.Items.Add(desa);
            }
        }

        private void GetMonthsAndYear()
        {
            Tanggal tanggal = new Tanggal();

            foreach (string bulan in tanggal.GetDaftarBulan())
            {
                comBoxBulan.Items.Add(bulan);
            }

            PwskiaViewModel kunjunganContext = new PwskiaViewModel();
            foreach (string tahun in kunjunganContext.GetDaftarTahun())
            {
                comBoxTahun.Items.Add(tahun);
            }
        }

        public void GetDesa()
        {
            foreach (EnumDesa desa in Enum.GetValues(typeof(EnumDesa)))
            {
                comBoxDesaSasaran.Items.Add(desa.ToString());
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
            if(string.IsNullOrEmpty(comBoxDesaSasaran.Text) || string.IsNullOrEmpty(txtBumil.Text) || string.IsNullOrEmpty(txtBulin.Text) || string.IsNullOrEmpty(txtBumilRisti.Text))
            {
                MessageBox.Show("Data tidak boleh kosong", "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DesaContext context = new DesaContext();
                Desa desa = new Desa();
                desa.nama = comBoxDesaSasaran.Text;
                desa.sasaran = new Sasaran()
                {
                    bumil = int.Parse(txtBumil.Text),
                    bulin = int.Parse(txtBulin.Text),
                    bumilRisti = int.Parse(txtBumilRisti.Text)
                };

                try
                {
                    string message = context.EditSasaran(desa);
                    if (message != null)
                    {
                        var yes = MessageBox.Show(message, "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
                        dgSasaran.Items.Clear();
                        GetNilaiSasaran();
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

        }

        private void btnTriggerInputSasaran_Click(object sender, RoutedEventArgs e)
        {
            popUpFromSasaran.IsOpen = true;
        }

        private void comBoxBulan_DropDownClosed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comBoxBulan.Text))
            {
                comBoxTahun.IsEnabled = true;
            }
        }

        private void comBoxTahun_DropDownClosed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comBoxTahun.Text))
            {
                DesaContext desaContext = new DesaContext();
                string bulanDanTahun = $"{comBoxBulan.Text} {comBoxTahun.Text}";

                dgSasaranPerbulan.Items.Clear();
                try
                {
                    titleTableBulanDanTahun.Text = $"Data Sasaran \"{bulanDanTahun}\"";
                    foreach (Desa desa in desaContext.GetSasaranPerBulan(bulanDanTahun))
                    {
                        dgSasaranPerbulan.Items.Add(desa);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
