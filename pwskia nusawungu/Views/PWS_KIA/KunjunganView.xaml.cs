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
            //GetMonthsAndYears();
        }

        //private void txtNumberFormat_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
        //    {
        //        e.Handled = true;
        //    }
        //}

        //public void GetMonthsAndYears()
        //{
        //    int currentYear = int.Parse(DateTime.Now.ToString("yyyy")) - 5;
        //    for(int i=1; i<=12; i++)
        //    {
        //        comBoxBulan.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i));
        //        comBoxTahun.Items.Add(currentYear.ToString());
        //        currentYear++;
        //    }
        //}


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

        private void btnTriggerInputData_Click(object sender, RoutedEventArgs e)
        {
            FormInputData formInput = new FormInputData(penanggungJawab);
            formInput.ShowDialog();
        }

        private void btnTriggerLihatData_Click(object sender, RoutedEventArgs e)
        {
            
        }

        

        private void btnTriggerUbahData_Click(object sender, RoutedEventArgs e)
        {
            
        }

        
        //private void btnLihatData_Click(object sender, RoutedEventArgs e)
        //{
        //    dgKunjungan1.Items.Clear();
        //    int kunjunganke;
        //    if (comBoxKunjunganKe.Text != "")
        //    {
        //        try
        //        {
        //            kunjunganke = int.Parse(comBoxKunjunganKe.Text);
        //            GetDataKunjungan(kunjunganke);
        //        }catch(Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "Info!", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Pilih kunjungan ke-", "Info!", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //}
    }
}
