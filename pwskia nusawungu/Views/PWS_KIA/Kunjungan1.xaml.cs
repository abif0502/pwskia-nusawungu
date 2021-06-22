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

            foreach (string bulan in new string[] { "Januari", "Februari", "Maret", "April", "Mei", "juni", "Juli", "Agustus", "September", "Novermber", "Desember" })
            {
                comBoxMonth.Items.Add(bulan);
            }
        }

    }
}
