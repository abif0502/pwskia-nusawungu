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
using System.Windows.Shapes;

using pwskia_nusawungu.Models;
using pwskia_nusawungu.ViewModels.PWSKIA;

namespace pwskia_nusawungu.Views.PWS_KIA
{
    /// <summary>
    /// Interaction logic for FormInputData.xaml
    /// </summary>
    public partial class FormInputData : Window
    {

        public string penanggungJawab { get; set; }
        public FormInputData(string penanggungJawab)
        {
            InitializeComponent();

            //penggil method get desa untuk combobox desa
            GetDesa();

            //Set penanggung jawab dari adminName
            this.penanggungJawab = penanggungJawab;
        }

        private int GetValueFromRadioButton()
        {
            int value = 0;
            if(rbKunjungan1.IsChecked == true)
            {
                value = 1;
            }else if(rbKunjungan4.IsChecked == true)
            {
                value = 4;

            }else if(rbKunjungan6.IsChecked == true)
            {
                value = 6;
            }

            return value;
        }

        private void txtNumberFormat_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


        ///////////////////////// Logic function non action ////////////////////////////

        // Set desa di combobox
        public void GetDesa()
        {

            foreach (EnumDesa desa in Enum.GetValues(typeof(EnumDesa)))
            {
                comBoxDesa.Items.Add(desa.ToString());
            }
        }

        public int GetJumlahBulanLalu(int kunjunganKe, string namaDesa)
        {
            int jumlahBulanLalu=0;
            //int bulanLalu = int.Parse(DateTime.Now.ToString("MM")) - 1;

            // Hanya bulan sementara di tahun 2017
            string namaBulanLalu = DateTimeFormatInfo.CurrentInfo.GetMonthName(1);

            KunjunganViewModel kunjunganContext = new KunjunganViewModel();
            
            jumlahBulanLalu = kunjunganContext.GetJumlahBulanLalu(kunjunganKe, namaDesa, namaBulanLalu);
            return jumlahBulanLalu;
        }

        ////////////////////////////////// Function button //////////////////////////////


        // button action for simpan data

        private void btnSimpanData_Click(object sender, RoutedEventArgs e)
        {
            KunjunganViewModel kunjunganContext = new KunjunganViewModel();
            Kunjungan kunjungan = new Kunjungan();
            DesaContext desaContext = new DesaContext();

            if(string.IsNullOrEmpty(txtJumlahBulanIni.Text) || GetValueFromRadioButton() == 0 || string.IsNullOrEmpty(comBoxDesa.Text))
            {

            }
            else
            {
                int kunjunganKe = GetValueFromRadioButton();
                string namaDesa = comBoxDesa.Text;

                int jumlahBulanLalu = GetJumlahBulanLalu(kunjunganKe, namaDesa);
                int jumlahBulanIni = int.Parse(txtJumlahBulanIni.Text);
                int jumlahR = 0;

                //kunjungan.tanggal = DateTime.Now.ToString("dd MMMM yyyy");
                // Tanggal sementara
                kunjungan.tanggal = "01 February 2017";
                kunjungan.kunjunganKe = kunjunganKe;

                foreach (Desa desa in desaContext.GetSasaranPerDesa(namaDesa))
                {

                    kunjungan.desa = new Desa
                    {
                        nama = comBoxDesa.Text,
                        jmlBulanLalu = jumlahBulanLalu,
                        jmlBulanIni = jumlahBulanIni,
                        sasaran = new Sasaran
                        {
                            bumil = desa.sasaran.bumil,
                            bulin = desa.sasaran.bulin,
                            bumilRisti = desa.sasaran.bumilRisti
                        },
                        r = jumlahR,
                    };
                }

                kunjungan.penanggungJawab = penanggungJawab;

                try
                {
                    string message = kunjunganContext.AddDataKunjungan(kunjungan);
                    MessageBox.Show(message, "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        // Fungsi tutup form input data
        private void btnTutup_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
