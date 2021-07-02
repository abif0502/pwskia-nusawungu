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

namespace pwskia_nusawungu.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();
            GetDesa();
            GetNilaiSasaran();
        }

        public void GetNilaiSasaran()
        {
            DesaContext context = new DesaContext();
            foreach(Desa desa in context.GetSasaran())
            {
                dgSasaran.Items.Add(desa);
            }
        }

        public void GetDesa()
        {
            foreach (EnumDesa desa in Enum.GetValues(typeof(EnumDesa)))
            {
                comBoxDesaSasaran.Items.Add(desa.ToString());
            }
        }

        public string windowTitle()
        {
            return "Dashboard";
        }

        private void txtNumberFormat_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void btnSimpanSasaran_Click(object sender, RoutedEventArgs e)
        {
            DesaContext context = new DesaContext();
            Desa desa = new Desa();
            desa.nama = comBoxDesaSasaran.Text;
            desa.sasaran = new Sasaran()
            {
                bumil = int.Parse(txtBumil.Text),
                bulin = int.Parse(txtBulin.Text),
                bumilRisti = int.Parse(txtBumilRisti.Text)
            };
            
            try
            {
                string message = context.EditSasaran(desa);
                if (message != null)
                {
                    var yes = MessageBox.Show(message, "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgSasaran.Items.Clear();
                    GetNilaiSasaran();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Gagal!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            txtBulin.Clear();
            txtBumil.Clear();
            txtBumilRisti.Clear();
        }

        private void btnTriggerInputSasaran_Click(object sender, RoutedEventArgs e)
        {
            popUpFromSasaran.IsOpen = true;
        }
    }
}
