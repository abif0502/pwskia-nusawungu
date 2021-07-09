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
                value = 2;

            }else if(rbKunjungan6.IsChecked == true)
            {
                value = 3;
            }
            else if (rbKF.IsChecked == true)
            {
                value = 4;

            }
            else if (rbPersalNakes.IsChecked == true)
            {
                value = 5;
            }
            else if (rbDeteksiRisNakes.IsChecked == true)
            {
                value = 6;

            }
            else if (rbDeteksiRisMasy.IsChecked == true)
            {
                value = 7;
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

        public int GetJumlahBulanLalu(int idJenis, string namaDesa)
        {
            Tanggal tanggal = new Tanggal();
            int jmlBulanLalu = 0;

            //int bulanLalu = int.Parse(DateTime.Now.ToString("MM")) - 1;
            // Hanya bulan sementara di tahun 2017
            int bulanLalu = 1; //  bulan lalu = Januari
            PwskiaViewModel kunjunganContext = new PwskiaViewModel();

            string namaBulanLalu;
            if (bulanLalu > 0)
            {
                namaBulanLalu = tanggal.GetDaftarBulan()[bulanLalu - 1];
                jmlBulanLalu = kunjunganContext.GetJumlahBulanLalu(idJenis, namaDesa, namaBulanLalu);

            }
            else if (bulanLalu == 0)
            {
                namaBulanLalu = tanggal.GetDaftarBulan()[11];
                jmlBulanLalu = kunjunganContext.GetJumlahBulanLalu(idJenis, namaDesa, namaBulanLalu);
            }

            return jmlBulanLalu;
        }

        ////////////////////////////////// Function button //////////////////////////////


        // button action for simpan data

        private void btnSimpanData_Click(object sender, RoutedEventArgs e)
        {
            PwskiaViewModel kunjunganContext = new PwskiaViewModel();
            DesaContext desaContext = new DesaContext();



            if(string.IsNullOrEmpty(txtJumlahBulanIni.Text) || GetValueFromRadioButton() == 0 || string.IsNullOrEmpty(comBoxDesa.Text))
            {
                MessageBox.Show("Data harus diisi", "Peringatan!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                int idJenis = GetValueFromRadioButton();
                string namaDesa = comBoxDesa.Text;
                int jumlahBulanLalu = GetJumlahBulanLalu(idJenis, namaDesa);
                int jumlahBulanIni = int.Parse(txtJumlahBulanIni.Text);

                //kunjungan.tanggal = DateTime.Now.ToString("dd MMMM yyyy");

                // Tanggal sementara 
                string tanggalSekarang = "Rabu, 1 Februari 2017";

                // Cek jika ada data duplikat
                PwskiaViewModel pwskiaContext = new PwskiaViewModel();
                string[] tanggalSplit = tanggalSekarang.Split(' ');
                string bulanDanTahun = tanggalSplit[2] + " " + tanggalSplit[3];

                if (!pwskiaContext.GetSingleDataPwsKia(idJenis, bulanDanTahun, namaDesa).Any())
                {
                    //Simpan data jika tidak ada data duplikat

                    Pwskia pwskia = new Pwskia();

                    pwskia.tanggal = tanggalSekarang;
                    pwskia.idJenis = idJenis;
                    foreach (Desa desa in desaContext.GetSasaranPerBulan(desa: namaDesa))
                    {
                        pwskia.desa = new Desa
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
                        };
                    }

                    pwskia.penanggungJawab = penanggungJawab;

                    try
                    {
                        string message = kunjunganContext.AddDataPwskia(pwskia);
                        MessageBox.Show(message, "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // Jika ada data akan mengirim informasi, dan tidak akan menyimpan
                    foreach (Pwskia duplikat in pwskiaContext.GetSingleDataPwsKia(idJenis, bulanDanTahun, namaDesa))
                    {
                        MessageBox.Show($"Data \"{duplikat.jenis}\" untuk \"{duplikat.desa.nama}\" di bulan \"{bulanDanTahun}\" sudah ada. Jika ada kesalahan input silahkan pilih ubah data",
                            "Info!",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
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
