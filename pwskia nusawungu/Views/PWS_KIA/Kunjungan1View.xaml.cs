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
using pwskia_nusawungu.ViewModels.PWSKIA;

namespace pwskia_nusawungu.Views.PWS_KIA
{
    /// <summary>
    /// Interaction logic for Kunjungan1.xaml
    /// </summary>
    public partial class Kunjungan1View : Page
    {

        private int num = 1;
        public string penanggungJawab { get; set; }
        public Kunjungan1View(string adminName)
        {
            InitializeComponent();

            GetDesa();
            penanggungJawab = adminName;
        }

        public void GetDesa()
        {
            DesaContext desa = new DesaContext();
            foreach(Desa des in desa.GetDesa())
            {
                comBoxDesa.Items.Add(des.nama);
                comBoxUbahDesa.Items.Add(des.nama);
            }
        }

        public string GetBulan(int bulan)
        {
            switch (bulan)
            {
                case 1:
                    return "Januari";
                case 2:
                    return "Februari";
                case 3:
                    return "Maret";
                case 4:
                    return "April";
                case 5:
                    return "Mei";
                case 6:
                    return "Juni";
                case 7:
                    return "Juli";
                case 8:
                    return "Agustus";
                case 9:
                    return "September";
                case 10:
                    return "Oktober";
                case 11:
                    return "November";
                case 12:
                    return "Desember";
                default:
                    return "";
            }
        }

        private void btnTriggerLihatData_Click(object sender, RoutedEventArgs e)
        {
            stackPanelLihatData.IsEnabled = true;
            comBoxDesa.Text = "";
        }

        private void btnSubmitLihatData_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void txtNumberFormat_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void btnTriggerUbahData_Click(object sender, RoutedEventArgs e)
        {
            stackPanelEditData.IsEnabled = true;
        }

        private void btnSimpanData_Click(object sender, RoutedEventArgs e)
        {
            Kunjungan1ViewModel context = new Kunjungan1ViewModel();
            Kunjungan kunjungan = new Kunjungan();

            kunjungan.id = num;
            kunjungan.bulan = GetBulan(int.Parse(DateTime.Now.Month.ToString("d1")));
            kunjungan.kunjunganKe = 1;
            kunjungan.desa = new Desa
            {
                nama = comBoxUbahDesa.Text.ToString(),
                jmlBulanLalu = 0,
                jmlBulanIni = Int32.Parse(txtJumlahBulanIni.Text),
                sasaran = new Sasaran { bumil = 116 },
                r = 0,
            };

            kunjungan.penanggungJawab = penanggungJawab;

            foreach (Kunjungan kj1 in context.AddDataKunjunganSatu(kunjungan))
            {
                dgKunjungan1.Items.Add(kj1);
            }

            num++;
        }
    }
}
