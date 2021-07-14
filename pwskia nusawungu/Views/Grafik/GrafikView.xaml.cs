using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace pwskia_nusawungu.Views.Grafik
{
    /// <summary>
    /// Interaction logic for GrafikView.xaml
    /// </summary>
    public partial class GrafikView : Page
    {
        public Admin penanggungJawab { get; set; }

        public GrafikView(Admin admin)
        {
            InitializeComponent();
            penanggungJawab = admin;
            GetMonthsAndYear();


        }

        private void GetMonthsAndYear()
        {
            Tanggal tanggal = new Tanggal();

            foreach (string bulan in tanggal.GetDaftarBulan())
            {
                comBoxBulan.Items.Add(bulan);
            }

            PwskiaViewModel kunjunganContext = new PwskiaViewModel();
            foreach (string tahun in kunjunganContext.GetDaftarTahun())
            {
                comBoxTahun.Items.Add(tahun);
            }
        }

        private List<float> TargetPersentase()
        {
            List<float> targets = new List<float>();
            for(int i = 0; i < 12; i++)
            {
                targets.Add((float)(100.0 / 12.0 * (i + 1)));
            }

            return targets;
        }

        private void DrawRectangle(Pwskia pwskia, int column, int indexBulan)
        {
            Rectangle rectangle = new Rectangle();

            if (pwskia.desa.jmlBulanIni > pwskia.desa.jmlBulanLalu)
            {
                if (pwskia.desa.persentase > TargetPersentase()[indexBulan])
                {
                    rectangle.Fill = Brushes.Green;
                }

                else
                {
                    rectangle.Fill = Brushes.Yellow;
                }
            }
            else if(pwskia.desa.jmlBulanIni < pwskia.desa.jmlBulanLalu)
            {
                if(pwskia.desa.persentase > TargetPersentase()[indexBulan])
                {
                    rectangle.Fill = Brushes.Blue;
                }
                else
                {
                    rectangle.Fill = Brushes.Red;
                }
            }
            else
            {
                rectangle.Fill = Brushes.White;
                rectangle.Stroke = Brushes.Black;
            }

            
            rectangle.Height = 288.0 * pwskia.desa.persentase/100.0;
            rectangle.VerticalAlignment = VerticalAlignment.Bottom;
            rectangle.Margin = new Thickness(5, 0, 5, 0);

            gridGrafikPwskia.Children.Add(rectangle);

            Grid.SetColumn(rectangle, column);
            Grid.SetRow(rectangle, 0);
        }

        public void GetJumlah(Pwskia pwskia, int column)
        {
            TextBlock textBulanIni = new TextBlock();
            TextBlock textBulanlalu = new TextBlock();

            textBulanIni.Text = pwskia.desa.jmlBulanIni.ToString();
            textBulanIni.TextWrapping = TextWrapping.Wrap;
            textBulanIni.TextAlignment = TextAlignment.Center;
            textBulanIni.VerticalAlignment = VerticalAlignment.Center;
            textBulanIni.FontSize = 12;

            textBulanlalu.Text = pwskia.desa.jmlBulanLalu.ToString();
            textBulanlalu.TextWrapping = TextWrapping.Wrap;
            textBulanlalu.TextAlignment = TextAlignment.Center;
            textBulanlalu.VerticalAlignment = VerticalAlignment.Center;
            textBulanlalu.FontSize = 12;

            gridGrafikPwskia.Children.Add(textBulanIni);
            gridGrafikPwskia.Children.Add(textBulanlalu);


            Grid.SetColumn(textBulanIni, column);
            Grid.SetColumn(textBulanlalu, column);

            Grid.SetRow(textBulanIni, 2);
            Grid.SetRow(textBulanlalu, 3);
        }

        private void GetData(int idJenis, string bulan, string tahun)
        {
            PwskiaViewModel pwskiaContext = new PwskiaViewModel();
            gridGrafikPwskia.Children.Clear();

            Tanggal tanggal = new Tanggal();
            int indexBulan = Array.IndexOf(tanggal.GetDaftarBulan(), bulan);

            string dispPersentase = "";

            try
            {
                int column = 0;
                foreach(Pwskia pwskia in pwskiaContext.GetAllDataPwskia(idJenis, bulan + " " + tahun).OrderByDescending(o=>o.desa.persentase).ToList())
                {
                    TextBlock text = new TextBlock();
                    TextBlock textDesa = new TextBlock();

                    dispPersentase = pwskia.desa.persentase.ToString("0.00");

                    text.Text = $"{dispPersentase}%";
                    text.TextAlignment = TextAlignment.Center;
                    text.VerticalAlignment = VerticalAlignment.Center;
                    text.FontSize = 12;

                    textDesa.Text = $"{pwskia.desa.nama}";
                    textDesa.TextAlignment = TextAlignment.Center;
                    textDesa.VerticalAlignment = VerticalAlignment.Center;
                    textDesa.FontSize = 12;

                    DrawRectangle(pwskia, column, indexBulan);
                    GetJumlah(pwskia, column);

                    gridGrafikPwskia.Children.Add(text);
                    gridGrafikPwskia.Children.Add(textDesa);

                    Grid.SetColumn(text, column);
                    Grid.SetRow(text, 1);

                    Grid.SetColumn(textDesa, column);
                    Grid.SetRow(textDesa, 4);

                    column++;
                }
            }catch(Exception ex)
            {
                
            }
        }

        private void comBoxJenis_DropDownClosed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comBoxJenis.Text))
            {
                comBoxBulan.IsEnabled = true;
            }
        }

        private void comBoxBulan_DropDownClosed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comBoxBulan.Text))
            {
                comBoxTahun.IsEnabled = true;
            }
        }

        private void DrawChart()
        {
            gridBulan.Children.Clear();
            gridTextLabel.Children.Clear();

            string[] label = { "% Kumulatif", "Jumlah Bulan Ini","Jumlah Bulan Lalu", "Desa" };

            for(int i=0; i<4; i++)
            {
                TextBlock textLabel = new TextBlock();
                textLabel.Text = label[i];
                textLabel.FontSize = 12;
                textLabel.VerticalAlignment = VerticalAlignment.Center;
                textLabel.TextAlignment = TextAlignment.Left;
                textLabel.Margin = new Thickness(5, 0, 0, 0);

                gridTextLabel.Children.Add(textLabel);
                Grid.SetRow(textLabel, i);
            }

            Tanggal tanggal = new Tanggal();
            string persentase;
            string bulan = comBoxBulan.Text;
            string tahun = comBoxTahun.Text;
            int idJenis;

            // Daftar Sasaran Bulan
            for (int i = 0; i < 12; i++)
            {
                TextBlock text = new TextBlock();
                TextBlock textPersentase = new TextBlock();
                Separator separator = new Separator();

                text.Text = tanggal.GetDaftarBulan()[i];
                text.TextAlignment = TextAlignment.Left;
                text.VerticalAlignment = VerticalAlignment.Center;
                text.Margin = new Thickness(5, 0, 0, 0);
                text.FontSize = 12;

                persentase = ((float)(100.0 / 12.0 * (i + 1))).ToString("0.00");

                textPersentase.Text = $"{persentase} %";
                textPersentase.TextAlignment = TextAlignment.Left;
                textPersentase.VerticalAlignment = VerticalAlignment.Center;
                textPersentase.FontSize = 12;

                separator.VerticalAlignment = VerticalAlignment.Top;
                separator.Margin = new Thickness(5, 0, 0, 0);

                gridBulan.Children.Add(text);
                gridBulan.Children.Add(separator);
                gridBulan.Children.Add(textPersentase);


                Grid.SetRow(text, 11 - i);

                Grid.SetRow(textPersentase, 11 - i);
                Grid.SetColumn(textPersentase, 1);

                Grid.SetRow(separator, 11 - i);
                Grid.SetColumnSpan(separator, 2);


            }


            switch (comBoxJenis.Text)
            {
                case "K1":
                    idJenis = 1;
                    break;
                case "K4":
                    idJenis = 2;
                    break;
                case "K6":
                    idJenis = 3;
                    break;
                case "KF":
                    idJenis = 4;
                    break;
                case "Persalinan Nakes":
                    idJenis = 5;
                    break;
                case "Deteksi Risiko Nakes":
                    idJenis = 6;
                    break;
                case "Deteksi Risiko Masyarakat":
                    idJenis = 7;
                    break;
                default:
                    idJenis = 0;
                    break;
            }
            txtJudulGrafik.Text = $"Grafik \"{comBoxJenis.Text}\" {bulan} {tahun}";

            // Ambil data dan gambar grafik
            GetData(idJenis, bulan, tahun);
        }

        private void comBoxTahun_DropDownClosed(object sender, EventArgs e)
        {
            DrawChart();
        }

        private void btnTriggerPrint_Click(object sender, RoutedEventArgs e)
        {
            
            txtPrintJenis.Text = $"GRAFIK CAKUPAN {comBoxJenis.Text}";
            txtPrintBulanDanTahun.Text = $"{comBoxBulan.Text} {comBoxTahun.Text}";
            txtPenanggungJawab.Text = penanggungJawab.nama;
            txtNIPPenanggungJawab.Text = $"NIP. {penanggungJawab.nip}";

            gridGrafik.Children.Remove(surfaceGrafik);
            
            paperContent.Children.Add(surfaceGrafik);
            DrawChart();

            PrintDialog print = new PrintDialog();
            if (print.ShowDialog() == true)
            {
                print.PrintTicket.PageOrientation = PageOrientation.Landscape;
                print.PrintVisual(paperPrintGrafikA4, "Cetak Grafik");
            }

            // Kembali Gambar Grafik
            paperContent.Children.Remove(surfaceGrafik);
            gridGrafik.Children.Add(surfaceGrafik);
            DrawChart();
        }
    }
}
