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
    public partial class Kunjungan1 : Page
    {
        public Kunjungan1()
        {
            InitializeComponent();

            GetDesa();
            
        }

        public void GetDesa()
        {
            Desa desa = new Desa();
            foreach(string des in desa.GetDesa())
            {
                comBoxDesa.Items.Add(des);
            }
        }

        private void btnTriggerLihatData_Click(object sender, RoutedEventArgs e)
        {
            stackPanelLihatData.Visibility = Visibility.Visible;
        }

        private void btnSubmitLihatData_Click(object sender, RoutedEventArgs e)
        {
            dgKunjungan1.Items.Clear();

            Kunjungan1ViewModel kunjungan1Context = new Kunjungan1ViewModel();

            string tanggal = dpTanggalK1.Text;
            string desa = comBoxDesa.Text;
            try
            {
                foreach (Kunjungan1Model kunjungan in kunjungan1Context.GetDataKunjunganByArgs(tanggal, desa))
                {
                    dgKunjungan1.Items.Add(kunjungan);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
