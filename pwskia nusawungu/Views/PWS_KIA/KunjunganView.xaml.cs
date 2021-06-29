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

namespace pwskia_nusawungu.Views.PWS_KIA
{
    /// <summary>
    /// Interaction logic for Kunjungan1.xaml
    /// </summary>
    public partial class KunjunganView : Page
    {
        public string penanggungJawab { get; set; }
        public KunjunganView(string adminName)
        {
            InitializeComponent();

            GetDesa();
            GetMonthsAndYears();
            penanggungJawab = adminName;
        }

        private void txtNumberFormat_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        public void GetMonthsAndYears()
        {
            int currentYear = int.Parse(DateTime.Now.ToString("yyyy")) - 5;
            for(int i=1; i<=12; i++)
            {
                comBoxBulan.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i));
                comBoxTahun.Items.Add(currentYear.ToString());
                currentYear++;
            }
        }

        public void GetDesa()
        {

            foreach (EnumDesa desa in Enum.GetValues(typeof(EnumDesa)))
            {
                comBoxDesa.Items.Add(desa.ToString());
            }
        }


        public void GetDataKunjungan(int kunjunganKe)
        {
            KunjunganViewModel kunjunganContext = new KunjunganViewModel();
            foreach (Kunjungan kunjungan in kunjunganContext.getAllDataKunjungan(kunjunganKe)){
                switch (kunjunganKe)
                {
                    case 1:
                        dgKunjungan1.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "Kunjungan 1";
                        break;
                    case 4:
                        dgKunjungan1.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "Kunjungan 4";
                        break;
                    case 6:
                        dgKunjungan1.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "Kunjungan 6";
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnTriggerLihatData_Click(object sender, RoutedEventArgs e)
        {
            txtLabelAksi.Text = "LIHAT DATA";

            comBoxBulan.Visibility = Visibility.Visible;
            comBoxTahun.Visibility = Visibility.Visible;
            comBoxKunjunganKe.Visibility = Visibility.Visible;
            btnLihatData.Visibility = Visibility.Visible;

            txtJmlR.Visibility = Visibility.Hidden;
            txtJumlahBulanIni.Visibility = Visibility.Hidden;

            btnSimpanData.Visibility = Visibility.Hidden;
            btnSimpanUbahData.Visibility = Visibility.Hidden;
        }

        

        private void btnTriggerUbahData_Click(object sender, RoutedEventArgs e)
        {
            txtLabelAksi.Text = "UBAH DATA";

            comBoxKunjunganKe.Visibility = Visibility.Visible;
            txtJumlahBulanIni.Visibility = Visibility.Visible;
            comBoxBulan.Visibility = Visibility.Hidden;
            comBoxTahun.Visibility = Visibility.Hidden;
            txtJmlR.Visibility = Visibility.Visible;

            btnSimpanUbahData.Visibility = Visibility.Visible;

            btnSimpanData.Visibility = Visibility.Hidden;
            btnLihatData.Visibility = Visibility.Hidden;
        }

        

        private void btnTriggerInputData_Click(object sender, RoutedEventArgs e)
        {
            txtLabelAksi.Text = "INPUT DATA";

            comBoxKunjunganKe.Visibility = Visibility.Visible;
            txtJumlahBulanIni.Visibility = Visibility.Visible;
            txtJmlR.Visibility = Visibility.Visible;

            btnSimpanData.Visibility = Visibility.Visible;


            btnLihatData.Visibility = Visibility.Hidden;
            btnSimpanUbahData.Visibility = Visibility.Hidden;

            comBoxBulan.Visibility = Visibility.Hidden;
            comBoxTahun.Visibility = Visibility.Hidden;
        }

        private void btnSimpanData_Click(object sender, RoutedEventArgs e)
        {
            KunjunganViewModel kunjunganContext = new KunjunganViewModel();
            Kunjungan kunjungan = new Kunjungan();
            DesaContext desaContext = new DesaContext();

            //kunjungan.tanggal = DateTime.Now.ToString("dd MMMM yyyy");
            kunjungan.tanggal = "01 January 2017";
            kunjungan.kunjunganKe = int.Parse(comBoxKunjunganKe.Text);

            foreach (Desa desa in desaContext.GetSasaranPerDesa(comBoxDesa.Text))
            {

                kunjungan.desa = new Desa
                {
                    nama = comBoxDesa.Text.ToString(),
                    jmlBulanLalu = 0,
                    jmlBulanIni = int.Parse(txtJumlahBulanIni.Text),
                    sasaran = new Sasaran
                    {
                        bumil = desa.sasaran.bumil,
                        bulin = desa.sasaran.bulin,
                        bumilRisti = desa.sasaran.bumilRisti
                    },
                    r = int.Parse(txtJmlR.Text),
                };
            }

            kunjungan.penanggungJawab = penanggungJawab;

            try
            {
                string message = kunjunganContext.AddDataKunjunganSatu(kunjungan);
                MessageBox.Show(message, "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnLihatData_Click(object sender, RoutedEventArgs e)
        {
            dgKunjungan1.Items.Clear();
            int kunjunganke;
            if (comBoxKunjunganKe.Text != "")
            {
                try
                {
                    kunjunganke = int.Parse(comBoxKunjunganKe.Text);
                    GetDataKunjungan(kunjunganke);
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Pilih kunjungan ke-", "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
