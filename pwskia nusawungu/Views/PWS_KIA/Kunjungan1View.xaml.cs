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
        public Kunjungan1View()
        {
            InitializeComponent();

            GetDesa();
            
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

        private void btnTriggerLihatData_Click(object sender, RoutedEventArgs e)
        {
            stackPanelLihatData.IsEnabled = true;
            comBoxDesa.Text = "";
        }

        private void btnSubmitLihatData_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void txtJumlahBulanIni_PreviewKeyDown(object sender, KeyEventArgs e)
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
            Base home = new Base();

            kunjungan.bulan = DateTime.Now.Month.ToString("d2");
            kunjungan.kunjunganKe = 1;
            kunjungan.desa = comBoxDesa.Text;
            kunjungan.jmlBulanIni = Int32.Parse(txtJumlahBulanIni.Text);

            kunjungan.penanggungJawab = home.profileName;
        }
    }
}
