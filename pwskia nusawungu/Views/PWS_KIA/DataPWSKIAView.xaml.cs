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
    public partial class DataPWSKIAView : Page
    {
        public string penanggungJawab { get; set; }
        public DataPWSKIAView(string adminName)
        {
            InitializeComponent();
            penanggungJawab = adminName;
            GetMonthsAndYear();
        }

        //private void txtNumberFormat_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
        //    {
        //        e.Handled = true;
        //    }
        //}

        private int GetIDJenis(string jenis)
        {
            switch(jenis)
            {
                case "Kunjungan 1":
                    return 1;
                case "Kunjungan 4":
                    return 2;
                case "Kunjungan 6":
                    return 3;
                case "KF":
                    return 4;
                case "Persalinan Nakes":
                    return 5;
                case "Deteksi Risiko Nakes":
                    return 6;
                case "Deteksi Risiko Masyarakat":
                    return 7;
                default:
                    return 0;
            }
        }

        private void GetMonthsAndYear()
        {
            for (int i = 1; i <= 12; i++)
            {
                comBoxBulan.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i));
            }

            PwskiaViewModel kunjunganContext = new PwskiaViewModel();
            foreach(string tahun in kunjunganContext.GetDaftarTahun())
            {
                comBoxTahun.Items.Add(tahun);
            }
        }


        // Fungsi untuk mengambil data kunjungan berdasarkan parameter yang diperlukan
        // Jika parameter bulan tidak diisi, tampilkan semua data
        // 
        public void GetDataKunjungan(int idJenis, string bulanDanTahun = "")
        {

            PwskiaViewModel kunjunganContext = new PwskiaViewModel();

            List<Pwskia> dataKunjungan = new List<Pwskia>();

            if (bulanDanTahun == "")
            {
                // GET semua data kunjungan
                dataKunjungan = kunjunganContext.GetAllDataPwskia(idJenis);
            }
            else
            {
                // GET data kunjungan per bulan dan tahun
                dataKunjungan = kunjunganContext.GetAllDataPwskia(idJenis, bulanDanTahun);
            }

            foreach (Pwskia kunjungan in dataKunjungan)
            {
                switch (idJenis)
                {
                    case 1:
                        dgKunjungan.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "Kunjungan 1";
                        break;
                    case 2:
                        dgKunjungan.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "Kunjungan 4";
                        break;
                    case 3:
                        dgKunjungan.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "Kunjungan 6";
                        break;
                    case 4:
                        dgKunjungan.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "KF";
                        break;
                    case 5:
                        dgKunjungan.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "Persalinan Nakes";
                        break;
                    case 6:
                        dgKunjungan.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "Deteksi Risiko Nakes";
                        break;
                    case 7:
                        dgKunjungan.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "Deteksi Risiko Masyarakat";
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnTriggerInputData_Click(object sender, RoutedEventArgs e)
        {
            FormInputData formInput = new FormInputData(penanggungJawab);
            formInput.ShowDialog();
        }

        private void btnTriggerLihatData_Click(object sender, RoutedEventArgs e)
        {
            if(popUpLihatData.IsOpen == true)
            {
                popUpLihatData.IsOpen = false;
            }
            else
            {
                popUpLihatData.IsOpen = true;
            }
        }

        

        private void btnTriggerUbahData_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnKunjungan1_Click(object sender, RoutedEventArgs e)
        {
            dgKunjungan.Items.Clear();
            try
            {
                GetDataKunjungan(1);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnKunjungan4_Click(object sender, RoutedEventArgs e)
        {
            dgKunjungan.Items.Clear();
            try
            {
                GetDataKunjungan(2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnKunjungan6_Click(object sender, RoutedEventArgs e)
        {
            dgKunjungan.Items.Clear();
            try
            {
                GetDataKunjungan(3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void comBoxJenis_DropDownClosed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comBoxJenis.Text) == false)
            {
                comBoxBulan.IsEnabled = true;
            }
        }

        private void comBoxBulan_DropDownClosed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comBoxBulan.Text) == false)
            {
                comBoxTahun.IsEnabled = true;
            }
        }

        // ACTION GET data kunjungan per bulan dan tahun
        private void comBoxTahun_DropDownClosed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comBoxTahun.Text) == false)
            {
                // Get data per bulan, tahun
                int kunjunganKe = 0;
                if (string.IsNullOrEmpty(comBoxJenis.Text) == false)
                {
                    kunjunganKe = GetIDJenis(comBoxJenis.Text);

                    string bulanDanTahun = $"{comBoxBulan.Text} {comBoxTahun.Text}";

                    try
                    {
                        dgKunjungan.Items.Clear();
                        GetDataKunjungan(kunjunganKe, bulanDanTahun);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnCariData_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void txtKeywordCariData_KeyUp(object sender, KeyEventArgs e)
        {
            PwskiaViewModel kunjunganContext = new PwskiaViewModel();
            titleTableKunjungan.Text = "Data PWSKIA";
            

            dgKunjungan.Items.Clear();

            if (string.IsNullOrEmpty(txtKeywordCariData.Text) != true)
            {
                foreach (Pwskia kunjungan in kunjunganContext.CariData(txtKeywordCariData.Text))
                {
                    dgKunjungan.Items.Add(kunjungan);
                }

            }
        }

        private void btnKF_Click(object sender, RoutedEventArgs e)
        {
            dgKunjungan.Items.Clear();

            try
            {
                GetDataKunjungan(4);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPersalNakes_Click(object sender, RoutedEventArgs e)
        {
            dgKunjungan.Items.Clear();

            try
            {
                GetDataKunjungan(5);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeteksiRisNakes_Click(object sender, RoutedEventArgs e)
        {
            dgKunjungan.Items.Clear();

            try
            {
                GetDataKunjungan(6);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeteksiRisMasy_Click(object sender, RoutedEventArgs e)
        {
            dgKunjungan.Items.Clear();

            try
            {
                GetDataKunjungan(7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
