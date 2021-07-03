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

        private void GetMonthsAndYear()
        {
            for (int i = 1; i <= 12; i++)
            {
                comBoxBulan.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i));
            }

            KunjunganViewModel kunjunganContext = new KunjunganViewModel();
            foreach(string tahun in kunjunganContext.GetDaftarTahun())
            {
                comBoxTahun.Items.Add(tahun);
            }
        }


        // Fungsi untuk mengambil data kunjungan berdasarkan parameter yang diperlukan
        // Jika parameter bulan tidak diisi, tampilkan semua data
        // 
        public void GetDataKunjungan(int kunjunganKe, string bulanDanTahun = "")
        {
            KunjunganViewModel kunjunganContext = new KunjunganViewModel();

            List<Kunjungan> dataKunjungan = new List<Kunjungan>();

            if(bulanDanTahun == "")
            {
                // GET semua data kunjungan
                dataKunjungan = kunjunganContext.GetAllDataKunjungan(kunjunganKe);
            }
            else
            {
                // GET data kunjungan per bulan dan tahun
                dataKunjungan = kunjunganContext.GetAllDataKunjungan(kunjunganKe, bulanDanTahun);
            }

            foreach (Kunjungan kunjungan in dataKunjungan)
            {
                switch (kunjunganKe)
                {
                    case 1:
                        dgKunjungan.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "Kunjungan 1";
                        break;
                    case 4:
                        dgKunjungan.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "Kunjungan 4";
                        break;
                    case 6:
                        dgKunjungan.Items.Add(kunjungan);
                        titleTableKunjungan.Text = "Kunjungan 6";
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
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnKunjungan4_Click(object sender, RoutedEventArgs e)
        {
            dgKunjungan.Items.Clear();
            try
            {
                GetDataKunjungan(4);
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
                GetDataKunjungan(6);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void comBoxKunjungan_DropDownClosed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comBoxKunjungan.Text) == false)
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
                if (string.IsNullOrEmpty(comBoxKunjungan.Text) == false)
                {
                    kunjunganKe = int.Parse(comBoxKunjungan.Text);

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
            KunjunganViewModel kunjunganContext = new KunjunganViewModel();

            dgKunjungan.Items.Clear();

            if (string.IsNullOrEmpty(txtKeywordCariData.Text) != true)
            {
                foreach (Kunjungan kunjungan in kunjunganContext.CariData(txtKeywordCariData.Text))
                {
                    dgKunjungan.Items.Add(kunjungan);
                }

            }
        }
    }
}
