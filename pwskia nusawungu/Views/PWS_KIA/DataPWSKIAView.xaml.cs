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
using System.Printing;

namespace pwskia_nusawungu.Views.PWS_KIA
{
    /// <summary>
    /// Interaction logic for Kunjungan1.xaml
    /// </summary>
    public partial class DataPWSKIAView : Page
    {
        public Admin dataAdmin { get; set; }
        public int Idrecord { get; set; }
        public DataPWSKIAView(Admin dataAdmin)
        {
            InitializeComponent();
            this.dataAdmin = dataAdmin;
            GetMonthsAndYear();
            GetDesa();
        }

        

        private void txtNumberFormat_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        public void GetDesa()
        {

            foreach (EnumDesa desa in Enum.GetValues(typeof(EnumDesa)))
            {
                comBoxUbahDesa.Items.Add(desa.ToString());
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
                namaBulanLalu = tanggal.GetDaftarBulan()[bulanLalu-1];
                jmlBulanLalu = kunjunganContext.GetJumlahBulanLalu(idJenis, namaDesa, namaBulanLalu);

            }
            else if (bulanLalu == 0)
            {
                namaBulanLalu = tanggal.GetDaftarBulan()[11];
                jmlBulanLalu = kunjunganContext.GetJumlahBulanLalu(idJenis, namaDesa, namaBulanLalu);
            }

            return jmlBulanLalu;
            
        }

        private int GetValueFromRadioButton()
        {
            int value = 0;
            if (rbKunjungan1.IsChecked == true)
            {
                value = 1;
            }
            else if (rbKunjungan4.IsChecked == true)
            {
                value = 2;

            }
            else if (rbKunjungan6.IsChecked == true)
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

        private int GetIDJenis(string jenis)
        {
            switch(jenis)
            {
                case "Kunjungan 1":
                    rbKunjungan1.IsChecked = true;
                    return 1;
                case "Kunjungan 4":
                    rbKunjungan4.IsChecked = true;
                    return 2;
                case "Kunjungan 6":
                    rbKunjungan6.IsChecked = true;
                    return 3;
                case "KF":
                    rbKF.IsChecked = true;
                    return 4;
                case "Persalinan Nakes":
                    rbPersalNakes.IsChecked = true;
                    return 5;
                case "Deteksi Risiko Nakes":
                    rbDeteksiRisNakes.IsChecked = true;
                    return 6;
                case "Deteksi Risiko Masyarakat":
                    rbDeteksiRisMasy.IsChecked = true;
                    return 7;
                default:
                    return 0;
            }
        }

        private void GetMonthsAndYear()
        {
            Tanggal tanggal = new Tanggal();

            foreach(string bulan in tanggal.GetDaftarBulan())
            {
                comBoxBulan.Items.Add(bulan);
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
            dgTabelLaporan.Items.Clear();
            PwskiaViewModel kunjunganContext = new PwskiaViewModel();

            List<Pwskia> dataPwskia = new List<Pwskia>();
            List<Pwskia> dataPwskiaNoRank = new List<Pwskia>();

            if (bulanDanTahun == "")
            {
                // GET semua data kunjungan
                dataPwskia = kunjunganContext.GetAllDataPwskia(idJenis);
            }
            else
            {
                // GET data kunjungan per bulan dan tahun
                int r = 1;
                dataPwskiaNoRank = kunjunganContext.GetAllDataPwskia(idJenis, bulanDanTahun);
                foreach(Pwskia pwskia in dataPwskiaNoRank.OrderBy(o => o.desa.persentase).ToList())
                {
                    dataPwskia.Add(new Pwskia
                    {
                        id = pwskia.id,
                        numRow = pwskia.numRow,
                        jenis = pwskia.jenis,
                        desa = new Desa
                        {
                            nama = pwskia.desa.nama,
                            sasaran = new Sasaran
                            {
                                bumil = pwskia.desa.sasaran.bumil,
                                bulin = pwskia.desa.sasaran.bulin,
                                bumilRisti = pwskia.desa.sasaran.bumilRisti
                            },
                            jmlBulanLalu = pwskia.desa.jmlBulanLalu,
                            jmlBulanIni = pwskia.desa.jmlBulanIni,
                            rank = r
                        },
                        tanggal = pwskia.tanggal,
                        penanggungJawab = pwskia.penanggungJawab
                    }) ;

                    r++;
                }
            }

            dataPwskia = dataPwskia.OrderBy(o => o.numRow).ToList();

            foreach (Pwskia pwskia in dataPwskia)
            {
                switch (idJenis)
                {
                    case 1:
                        dgTabelLaporan.Items.Add(pwskia);
                        dgDataPwskia.Items.Add(pwskia);
                        titleTableKunjungan.Text = "Kunjungan 1";
                        break;
                    case 2:
                        dgTabelLaporan.Items.Add(pwskia);
                        dgDataPwskia.Items.Add(pwskia);
                        titleTableKunjungan.Text = "Kunjungan 4";
                        break;
                    case 3:
                        dgTabelLaporan.Items.Add(pwskia);
                        dgDataPwskia.Items.Add(pwskia);
                        titleTableKunjungan.Text = "Kunjungan 6";
                        break;
                    case 4:
                        dgTabelLaporan.Items.Add(pwskia);
                        dgDataPwskia.Items.Add(pwskia);
                        titleTableKunjungan.Text = "KF";
                        break;
                    case 5:
                        dgTabelLaporan.Items.Add(pwskia);
                        dgDataPwskia.Items.Add(pwskia);
                        titleTableKunjungan.Text = "Persalinan Nakes";
                        break;
                    case 6:
                        dgTabelLaporan.Items.Add(pwskia);
                        dgDataPwskia.Items.Add(pwskia);
                        titleTableKunjungan.Text = "Deteksi Risiko Nakes";
                        break;
                    case 7:
                        dgTabelLaporan.Items.Add(pwskia);
                        dgDataPwskia.Items.Add(pwskia);
                        titleTableKunjungan.Text = "Deteksi Risiko Masyarakat";
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnTriggerInputData_Click(object sender, RoutedEventArgs e)
        {
            FormInputData formInput = new FormInputData(dataAdmin.name);
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
            dgDataPwskia.Items.Clear();
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
            dgDataPwskia.Items.Clear();
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
            dgDataPwskia.Items.Clear();
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
            btnPrintLaporan.IsEnabled = true;
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
                        dgDataPwskia.Items.Clear();
                        GetDataKunjungan(kunjunganKe, bulanDanTahun);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnKF_Click(object sender, RoutedEventArgs e)
        {
            dgDataPwskia.Items.Clear();

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
            dgDataPwskia.Items.Clear();

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
            dgDataPwskia.Items.Clear();

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
            dgDataPwskia.Items.Clear();

            try
            {
                GetDataKunjungan(7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Cari data dengan menekan button cari
        private void btnCariData_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtKeywordCariData.Text))
            {
                PwskiaViewModel kunjunganContext = new PwskiaViewModel();
                titleTableKunjungan.Text = $"Hasil Pencarian Data : \"{txtKeywordCariData.Text}\" ";


                dgDataPwskia.Items.Clear();

                if (string.IsNullOrEmpty(txtKeywordCariData.Text) != true)
                {
                    foreach (Pwskia kunjungan in kunjunganContext.CariData(txtKeywordCariData.Text))
                    {
                        dgDataPwskia.Items.Add(kunjungan);
                    }

                }
            }
        }

        // Cari data dengan tombol enter
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtKeywordCariData.Text))
                {
                    PwskiaViewModel kunjunganContext = new PwskiaViewModel();
                    titleTableKunjungan.Text = $"Hasil Pencarian Data : \"{txtKeywordCariData.Text}\" ";


                    dgDataPwskia.Items.Clear();

                    if (string.IsNullOrEmpty(txtKeywordCariData.Text) != true)
                    {
                        foreach (Pwskia kunjungan in kunjunganContext.CariData(txtKeywordCariData.Text))
                        {
                            dgDataPwskia.Items.Add(kunjungan);
                        }

                    }
                }
            }
        }

        private void btnSimpanPerubahanData_Click(object sender, RoutedEventArgs e)
        {
            DesaContext desaContext = new DesaContext();
            PwskiaViewModel pwskiaContext = new PwskiaViewModel();
            Pwskia pwskia = new Pwskia();

            // Memasukkan value ke objek dari componen;
            int idJenis = GetValueFromRadioButton();
            int jmlBulanLalu = GetJumlahBulanLalu(idJenis, comBoxUbahDesa.Text);


            foreach (Desa desa in desaContext.GetSasaranPerBulan(desa: comBoxUbahDesa.Text))
            {
                pwskia.id = Idrecord;
                pwskia.tanggal = "01 Februari 2017";
                pwskia.idJenis = idJenis;
                pwskia.desa = new Desa
                {
                    nama = comBoxUbahDesa.Text,
                    jmlBulanLalu = jmlBulanLalu,
                    jmlBulanIni = int.Parse(txtUbahJmlBulanIni.Text),

                    sasaran = new Sasaran
                    {
                        bumil = desa.sasaran.bumil,
                        bulin = desa.sasaran.bulin,
                        bumilRisti = desa.sasaran.bumil
                    },


                };
            }

            try
            {
                string message = pwskiaContext.EditDataPWSKIA(pwskia);
                MessageBox.Show(message, "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUbahData_Click(object sender, RoutedEventArgs e)
        {
            Pwskia dataPwskia = dgDataPwskia.SelectedItem as Pwskia;
            // simpan objek untuk ubah data
            

            if (dgDataPwskia.SelectedItem != null)
            {
                // Mengisi nilai pada component
                comBoxUbahDesa.SelectedItem = dataPwskia.desa.nama;
                txtUbahJmlBulanIni.Text = dataPwskia.desa.jmlBulanIni.ToString();
                _ = GetIDJenis(dataPwskia.jenis);
                Idrecord = dataPwskia.id;
                popUpUbahData.IsOpen = true;

            }
        }

        private void btnTriggerHapus_Click(object sender, RoutedEventArgs e)
        {
            popUpConfirmDelete.IsOpen = true;
        }

        private void btnbatalHapus_Click(object sender, RoutedEventArgs e)
        {
            if(popUpConfirmDelete.IsOpen == true)
            {
                popUpConfirmDelete.IsOpen = false;
            }
        }

        private void btnHapusData_Click(object sender, RoutedEventArgs e)
        {
            Pwskia dataPwskia = dgDataPwskia.SelectedItem as Pwskia;

            PwskiaViewModel pwskiaContext = new PwskiaViewModel();

            try
            {
                string message = pwskiaContext.DeleteDataRecord(dataPwskia.id);
                MessageBox.Show(message, "Info!", MessageBoxButton.OK, MessageBoxImage.Information);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnPrintLaporan_Click(object sender, RoutedEventArgs e)
        {
            int jenis = GetIDJenis(comBoxJenis.Text);
            string bulan = comBoxBulan.Text;
            string tahun = comBoxTahun.Text;

            txtPrintBulanDanTahun.Text = bulan + " " + tahun;
            txtPrintJenis.Text = comBoxJenis.Text;
            txtPenanggungJawab.Text = dataAdmin.name;
            txtNIPPenanggungJawab.Text = "NIP. " + dataAdmin.nip;

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
                printDialog.PrintVisual(gridPrintLaporan, "Laporan PWSKIA");
            }
        }
    }
}
